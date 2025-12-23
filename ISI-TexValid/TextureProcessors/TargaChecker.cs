using System;
using System.IO;
using TGASharpLib;

namespace ISI_TexValid.TextureProcessors
{
    internal class TargaChecker
    {
        public string fileName;
        public int width;
        public int height;

        public TargaChecker(string texture)
        {
            TGA targa = new TGA(texture);
            fileName = Path.GetFileName(texture).ToLower();
            width = targa.Width;
            height = targa.Height;
        }

        public static bool ValidDimensions(TargaChecker targa)
        {
            bool foundWidth = false;
            bool foundHeight = false;

            for (int i = 2; i <= 11; i++)
            {
                if (Math.Pow(2, i) == targa.width)
                {
                    foundWidth = true;
                }
                if (Math.Pow(2, i) == targa.height)
                {
                    foundHeight = true;
                }
            }

            if (foundWidth && foundHeight)
            {
                return true;
            }
            return false;
        }
    }
}
