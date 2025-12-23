using ISI_TexValid.TextureProcessors;
using System;
using System.Collections.Generic;
using System.IO;

namespace ISI_TexValid
{
    internal class Program
    {
        static void Main(string[] _)
        {
            // Get the current directory and load all files
            string path = Environment.CurrentDirectory;
            string[] texturesBmp = Directory.GetFiles(path,"*.bmp");
            BitmapChecker[] textures = Array.ConvertAll(texturesBmp, texture => new BitmapChecker(texture));

            // Process each texture based on its file extension
            List<BitmapChecker> invalidTextures = new List<BitmapChecker>();
            foreach (BitmapChecker texture in textures)
            {       
                if (texture.fileName.EndsWith(".bmp"))
                {
                    if (!BitmapChecker.ValidDimensions(texture) || !BitmapChecker.ValidPixelFormat(texture))
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

            // Output invalid textures to output.txt
            File.WriteAllText("output.txt", "---------------------------------------------------------------------------------------------\n");
            foreach (BitmapChecker bitmap in invalidTextures)
            {
                if (!BitmapChecker.ValidDimensions(bitmap))
                {
                    File.AppendAllText("output.txt", $"Invalid Texture: {bitmap.fileName}, Width: {bitmap.width}, Height: {bitmap.height}\n");
                }
                if (!BitmapChecker.ValidPixelFormat(bitmap))
                {
                    File.AppendAllText("output.txt", $"Incorrect Format: {bitmap.fileName}, {bitmap.pixelFormat}\n");
                }
                File.AppendAllText("output.txt", "---------------------------------------------------------------------------------------------\n");
            }
        }
    }
}
