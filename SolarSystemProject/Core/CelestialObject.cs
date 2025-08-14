using System;
using OpenGL;

namespace SolarSystemProject
{
    public abstract class CelestialObject
    {
        
        public float OrbitalRadius { get; set; } // Poloměr oběžné dráhy
        public float OrbitalSpeed { get; set; } // Rychlost oběhu
        public float RotationSpeed { get; set; } // Rychlost rotace kolem vlastní osy 
        public float Size { get; set; } // Velikost objektu (poloměr koule)
        public float[] color { get; set; } // Barva objektu (RGBA)
        public float OrbitalPosition { get; set; } // Aktuální pozice na oběžné dráze
        public float RotationAngle { get; set; } // Úhel rotace kolem vlastní osy
        public CelestialObject OrbitAround { get; set; } // Objekt, kolem kterého se tento objekt otáčí (pokud null -> objekt je statický)

        public float X { get; private set; }
        public float Y { get; private set; }
        public float Z { get; private set; }

        public CelestialObject()
        {
            OrbitalPosition = (float)(new Random().NextDouble() * Math.PI * 2); // náhodná počáteční pozice na oběžné dráze
            RotationAngle = 0f; // výchozí úhel rotace kolem vlastní osy
            color = new float[] { 1f, 1f, 1f, 1f}; // výchozí barva (bílá)
        }

        public virtual void Update(float deltaTime)
        {
            if(OrbitalSpeed > 0)
            {
                OrbitalPosition += OrbitalSpeed * deltaTime; // aktualizace pozice na oběžné dráze
                // Zajištění, že pozice zůstane v rozsahu 0 - 2 * PI
                if (OrbitalPosition > Math.PI * 2) 
                    OrbitalPosition -= (float)(Math.PI * 2);
            }

            if(RotationSpeed > 0)
            {
                // Aktualizace úhlu rotace kolem vlastní osy
                RotationAngle += RotationSpeed * deltaTime;
                if (RotationAngle > 360f)
                    RotationAngle -= 360f;
            }

            UpdatePosition();


        }

        public virtual void UpdatePosition()
        {
            // Výpočet souřadnic v prostoru
            if (OrbitAround != null)
            {
                X = OrbitAround.X + (float)Math.Cos(OrbitalPosition) * OrbitalRadius;
                Z = OrbitAround.Z + (float)Math.Sin(OrbitalPosition) * OrbitalRadius;
                Y = OrbitAround.Y;
            }
            else
            {
                X = Y = Z = 0f; //statická pozice
            }
        }

        public virtual void Draw()
        {
            DrawCore(true); // Vykreslení jádra objektu s překladem do jeho pozice
        }

        public void DrawSelected()
        {
            DrawCore(false); // Vykreslení jádra objektu bez překladu do jeho pozice
        }

        public void DrawCore(bool translateToPosition)
        {
            SetupMaterial();

            gl.PushMatrix(); // Uložení aktuální matice
            if(translateToPosition)
                gl.Translatef(X, Y, Z); // Přesun do pozice objektu

            gl.Rotatef(RotationAngle, 0, 1, 0); // Rotace kolem osy Y
            gl.Color4f(color[0], color[1], color[2], color[3]); // Nastavení barvy objektu
            DrawSphere(Size, 20, 20);  // Vykreslení koule
            gl.PopMatrix(); // Obnovení původní matice
        }

        protected virtual void SetupMaterial()
        {
            // Základní materiál - přepíšou si ho potomci
            float[] matAmbient = { color[0] * 0.3f, color[1] * 0.3f, color[2] * 0.3f, 1.0f };
            float[] matDiffuse = { color[0], color[1], color[2], 1.0f };
            float[] matSpecular = { 0.3f, 0.3f, 0.3f, 1.0f };
            float matShininess = 16.0f;

            gl.Materialfv(gl.FRONT, gl.AMBIENT, matAmbient);
            gl.Materialfv(gl.FRONT, gl.DIFFUSE, matDiffuse);
            gl.Materialfv(gl.FRONT, gl.SPECULAR, matSpecular);
            gl.Materialf(gl.FRONT, gl.SHININESS, matShininess);
        }

        protected void DrawSphere(float radius, int slices, int stacks)
        {
            float drho = (float)(Math.PI / stacks);
            float dtheta = (float)(2.0 * Math.PI / slices);

            // Vykresli trojúhelníky tvořící kouli
            for (int i = 0; i < stacks; i++)
            {
                float rho = i * drho;
                float srho = (float)Math.Sin(rho);
                float crho = (float)Math.Cos(rho);
                float srhodrho = (float)Math.Sin(rho + drho);
                float crhodrho = (float)Math.Cos(rho + drho);

                gl.Begin(gl.TRIANGLE_STRIP);

                // Pro každý segment kružnice
                for (int j = 0; j <= slices; j++)
                {
                    float theta = (j == slices) ? 0.0f : j * dtheta;
                    float stheta = (float)Math.Sin(theta);
                    float ctheta = (float)Math.Cos(theta);

                    float x = stheta * srho;
                    float y = crho;
                    float z = ctheta * srho;
                    float nx = x;
                    float ny = y;
                    float nz = z;

                    // Normála pro správné osvětlení
                    gl.Normal3f(nx, ny, nz);
                    gl.Vertex3f(x * radius, y * radius, z * radius);

                    x = stheta * srhodrho;
                    y = crhodrho;
                    z = ctheta * srhodrho;
                    nx = x;
                    ny = y;
                    nz = z;

                    gl.Normal3f(nx, ny, nz);
                    gl.Vertex3f(x * radius, y * radius, z * radius);
                }

                gl.End();
            }
        }
    }
}
