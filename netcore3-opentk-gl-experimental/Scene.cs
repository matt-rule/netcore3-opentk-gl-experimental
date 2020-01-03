using System.Collections.Generic;
using System.Linq;
using netcore3_simple_game_engine;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace netcore3_opentk_gl_experimental
{
    public struct SceneInfo
    {
        public int Width;
        public int Height;
    };

    public class Scene : IScene
    {
        private SceneInfo sceneInfo;
        private double RotationAngle = 0.0;

        public List<Object3d> GameObjects = null;
        private ShaderProgram MainShaderProgram;

        public Scene(SceneInfo info)
        {
            sceneInfo = info;
        }

        public void Update(double elapsedTime)
        {
            RotationAngle += elapsedTime;

            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 projMatrix = Matrix4.CreateOrthographicOffCenter(0, sceneInfo.Width, 0, sceneInfo.Height, -1.0f, 1.0f);
            Matrix4 viewMatrix = Matrix4.Identity;
            Matrix4.CreateRotationZ((float)RotationAngle, out Matrix4 tempMatrix);
            Matrix4.CreateTranslation(-50.0f, -50.0f, 0.0f, out Matrix4 tempMatrix2);

            foreach (var obj in GameObjects)
            {
                Matrix4 mvp = tempMatrix2 * tempMatrix * obj.ModelMatrix * viewMatrix * projMatrix;
                    
                GL.UseProgram(MainShaderProgram.ProgramId);

                GlCustomUtil.Bind3dObject(obj);

                GL.UniformMatrix4(MainShaderProgram.MatrixShaderLocation, false, ref mvp);

                GL.DrawElements(PrimitiveType.Triangles, obj.Indices.Length, DrawElementsType.UnsignedInt, 0);
                var x = GL.GetError();
            }
        }

        // Usage: Call after OpenGL has been initialised.
        public void Initialise()
        {
            GameObjects = new List<BufferData> {
                Geometry.GetTriangle1(),
                Geometry.GetTriangle2()
            }
            .Select(x => GlCustomUtil.GeometryTo3dObject(x))
            .ToList();

            // TODO: Use the unbind function to clean up. Maybe have a class CustomGlContext.
            foreach (Object3d obj3d in GameObjects)
                GlCustomUtil.Bind3dObject(obj3d);

            MainShaderProgram = GlCustomUtil.CreateShaderProgram();
        }
    }
}