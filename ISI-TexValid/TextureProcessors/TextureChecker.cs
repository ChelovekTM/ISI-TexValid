using System;
using System.IO;

namespace TextureProcessors
{
    internal class TextureChecker
    {
        public string fileName;
        public int width;
        public int height;
        public int errorLevel;

        public TextureChecker(string texture)
        {
            fileName = Path.GetFileName(texture).ToLower();
            errorLevel = 0;
        }

        /// <summary>
        /// Checks if the texture has valid dimensions. Valid dimensions are powers of two between 4 and 2048 (inclusive).
        /// </summary>
        /// <remarks>Note that powers of two above 2048 are considered invalid as trust me man you do not need THAT many pixels.</remarks>
        /// <param name="texture">The texture to be validated</param>
        /// <returns>True if the texture is valid, false if it has incorrect dimensions</returns>
        public static bool ValidDimensions(TextureChecker texture)
        {
            bool foundWidth = false;
            bool foundHeight = false;

            for (int i = 2; i <= 11; i++)
            {
                if (Math.Pow(2, i) == texture.width)
                {
                    foundWidth = true;
                }
                if (Math.Pow(2, i) == texture.height)
                {
                    foundHeight = true;
                }
            }

            if (foundWidth && foundHeight)
            {
                return true;
            }

            texture.errorLevel += 3;
            return false;
        }

        /// <summary>
        /// Checks whether the file name of the specified texture is 28 characters or fewer
        /// </summary>
        /// <param name="texture">The texture file</param>
        /// <returns>True if the file name length is less than or equal to 28 characters, otherwise false</returns>
        public static bool FileNameLength(TextureChecker texture)
        {
            if (texture.fileName.Length <= 28)
            {
               return true;
            }

            texture.errorLevel += 2;
            return false;
        }
    }
}