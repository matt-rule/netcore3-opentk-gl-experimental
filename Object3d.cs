using OpenTK;

namespace netcore3_opentk_gl_experimental
{
    public struct Object3d
    {
        public Vector2 Position;
        public Vertex3d[] Vertices;
        public uint[] Indices;
        public int VertexArrayObjectId;
        public int VertexBufferObjectId;
        public int IndexBufferObjectId;
    }
}
