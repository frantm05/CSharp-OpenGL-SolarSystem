using OpenGL;
using SolarSystemProject.UI;
using SolarSystemProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseTest.UI
{
    public class RenderManager
    {
        private readonly Camera camera;
        private readonly ViewStateManager viewStateManager;
        private readonly UIRenderer uiRenderer;
        private readonly LightingManager lightingManager;
        private readonly SolarSystem solarSystem;

        public RenderManager(Camera camera, ViewStateManager viewStateManager,
            UIRenderer uiRenderer, LightingManager lightingManager, SolarSystem solarSystem)
        {
            this.camera = camera;
            this.viewStateManager = viewStateManager;
            this.uiRenderer = uiRenderer;
            this.lightingManager = lightingManager;
            this.solarSystem = solarSystem;
        }

        public void Render()
        {
            ClearScreen();
            SetupCamera();

            if (viewStateManager.IsPlanetSelected && viewStateManager.SelectedObject != null)
            {
                RenderSelectedObject();
            }
            else
            {
                RenderSolarSystem();
            }

            RenderUI();
        }

        private void ClearScreen()
        {
            gl.ClearColor(0.0f, 0, 0.1f, 1);
            gl.Clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);
        }

        private void SetupCamera()
        {
            gl.MatrixMode(gl.MODELVIEW);
            gl.LoadIdentity();
        }

        private void RenderSelectedObject()
        {
            gl.Translatef(0, 0, -7f);
            lightingManager.SetLightPosition(0f, 0f, 1f);

            if (viewStateManager.SelectedObject is Star sun)
                sun.Draw();
            else
                viewStateManager.SelectedObject.DrawSelected();
        }

        private void RenderSolarSystem()
        {
            if (viewStateManager.IsRotating) solarSystem.Update();

            camera.ApplyTransformations();
            solarSystem.Draw();
        }

        private void RenderUI()
        {
            uiRenderer.DrawMenu(viewStateManager);
            uiRenderer.DrawPlanetInfo(viewStateManager.SelectedObject);
        }
    }
}
