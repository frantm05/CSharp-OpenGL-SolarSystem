using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseTest.UI
{
    public class LightingManager
    {
        public void InitializeLighting()
        {
            gl.Enable(gl.LIGHTING);
            gl.Enable(gl.LIGHT0);

            SetupLight();
        }

        private void SetupLight()
        {
            float[] lightAmbient = { 0.2f, 0.2f, 0.2f, 1.0f };
            float[] lightDiffuse = { 0.9f, 0.9f, 0.9f, 1.0f };
            float[] lightSpecular = { 1.0f, 1.0f, 1.0f, 1.0f };

            gl.Lightfv(gl.LIGHT0, gl.AMBIENT, lightAmbient);
            gl.Lightfv(gl.LIGHT0, gl.DIFFUSE, lightDiffuse);
            gl.Lightfv(gl.LIGHT0, gl.SPECULAR, lightSpecular);
        }

        public void SetLightPosition(float x, float y, float z, float w = 0f)
        {
            float[] lightPos = { x, y, z, w };
            gl.Lightfv(gl.LIGHT0, gl.POSITION, lightPos);
        }
    }
}
