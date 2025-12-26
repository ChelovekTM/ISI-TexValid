using System;
using System.Drawing;

namespace ISI_TexValid.TextureProcessors
{
    internal class BitmapChecker : TextureChecker
    {
        public string pixelFormat;

        public BitmapChecker(string texture) : base(texture)
        {
            Bitmap bitmap = new Bitmap(texture);
            width = bitmap.Width;
            height = bitmap.Height;
            pixelFormat = bitmap.PixelFormat.ToString();
        }

        /// <summary>
        /// Checks whether the bitmap is 24-bit RGB format
        /// </summary>
        /// <param name="bitmap">The bitmap texture</param>
        /// <returns>Returs true if it is, false otherwise</returns>
        public static bool ValidPixelFormat(BitmapChecker bitmap)
        {
            if (bitmap.pixelFormat == "Format24bppRgb")
            {
                return true;
            }

            bitmap.errorLevel += 1;
            return false;
        }

        /// <summary>
        /// Gives a string describing the Pixel Format of the bitmap
        /// </summary>
        /// <param name="bitmap">The bitmap texture</param>
        /// <returns>A string containing info of the Pixel Format</returns>
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
