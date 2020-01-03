using System;
using System.Collections.Generic;
using System.Linq;
using netcore3_simple_game_engine;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace netcore3_opentk_gl_experimental
{
    class Program
    {
        private const int DEFAULT_WIDTH = 750;
        private const int DEFAULT_HEIGHT = 500;
        public const int MAJOR_VERSION = 4;
        public const int MINOR_VERSION = 5;
        private const String WINDOW_TITLE = "OpenTK Demo";
        public const int FRAMES_PER_SECOND = 60;
        Scene scene;

        Program()
        {
            var sceneInfo = new SceneInfo {
                Width = DEFAULT_WIDTH,
                Height = DEFAULT_HEIGHT
            };
            scene = new Scene(sceneInfo);
        }

        private static void OnResize(EventArgs e)
        {
            
        }

        private static void OnLoad(EventArgs e)
        {
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            GL.PatchParameter(PatchParameterInt.PatchVertices, 3);

            GL.Enable(EnableCap.Blend);
            GL.BlendEquation(BlendEquationMode.FuncAdd);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        }

        static void Main(string[] args)
        {
            WindowOptions options = new WindowOptions {

            };

            new MainWindow(options).Run(FRAMES_PER_SECOND);
        }
    }
}
