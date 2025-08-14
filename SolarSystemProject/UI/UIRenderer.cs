using OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolarSystemProject.UI
{
    public class UIRenderer
    {
        private readonly Control parentControl;
        private readonly Font infoFont = new Font("Arial", 12, FontStyle.Bold);

        public UIRenderer(Control parentControl)
        {
            this.parentControl = parentControl ?? throw new ArgumentNullException(nameof(parentControl));
        }

        public void DrawMenu(ViewStateManager viewStateManager)
        {
            if (!viewStateManager.ShowMenu) return;

            Begin2DDraw();

            DrawMenuBackground(viewStateManager.PlanetNames.Count);
            DrawMenuBorder(viewStateManager.PlanetNames.Count);
            DrawMenuItems(viewStateManager);

            End2DDraw();
        }

        public void DrawPlanetInfo(CelestialObject selectedObject)
        {
            if (selectedObject == null) return;

            Begin2DDraw();

            DrawInfoBackground();
            DrawInfoBorder();
            DrawObjectInfo(selectedObject);

            End2DDraw();
        }

        private void DrawMenuBackground(int itemCount)
        {
            gl.Color4f(0.1f, 0.1f, 0.2f, 0.95f);
            gl.Begin(gl.QUADS);
            gl.Vertex2f(50, 50);
            gl.Vertex2f(300, 50);
            gl.Vertex2f(300, 50 + itemCount * 30 + 40);
            gl.Vertex2f(50, 50 + itemCount * 30 + 40);
            gl.End();
        }

        private void DrawMenuBorder(int itemCount)
        {
            gl.Color4f(0.5f, 0.5f, 0.8f, 1.0f);
            gl.LineWidth(2);
            gl.Begin(gl.LINE_LOOP);
            gl.Vertex2f(50, 50);
            gl.Vertex2f(300, 50);
            gl.Vertex2f(300, 50 + itemCount * 30 + 40);
            gl.Vertex2f(50, 50 + itemCount * 30 + 40);
            gl.End();
        }

        private void DrawMenuItems(ViewStateManager viewStateManager)
        {
            for (int i = 0; i < viewStateManager.PlanetNames.Count; i++)
            {
                Color color = (i == viewStateManager.SelectedMenuIndex) ? Color.Yellow : Color.White;
                TextRenderer.DrawText(parentControl, viewStateManager.PlanetNames[i], 70, 80 + i * 30, infoFont, color);
            }
        }

        private void DrawInfoBackground()
        {
            gl.Color4f(0.1f, 0.1f, 0.2f, 0.95f);
            gl.Begin(gl.QUADS);
            gl.Vertex2f(parentControl.Width - 275, 45);
            gl.Vertex2f(parentControl.Width - 20, 45);
            gl.Vertex2f(parentControl.Width - 20, 225);
            gl.Vertex2f(parentControl.Width - 275, 225);
            gl.End();
        }

        private void DrawInfoBorder()
        {
            gl.Color4f(0.5f, 0.5f, 0.8f, 1.0f);
            gl.LineWidth(2);
            gl.Begin(gl.LINE_LOOP);
            gl.Vertex2f(parentControl.Width - 275, 45);
            gl.Vertex2f(parentControl.Width - 20, 45);
            gl.Vertex2f(parentControl.Width - 20, 225);
            gl.Vertex2f(parentControl.Width - 275, 225);
            gl.End();
        }

        private void DrawObjectInfo(CelestialObject selectedObject)
        {
            int textY = 70;

            string name = selectedObject is Star ? "Sun" : (selectedObject as Planet)?.Name ?? "Unknown";
            TextRenderer.DrawText(parentControl, name, parentControl.Width - 235, textY,
                new Font("Arial", 14, FontStyle.Bold), Color.White);
            textY += 38;

            if (selectedObject is Star)
            {
                DrawInfoLine("Diameter: 1,392,700 km", ref textY);
                DrawInfoLine("Surface temp: 5,500°C", ref textY);
                DrawInfoLine("Core temp: 15,000,000°C", ref textY);
            }
            else if (selectedObject is Planet planet)
            {
                if (planet.Size > 0 && planet.OrbitalRadius > 0)
                {
                    DrawInfoLine($"Diameter: {GetPlanetDiameter(planet.Size)} km", ref textY);
                    DrawInfoLine($"Distance: {planet.OrbitalRadius * 149.6:F1}M km", ref textY);
                    DrawInfoLine($"Orbit: {365 / planet.OrbitalSpeed:F1} days", ref textY);
                }
                else
                {
                    DrawInfoLine("Invalid planet data!", ref textY);
                }
            }
        }

        private void DrawInfoLine(string text, ref int yPos)
        {
            int textX = parentControl.Width - 235;
            TextRenderer.DrawText(parentControl, text, textX, yPos, infoFont, Color.White);
            yPos += 28;
        }

        private string GetPlanetDiameter(float size)
        {
            if (size < 0.25f) return "4,880"; // Mercury
            if (size < 0.45f) return "12,104"; // Venus
            if (size < 0.55f) return "12,742"; // Earth
            if (size < 0.4f) return "6,779"; // Mars
            if (size < 1.1f) return "139,820"; // Jupiter
            return "116,460"; // Saturn
        }

        private void Begin2DDraw()
        {
            gl.Disable(gl.DEPTH_TEST);
            gl.Disable(gl.LIGHTING);

            gl.MatrixMode(gl.PROJECTION);
            gl.PushMatrix();
            gl.LoadIdentity();
            glu.Ortho2D(0, parentControl.Width, parentControl.Height, 0);

            gl.MatrixMode(gl.MODELVIEW);
            gl.PushMatrix();
            gl.LoadIdentity();
        }

        private void End2DDraw()
        {
            gl.PopMatrix();
            gl.MatrixMode(gl.PROJECTION);
            gl.PopMatrix();
            gl.MatrixMode(gl.MODELVIEW);

            gl.Enable(gl.LIGHTING);
            gl.Enable(gl.DEPTH_TEST);
        }

    }
}
