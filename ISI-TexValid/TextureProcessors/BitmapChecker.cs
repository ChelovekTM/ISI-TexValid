using System;
using System.IO;
using System.Drawing;

namespace ISI_TexValid.TextureProcessors
{
    internal class BitmapChecker
    {
        public string fileName;
        public int width;
        public int height;
        public string pixelFormat;

        public BitmapChecker(string texture)
        {
            Bitmap bitmap = new Bitmap(texture);
            fileName = Path.GetFileName(texture).ToLower();
            width = bitmap.Width;
            height = bitmap.Height;
            pixelFormat = bitmap.PixelFormat.ToString();
        }

        public static bool Valid(BitmapChecker bitmap)
        {
            bool foundWidth = false;
            bool foundHeight = false;

            for (int i = 2; i <= 11; i++)
            {
                if (Math.Pow(2,i) == bitmap.width)
                {
                    foundWidth = true;
                }
                if (Math.Pow(2,i) == bitmap.height)
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
