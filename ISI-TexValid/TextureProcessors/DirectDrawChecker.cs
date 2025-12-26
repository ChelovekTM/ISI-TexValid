using DDSParser; // Written by jmschrack: https://github.com/jmschrack/DDS-Image-Wrapper.git

namespace ISI_TexValid.TextureProcessors
{
    internal class DirectDrawChecker : TextureChecker
    {
        public int mipMapCount;
        public string compressionType;

        public DirectDrawChecker(string texture) : base(texture)
        {
            byte[] rawDDS = System.IO.File.ReadAllBytes(texture);
            DDSImage DDS = new DDSImage(rawDDS);
            width = DDS.Width;
            height = DDS.Height;
            mipMapCount = DDS.MipMapCount;

            if (DDS.HasFourCC)
            {
                compressionType = DDS.GetPixelFormatFourCC().ToString();
            }
            else
            {
                compressionType = "Uncompressed RGB";
            }
        }

        /// <summary>
        /// Checks whether the DirectDraw texture has 2 or more mip-maps
        /// </summary>
        /// <remarks>DirectDraw textures always have at least 1 mip-map, which is the original texture.</remarks>
        /// <param name="directDraw">The DirectDraw texture</param>
        /// <returns>True if it has 2 or more mip-maps, false if it doesn't</returns>
        public static bool HasMipMaps(DirectDrawChecker directDraw)
        {
            if (directDraw.mipMapCount > 1)
            {
                return true;
            }

            directDraw.errorLevel += 1;
            return false;
        }

        /// <summary>
        /// Checks whether the DirectDraw texture is compressed or uncompressed
        /// </summary>
        /// <param name="directDraw">The DirectDraw texture</param>
        /// <returns>True if it's compressed, false if it's not</returns>
        public static bool IsCompressed(DirectDrawChecker directDraw)
        {
            if (directDraw.compressionType != "Uncompressed RGB")
            {
                return true;
            }
            directDraw.errorLevel += 1;
            return false;
        }
    }
}
