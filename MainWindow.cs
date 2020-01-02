﻿using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace netcore3_opentk_gl_experimental
{
    public class MainWindow : GameWindow
    {
        private const int DEFAULT_WIDTH = 750;
        private const int DEFAULT_HEIGHT = 500;
        public const int MAJOR_VERSION = 4;
        public const int MINOR_VERSION = 5;
        private const String WINDOW_TITLE = "OpenTK Demo";

        private List<Object3d> GameObjects = null;
        private int ProgramId;
        private ShaderProgram MainShaderProgram;

        public MainWindow()
            : base(DEFAULT_WIDTH,
                DEFAULT_HEIGHT,
                GraphicsMode.Default,
                WINDOW_TITLE,
                GameWindowFlags.Default,
                DisplayDevice.Default,
                MAJOR_VERSION,
                MINOR_VERSION,
                GraphicsContextFlags.ForwardCompatible)
        {
        }

        protected override void OnResize(EventArgs e)
        {
            
        }

        protected override void OnLoad(EventArgs e)
        {
            GameObjects = new List<BufferData> {
                Geometry.GetTriangle1(),
                Geometry.GetTriangle2()
            }
            .Select(x => GlCustomUtil.GeometryTo3dObject(x))
            .ToList();

            foreach (Object3d obj3d in GameObjects)
                GlCustomUtil.Bind3dObject(obj3d);

            MainShaderProgram = GlCustomUtil.CreateShaderProgram();
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            GL.PatchParameter(PatchParameterInt.PatchVertices, 3);

            GL.Enable(EnableCap.Blend);
            GL.BlendEquation(BlendEquationMode.FuncAdd);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 projMatrix = Matrix4.CreateOrthographicOffCenter(0, DEFAULT_WIDTH, 0, DEFAULT_HEIGHT, -1.0f, 1.0f);
            Matrix4 viewMatrix = Matrix4.Identity;

            foreach (var obj in GameObjects)
            {
                Matrix4 modelMatrix = Matrix4.CreateTranslation(new Vector3(obj.Position.X, obj.Position.Y, 0.0f));
                Matrix4 mvp = modelMatrix * viewMatrix * projMatrix;
                    
                GL.UseProgram(MainShaderProgram.ProgramId);

                GlCustomUtil.Bind3dObject(obj);

                GL.UniformMatrix4(MainShaderProgram.MatrixShaderLocation, false, ref mvp);

                GL.DrawElements(PrimitiveType.Triangles, obj.Indices.Length, DrawElementsType.UnsignedInt, 0);
                var x = GL.GetError();
            }

            SwapBuffers();
        }
    }
}
