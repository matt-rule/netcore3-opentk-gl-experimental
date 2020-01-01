using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace netcore3_opentk_gl_experimental
{
    public class MainWindow : GameWindow
    {
        private const int DefaultWidth = 750;
        private const int DefaultHeight = 500;
        public const int MajorVersion = 4;
        public const int MinorVersion = 5;
        private const String WindowTitle = "OpenTK Demo";

        public MainWindow()
            : base(DefaultWidth,
                DefaultHeight,
                GraphicsMode.Default,
                WindowTitle,
                GameWindowFlags.Default,
                DisplayDevice.Default,
                MajorVersion,
                MinorVersion,
                GraphicsContextFlags.ForwardCompatible)
        {
        }

        protected override void OnResize(EventArgs e)
        {
            
        }

        protected override void OnLoad(EventArgs e)
        {
            
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.ClearColor(0.0f, 0.5f, 0.0f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            SwapBuffers();
        }
    }
}
