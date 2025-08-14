using MouseTest;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SolarSystemProject
{
    public class InputManager
    {
        private Point lastMousePos;
        private bool isDragging = false;
        private readonly Camera camera;
        private readonly ViewStateManager viewStateManager;
        private readonly Control parentControl;

        public InputManager(Camera camera, ViewStateManager viewStateManager, Control parentControl)
        {
            this.camera = camera ?? throw new ArgumentNullException(nameof(camera));
            this.viewStateManager = viewStateManager ?? throw new ArgumentNullException(nameof(viewStateManager));
            this.parentControl = parentControl ?? throw new ArgumentNullException(nameof(parentControl));
        }

        public void onMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !viewStateManager.IsPlanetSelected)
            {
                isDragging = true;
                lastMousePos = e.Location;
                parentControl.Cursor = Cursors.SizeAll;
            }
        }
        public void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
                parentControl.Cursor = Cursors.Default;
            }
        }

        public void OnMouseMove(MouseEventArgs e)
        {
            if (!viewStateManager.IsPlanetSelected && isDragging)
            {
                int deltaX = e.X - lastMousePos.X;
                int deltaY = e.Y - lastMousePos.Y;

                camera.Rotate(deltaY * 0.5f, deltaX * 0.5f);
                lastMousePos = e.Location;
            }
        }

        public void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    if (!viewStateManager.ShowMenu && !viewStateManager.IsPlanetSelected)
                        viewStateManager.ToggleRotation();
                    break;
                case Keys.W:
                    if (!viewStateManager.ShowMenu && !viewStateManager.IsPlanetSelected)
                        camera.Move(0, 0, 1);
                    break;
                case Keys.S:
                    if (!viewStateManager.ShowMenu && !viewStateManager.IsPlanetSelected)
                        camera.Move(0, 0, -1);
                    break;
                case Keys.A:
                    if (!viewStateManager.ShowMenu && !viewStateManager.IsPlanetSelected)
                        camera.Move(1, 0, 0);
                    break;
                case Keys.D:
                    if (!viewStateManager.ShowMenu && !viewStateManager.IsPlanetSelected)
                        camera.Move(-1, 0, 0);
                    break;
                case Keys.Up:
                    if (viewStateManager.ShowMenu)
                        viewStateManager.MoveMentSelection(-1);
                    break;
                case Keys.Down:
                    if (viewStateManager.ShowMenu)
                        viewStateManager.MoveMentSelection(1);
                    break;
                case Keys.Enter:
                    if (viewStateManager.ShowMenu)
                        viewStateManager.SelectCurrentMenuItem();
                    break;
                case Keys.Escape:
                    HandleEscapeKey();
                    break;
            }

        }
        private void HandleEscapeKey()
        {
            if (viewStateManager.IsPlanetSelected)
            {
                viewStateManager.DeselectPlanet();
            }
            else if (!viewStateManager.IsPlanetSelected)
            {
                viewStateManager.ToggleMenu();
            }
            else if (viewStateManager.ShowMenu)
            {
                viewStateManager.HideMenu();
            }
        }
    }
}

