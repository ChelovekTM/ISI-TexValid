using ISI_TexValid.TextureProcessors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace ISI_TexValid
{
    internal class Program
    {
        static void Main(string[] _)
        {
            // Get the current directory and load all files
            string path = Environment.CurrentDirectory;
            Console.WriteLine(path);
            string[] texturesBmp = Directory.GetFiles(path,"*.bmp");
            BitmapChecker[] textures = Array.ConvertAll(texturesBmp, texture => new BitmapChecker(texture));

            // Process each texture based on its file extension
            List<BitmapChecker> invalidTextures = new List<BitmapChecker>();
            foreach (BitmapChecker texture in textures)
            {       
                if (texture.fileName.EndsWith(".bmp"))
                {
                    if (!BitmapChecker.Valid(texture))
                    {
                        invalidTextures.Add(texture);
                    }
                }
                //else if (texture.ToLower().EndsWith(".tga"))
                {
                    // Process Targa file
                }
                //else if (texture.ToLower().EndsWith(".dds"))
                {
                    // Process DirectDraw Surface file
                }
            }

            foreach (BitmapChecker bitmap in invalidTextures)
            {
                Console.WriteLine($"{bitmap.fileName}, {bitmap.width}, {bitmap.height}, {bitmap.pixelFormat}");
            }
        }
    }
}
