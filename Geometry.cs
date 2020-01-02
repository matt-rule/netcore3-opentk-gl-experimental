using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;

namespace netcore3_opentk_gl_experimental
{
    public struct BufferData
    {
        public Vertex3d[] Vertices;
        public uint[] Indices;
    }

    public class Geometry
    {
        public static BufferData GetTriangle1()
        {
            Color4 col = new Color4(0, 191, 0, 127);

            return new BufferData {
                Vertices = new Vertex3d[] {
                    new Vertex3d(new Vector4( 0.0f, 0.0f, 0.0f, 1.0f), col),
                    new Vertex3d(new Vector4( 100.0f, 0.0f, 0.0f, 1.0f), col),
                    new Vertex3d(new Vector4( 100.0f, 100.0f, 0.0f, 1.0f), col)
                },
                Indices = new uint[] {
                    0, 1, 2
                }
            };
        }

        public static BufferData GetTriangle2()
        {
            Color4 col = new Color4(191, 0, 0, 127);

            return new BufferData {
                Vertices = new Vertex3d[] {
                    new Vertex3d(new Vector4( 0.0f, 0.0f, 0.0f, 1.0f), col),
                    new Vertex3d(new Vector4( 100.0f, 0.0f, 0.0f, 1.0f), col),
                    new Vertex3d(new Vector4( 0.0f, 100.0f, 0.0f, 1.0f), col),
                },
                Indices = new uint[] {
                    0, 1, 2
                }
            };
        }
    }
}
