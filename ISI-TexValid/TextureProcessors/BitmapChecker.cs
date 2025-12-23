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

        /// <summary>
        /// Checks if the bitmap has valid dimensions. Valid dimensions are powers of two between 4 and 2048 (inclusive).
        /// </summary>
        /// <remarks>Note that powers of two above 2048 are considered invalid as trust me man you do not need THAT many pixels.</remarks>
        /// <param name="bitmap">The texture to be validated</param>
        /// <returns>True if the texture is valid, false if it has incorrect dimensions</returns>
        public static bool ValidDimensions(BitmapChecker bitmap)
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

        public static bool ValidPixelFormat(BitmapChecker bitmap)
        {
            if (bitmap.pixelFormat == "Format24bppRgb")
            {
                return true;
            }
            return false;
        }

        public static string PixelFormat(BitmapChecker bitmap)
        {
            if (bitmap.pixelFormat.Contains("Format8"))
            {
                return "an 8-bit bitmap";
            }
            else if (bitmap.pixelFormat.Contains("Format16"))
            {
                return "a 16-bit bitmap";
            }
            else if (bitmap.pixelFormat.Contains("Format24"))
            {
                return "a 24-bit bitmap";
            }
            else if (bitmap.pixelFormat.Contains("Format32"))
            {
                return "a 32-bit bitmap";
            }
            else if (bitmap.pixelFormat.Contains("Format48"))
            {
                return "a 48-bit bitmap";
            }
            else if (bitmap.pixelFormat.Contains("Format64"))
            {
                return "a 64-bit bitmap";
            }
            return "an unknown format bitmap";
        }
    }
}
