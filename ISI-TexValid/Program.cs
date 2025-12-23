using ISI_TexValid.TextureProcessors;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            // string[] texturesDds = Directory.GetFiles(path, "*.dds");
            BitmapChecker[] texturesBitmap = Array.ConvertAll(texturesBmp, texture => new BitmapChecker(texture));
            TargaChecker[] texturesTarga = Array.ConvertAll(texturesTga, texture => new TargaChecker(texture));
            // DirectDrawChecker[] texturesDds = Array.ConvertAll(texturesDds, texture => new DirectDrawChecker(texture));

            // Process each texture based on its file extension
            List<BitmapChecker> invalidBitmaps = new List<BitmapChecker>();
            List<TargaChecker> invalidTargas = new List<TargaChecker>();
            // List<DirectDrawChecker> invalidDirectDraws = new List<DirectDrawChecker>();

            foreach (BitmapChecker texture in texturesBitmap)
            {
                if (!BitmapChecker.ValidDimensions(texture) || !BitmapChecker.ValidPixelFormat(texture))
                {
                    invalidBitmaps.Add(texture);
                }
            }
            foreach (TargaChecker texture in texturesTarga)
            {
                if (!TargaChecker.ValidDimensions(texture))
                {
                    invalidTargas.Add(texture);
                }
            }

            // Output invalid textures to output.txt
            File.WriteAllText("output.txt", "---------------------------------------------------------------------------------------------\n");
            foreach (BitmapChecker bitmap in invalidBitmaps)
            {
                File.AppendAllText("output.txt", $"Texture: {bitmap.fileName}\n");
                if (!BitmapChecker.ValidDimensions(bitmap))
                {
                    File.AppendAllText("output.txt", $"Invalid Dimensions: Width: {bitmap.width}, Height: {bitmap.height}\n");
                }
                if (!BitmapChecker.ValidPixelFormat(bitmap))
                {
                    File.AppendAllText("output.txt", $"Info: {bitmap.fileName} is {BitmapChecker.PixelFormat(bitmap)}\n");
                }
                File.AppendAllText("output.txt", "---------------------------------------------------------------------------------------------\n");
            }

            foreach (TargaChecker targa in invalidTargas)
            {
                File.AppendAllText("output.txt", $"Texture: {targa.fileName}\n");
                if (!TargaChecker.ValidDimensions(targa))
                {
                    File.AppendAllText("output.txt", $"Invalid Dimensions: Width: {targa.width}, Height: {targa.height}\n");
                }
                if ((int)targa.isCompressed == 10)
                {
                    File.AppendAllText("output.txt", $"Info: {targa.fileName} is using RLE-Compression\n");
                }
                File.AppendAllText("output.txt", "---------------------------------------------------------------------------------------------\n");
            }
        }
    }
}
