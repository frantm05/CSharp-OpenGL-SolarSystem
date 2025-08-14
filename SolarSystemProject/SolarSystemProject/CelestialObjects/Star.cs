using MouseTest.UI;
using OpenGL;
using System;

namespace SolarSystemProject
{
    public class Star : CelestialObject
    {
        private Random random = new Random();
        LightingManager lightingManager = new LightingManager();

        public Star(float size) {
            Size = size;
        }

        public override void Draw()
        {
            lightingManager.InitializeLighting(); // Nastavení pozice světla na slunce
            float[] lightPosition = { 0.0f, 0.0f, 0.0f, 1.0f }; // pozice světla
            gl.Lightfv(gl.LIGHT0, gl.POSITION, lightPosition);

            gl.Disable(gl.LIGHTING);

            // Samotné slunce
            gl.PushMatrix();
            gl.Color4f(1.0f, 0.8f, 0.2f, 1.0f);
            DrawSphere(Size, 20, 20);
            gl.PopMatrix();

            gl.Enable(gl.LIGHTING);
        }

    }
}
