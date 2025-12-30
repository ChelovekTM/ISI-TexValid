using TGASharpLib; // Written by Olexandr Zelenskyi: https://github.com/ALEXGREENALEX/TGASharpLib.git

namespace TextureProcessors
{
    internal class TargaChecker : TextureChecker
    {
        public TgaImageType isCompressed;

        public TargaChecker(string texture) : base(texture)
        {
            TGA targa = new TGA(texture);
            width = targa.Width;
            height = targa.Height;
            isCompressed = targa.Header.ImageType;
        }

        /// <summary>
        /// Checks whether the Targa file is compressed or uncompressed
        /// </summary>
        /// <param name="targa">The Targa texture</param>
        /// <returns>True if the texture compressed, otherwise false</returns>
        public static bool Uncompressed(TargaChecker targa)
        {
            if ((int)targa.isCompressed == 2)
            {
                return true;
            }

            targa.errorLevel += 1;
            return false;
        }

        /// <summary>
        /// Gives a string indicating whether the Targa file is compressed or uncompressed
        /// </summary>
        /// <param name="targa">The Targa texture</param>
        /// <returns>A string containing whether the file is compressed or uncompressed</returns>
        public static string CompressionType(TargaChecker targa)
        {
            if ((int)targa.isCompressed == 10)
            {
                return "compressed";
            }
            else
            {
                return "uncompressed";
            }
        }
    }
}
