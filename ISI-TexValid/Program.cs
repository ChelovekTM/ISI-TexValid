using System;
using System.IO;

namespace ISI_TexValid
{
    internal class Program
    {
        static void Main(string[] _)
        {
            string path = Environment.CurrentDirectory;
            Console.WriteLine($"Path: {path}");
            var textures = Directory.EnumerateFiles(path);
            foreach (var texture in textures)
            {
                if (texture.EndsWith(".BMP"))
                {
                    Console.WriteLine($"File: {texture}");
                }
            }
        }
    }
}
