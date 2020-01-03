using OpenTK;

namespace netcore3_opentk_gl_experimental
{
    public struct Object3d
    {
        public Matrix4 ModelMatrix;
        public Vertex3d[] Vertices;
        public uint[] Indices;
        public int VertexArrayObjectId;
        public int VertexBufferObjectId;
        public int IndexBufferObjectId;
    }
}
