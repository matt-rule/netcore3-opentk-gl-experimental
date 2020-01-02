using System;
using OpenTK;

namespace netcore3_opentk_gl_experimental
{
    class Program
    {
        public const int FRAMES_PER_SECOND = 60;

        static void Main(string[] args)
        {
            new MainWindow().Run(FRAMES_PER_SECOND);
        }
    }
}
