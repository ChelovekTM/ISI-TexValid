using ISI_TexValid.TextureProcessors;
using System;
using System.Collections.Generic;
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
            string[] texturesBmp = Directory.GetFiles(path, "*.bmp");
            string[] texturesTga = Directory.GetFiles(path, "*.tga");
            string[] texturesDds = Directory.GetFiles(path, "*.dds");

            // Load textures
            TextureChecker[] texturesBitmap = Array.ConvertAll(texturesBmp, texture => new BitmapChecker(texture));
            TextureChecker[] texturesTarga = Array.ConvertAll(texturesTga, texture => new TargaChecker(texture));
            TextureChecker[] texturesDirectDraw = Array.ConvertAll(texturesDds, texture => new DirectDrawChecker(texture));

            // Process invalid textures
            List<TextureChecker> invalidTextures = new List<TextureChecker>();
            ProcessTextures(texturesBitmap, invalidTextures);
            ProcessTextures(texturesTarga, invalidTextures);
            ProcessTextures(texturesDirectDraw, invalidTextures);

            // Order invalid textures by error level so that the more severe errors appear first in the output
            List<TextureChecker> orderedTextures = invalidTextures.OrderByDescending(texture => texture.errorLevel).ToList();

            // Output invalid textures to output.txt
            File.WriteAllText("output.txt", "---------------------------------------------------------------------------------------------\n");
            foreach (TextureChecker texture in orderedTextures)
            {
                File.AppendAllText("output.txt", $"Texture: {texture.fileName}\n");
                if (!TextureChecker.ValidDimensions(texture))
                {
                    File.AppendAllText("output.txt", $"Invalid Dimensions: Width: {texture.width}, Height: {texture.height}\n");
                }
                if (!TextureChecker.FileNameLength(texture))
                {
                    File.AppendAllText("output.txt", $"Warning: {texture.fileName} file name may be too long\n");
                }
                if (texture is BitmapChecker bitmap && !BitmapChecker.ValidPixelFormat(bitmap))
                {
                    File.AppendAllText("output.txt", $"Info: {bitmap.fileName} is {BitmapChecker.PixelFormat(bitmap)}\n");
                }
                if (texture is TargaChecker targa && !TargaChecker.Uncompressed(targa))
                {
                    File.AppendAllText("output.txt", $"Info: {targa.fileName} is {TargaChecker.CompressionType(targa)}\n");
                }
                if (texture is DirectDrawChecker directDraw1 && !DirectDrawChecker.HasMipMaps(directDraw1))
                {
                    File.AppendAllText("output.txt", $"Info: {directDraw1.fileName} has no mip-maps\n");
                }
                if (texture is DirectDrawChecker directDraw2 && !DirectDrawChecker.IsCompressed(directDraw2))
                {
                     File.AppendAllText("output.txt", $"Info: {directDraw2.fileName} is uncompressed\n");
                }
                File.AppendAllText("output.txt", "---------------------------------------------------------------------------------------------\n");
            }
        }

        /// <summary>
        /// Checks each texture for validity and adds invalid ones to a list
        /// </summary>
        /// <param name="textures">The input array contsaining textures</param>
        /// <param name="invalidTextures">The list to add invalid textures to</param>
        static void ProcessTextures(TextureChecker[] textures, List<TextureChecker> invalidTextures)
        {
            foreach (TextureChecker texture in textures)
            {
                if (!TextureChecker.ValidDimensions(texture) | !TextureChecker.FileNameLength(texture) |
                    (texture is BitmapChecker bitmap && !BitmapChecker.ValidPixelFormat(bitmap)) | 
                    (texture is TargaChecker targa && !TargaChecker.Uncompressed(targa)) |
                    (texture is DirectDrawChecker directDraw && (!DirectDrawChecker.HasMipMaps(directDraw)) | 
                    (!DirectDrawChecker.IsCompressed(directDraw))))
                {
                    invalidTextures.Add(texture);
                }
            }
        }
    }
}
