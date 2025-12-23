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
            string[] texturesBmp = Directory.GetFiles(path, "*.bmp");
            string[] texturesTga = Directory.GetFiles(path, "*.tga");
            string[] texturesDds = Directory.GetFiles(path, "*.dds");
            BitmapChecker[] texturesBitmap = Array.ConvertAll(texturesBmp, texture => new BitmapChecker(texture));
            // TargaChecker[] texturesTarga = Array.ConvertAll(texturesTga, texture => new TargaChecker(texture));
            // DirectDrawChecker[] texturesDds = Array.ConvertAll(texturesDds, texture => new DirectDrawChecker(texture));

            // Process each texture based on its file extension
            List<BitmapChecker> invalidBitmaps = new List<BitmapChecker>();
            // List<TargaChecker> invalidTargas = new List<TargaChecker>();
            // List<DirectDrawChecker> invalidDirectDraws = new List<DirectDrawChecker>();

            foreach (BitmapChecker texture in texturesBitmap)
            {
                if (!BitmapChecker.ValidDimensions(texture) || !BitmapChecker.ValidPixelFormat(texture))
                {
                    invalidBitmaps.Add(texture);
                }
            }

            // Output invalid textures to output.txt
            File.WriteAllText("output.txt", "---------------------------------------------------------------------------------------------\n");
            foreach (BitmapChecker bitmap in invalidBitmaps)
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
