using OpenTK;

namespace OpenGL4Demo
{
    public struct Object3d
    {
        public Vector2 Position;
        public Vertex3d[] Vertices;
        public uint[] Indices;
        public int VAO;
        public int VBO;
        public int IBO;
    }
}
