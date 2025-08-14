using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MouseTest.UI;
using OpenGL;
using SolarSystemProject.UI;

namespace SolarSystemProject
{
    public partial class Form1 : Form
    {
        private OpenGL.Context context;
        private readonly Camera camera;
        private readonly ViewStateManager viewStateManager;
        private readonly InputManager inputManager;
        private readonly UIRenderer uiRenderer;
        private readonly LightingManager lightingManager;
        private readonly RenderManager renderManager;
        private readonly SolarSystem solarSystem;
        public Form1()
        {
            InitializeComponent();
            SetupForm();

            // Inicializace komponent
            camera = new Camera();
            solarSystem = new SolarSystem();
            viewStateManager = new ViewStateManager(solarSystem);
            inputManager = new InputManager(camera, viewStateManager, this);
            uiRenderer = new UIRenderer(this);
            lightingManager = new LightingManager();
            renderManager = new RenderManager(camera, viewStateManager, uiRenderer, lightingManager, solarSystem);

            InitializeOpenGL();
            SetupEventHandlers();
        }

        private void SetupForm()
        {
            this.ClientSize = new Size(1024, 768);
        }

        private void SetupEventHandlers()
        {
            this.MouseDown += (s, e) => inputManager.onMouseDown(e);
            this.MouseUp += (s, e) => inputManager.OnMouseUp(e);
            this.MouseMove += (s, e) => inputManager.OnMouseMove(e);
            this.KeyDown += (s, e) => inputManager.OnKeyDown(e);
        }

        private void InitializeOpenGL()
        {
            context = new Context(this, 32, 32, 0);
            SetupProjection();
            SetupOpenGLState();
            lightingManager.InitializeLighting();
        }

        private void SetupProjection()
        {
            gl.MatrixMode(gl.PROJECTION);
            gl.LoadIdentity();
            glu.Perspective(40.0, 1024f / 768f, 0.001, 100); 
        }

        private void SetupOpenGLState()
        {
            gl.Enable(gl.DEPTH_TEST);
            gl.DepthFunc(gl.LEQUAL);
            gl.Enable(gl.NORMALIZE);
            gl.Enable(gl.BLEND);
            gl.BlendFunc(gl.SRC_ALPHA, gl.ONE_MINUS_SRC_ALPHA);
        }

        public void Draw()
        {
            renderManager.Render();
            context.SwapBuffers();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Prázdná implementace - vše je inicializováno v konstruktoru
        }
    }
}
