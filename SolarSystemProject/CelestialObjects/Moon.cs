using OpenGL;

namespace SolarSystemProject
{
    public class Moon : CelestialObject
    {
        protected override void SetupMaterial()
        {
            float[] matAmbient = { color[0] * 0.3f, color[1] * 0.3f, color[2] * 0.3f, 1.0f };
            float[] matDiffuse = { color[0], color[1], color[2], 1.0f };
            float[] matSpecular = { 0.1f, 0.1f, 0.1f, 1.0f };
            float matShininess = 5.0f; 

            gl.Materialfv(gl.FRONT, gl.AMBIENT, matAmbient);    // Okolní odrazivost materiálu - barva se přičítá k celkovému ambientnímu světlu - params je pole 4f (RGBA)
            gl.Materialfv(gl.FRONT, gl.DIFFUSE, matDiffuse);    // Difůzní odrazivost materiálu - určuje jak materiál odráží světlo z bodových nebo směrových zdrojů světla - params je pole 4f (RGBA)
            gl.Materialfv(gl.FRONT, gl.SPECULAR, matSpecular);  // Zrcadlová odrazivost materiálu - určuje barvu odlesků na povrchu objektu - params je pole 4f (RGBA)
            gl.Materialf(gl.FRONT, gl.SHININESS, matShininess); // Lesk materiálu - určuje jak ostře / rozmazaně se odlesky objevují - param je float (0-128, kde 0 je matný a 128 velmi lesklý)
        }
    }
}
