using OpenGL;
using System;
using System.Collections.Generic;

namespace SolarSystemProject
{
    public class Planet : CelestialObject 
    {
        public String Name { get; set; }
        public List<Moon> Moons { get; set; } = new List<Moon>();

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            foreach (var moon in Moons)
            {
                moon.Update(deltaTime);
            }
        }

        public override void Draw()
        {
            DrawOrbit();

            base.Draw(); 

            foreach (var moon in Moons)
            {
                moon.Draw();
            }
        }

        protected override void SetupMaterial()
        {
            float[] matAmbient = { color[0] * 0.3f, color[1] * 0.3f, color[2] * 0.3f, 1.0f };
            float[] matDiffuse = { color[0], color[1], color[2], 1.0f };
            float[] matSpecular = { 0.5f, 0.5f, 0.5f, 1.0f };
            float matShininess = 32.0f;

            gl.Materialfv(gl.FRONT, gl.AMBIENT, matAmbient);
            gl.Materialfv(gl.FRONT, gl.DIFFUSE, matDiffuse);
            gl.Materialfv(gl.FRONT, gl.SPECULAR, matSpecular);
            gl.Materialf(gl.FRONT, gl.SHININESS, matShininess);
        }

        private void DrawOrbit()
        {
            if (OrbitAround != null) {

                gl.Disable(gl.LIGHTING); // Vypnutí osvětlení 

                gl.PushMatrix(); // Uložení aktuální matice
                gl.Translatef(OrbitAround.X, OrbitAround.Y, OrbitAround.Z); // Přesun do středu oběžné dráhy
                gl.Begin(gl.LINE_LOOP); // Vykreslení oběžné dráhy jako uzavřené čáry
                gl.Color4f(0.5f, 0.5f, 0.5f, 0.5f); // Barva oběžné dráhy (šedá s průhledností)

                // Vykreslení oběžné dráhy jako kruh
                for (int i = 0; i < 50; i++)
                {
                    float angle = (float)(i * Math.PI * 2 / 50);
                    float x = (float)Math.Cos(angle) * OrbitalRadius;
                    float z = (float)Math.Sin(angle) * OrbitalRadius;
                    gl.Vertex3f(x, 0, z);
                }

                gl.End(); // Konec vykreslování oběžné dráhy
                gl.PopMatrix(); // Obnovení původní matice

                gl.Enable(gl.LIGHTING); // Znovu zapnutí osvětlení
            }
        }
    }
}
