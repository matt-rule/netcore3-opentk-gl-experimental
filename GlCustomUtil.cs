using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace netcore3_opentk_gl_experimental
{
    public struct ShaderProgram: IDisposable
    {
        public int ProgramId;
        public List<int> Shaders;
        public int MatrixShaderLocation;

        public void Dispose()
        {
            foreach (var shader in Shaders)
            {
                GL.DetachShader(ProgramId, shader);
                GL.DeleteShader(shader);
            }
            GL.DeleteProgram(ProgramId);
        }
    }

    public class GlCustomUtil
    {
        public static Object3d GeometryTo3dObject(BufferData bufferData)
        {
            return new Object3d()
            {
                Position = new Vector2(300.0f, 200.0f),
                Vertices = bufferData.Vertices,
                Indices = bufferData.Indices,
                VertexArrayObjectId = GL.GenVertexArray(),
                VertexBufferObjectId = GL.GenBuffer(),
                IndexBufferObjectId = GL.GenBuffer()
            };
        }

        public static void Bind3dObject(Object3d obj3d)
        {
            GL.BindVertexArray(obj3d.VertexArrayObjectId);
            GL.BindBuffer(BufferTarget.ArrayBuffer, obj3d.VertexArrayObjectId);

            // create vertex buffer
            GL.NamedBufferStorage(
                obj3d.VertexBufferObjectId,
                8 * sizeof(float) * obj3d.Vertices.Length,
                obj3d.Vertices,
                BufferStorageFlags.MapWriteBit);

            // position attribute
            GL.VertexArrayAttribBinding(obj3d.VertexArrayObjectId, 0, 0);
            GL.EnableVertexArrayAttrib(obj3d.VertexArrayObjectId, 0);
            // VertexArrayAttribFormat is the equivalent of glVertexAttribPointer
            GL.VertexArrayAttribFormat(obj3d.VertexArrayObjectId, 0, 4, VertexAttribType.Float, false, 0);

            // colour attribute
            // position precedes this; offset by its size
            GL.VertexArrayAttribBinding(obj3d.VertexArrayObjectId, 1, 0);
            GL.EnableVertexArrayAttrib(obj3d.VertexArrayObjectId, 1);
            GL.VertexArrayAttribFormat(obj3d.VertexArrayObjectId, 1, 4, VertexAttribType.Float, false, Marshal.SizeOf<Vector4>());
            
            GL.VertexArrayVertexBuffer(obj3d.VertexArrayObjectId, 0, obj3d.VertexBufferObjectId, IntPtr.Zero, 8 * sizeof(float));

            // index buffer object
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, obj3d.IndexBufferObjectId);
            GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(uint) * obj3d.Indices.Length, obj3d.Indices, BufferUsageHint.StaticDraw);
        }

        public static void Unbind3dObject(Object3d obj3d)
        {
            GL.DeleteVertexArray(obj3d.VertexArrayObjectId);
            GL.DeleteBuffer(obj3d.VertexBufferObjectId);
            GL.DeleteBuffer(obj3d.IndexBufferObjectId);
        }

        private static int CompileShader(ShaderType type, String path)
        {
            var shader = GL.CreateShader(type);
            var src = File.ReadAllText(path);
            GL.ShaderSource(shader, src);
            GL.CompileShader(shader);
            var info = GL.GetShaderInfoLog(shader);
            if (!String.IsNullOrWhiteSpace(info))
                throw new Exception($"CompileShader {type} had errors: {info}");
            return shader;
        }
        
        public static ShaderProgram CreateShaderProgram()
        {
            try
            {
                int programID = GL.CreateProgram();
                int vertexShaderId = CompileShader(ShaderType.VertexShader, "shader.vert");
                int fragmentShaderId = CompileShader(ShaderType.FragmentShader, "shader.frag");

                GL.AttachShader(programID, vertexShaderId);
                GL.AttachShader(programID, fragmentShaderId);
                GL.LinkProgram(programID);
                String debugLog = GL.GetProgramInfoLog(programID);
                if (!String.IsNullOrEmpty(debugLog))
                    Debug.WriteLine("Error: " + debugLog);

                int matrixShaderLocation = GL.GetUniformLocation(programID, "mvp");

                return new ShaderProgram {
                    ProgramId = programID,
                    Shaders = new List<int> {
                        vertexShaderId,
                        fragmentShaderId
                    },
                    MatrixShaderLocation = matrixShaderLocation
                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}