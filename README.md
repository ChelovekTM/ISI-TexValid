# ISI-TexValid

**ISI-TexValid** is a small utility designed for **modding legacy ISI engine games**, that validates texture files and reports common issues that may cause crashes, graphical glitches, or performance problems.

The tool scans all supported texture files in its directory, checks their dimensions, and outputs a detailed report to an `output.txt` file.

## REQUIRES .NET FRAMEWORK 4.8.1
---

## Features

- Detects **non–power-of-two texture dimensions**
- Flags **very large textures** (above 2048×2048) that may cause performance issues
- Provides additional file-specific information:
  - **BMP**: pixel format
  - **TGA**: whether the texture is compressed
  - **DDS**: mip-map count and whether the texture is uncompressed
- Warns if a **filename may be too long** for the engine
- Outputs results to a simple, readable `output.txt` file

---

## Supported Formats

- `.bmp`*
- `.tga`
- `.dds`
NOTE: Apparently I accidentally added support for .png and .jpg files that have .bmp extensions. In this case your files may get flagged incorrectly (but then again, you are using a different file extentsion,)

---

## How to Use

1. Download the latest release from the **Releases** page.
2. Copy `ISI-TexValid.exe` into the folder containing your textures.
3. **Double-click the executable**.
4. Open the generated `output.txt` file with any text editor (e.g. Notepad).

No command-line arguments or setup required.

---

## `output.txt` Data

The output file contains **three types of entries** for flagged textures:

### Invalid Dimensions
- The texture dimensions are **not powers of two**
- These textures will **most likely crash the game**
- Textures larger than **2048×2048** are also flagged, as they can cause heavy performance drops on such an old engine

### Warning
- Indicates that the **filename might be too long**
- If the game crashes without a clear error, check these textures and the materials using them

### Info
- Additional information that usually won’t crash the game
- Useful when diagnosing **unexpected texture behavior**

---

## Contributing

Contributions, bug reports, and feature suggestions are welcome.  
Feel free to open an issue or submit a pull request. (Or shoot a dm on discord via @chelovektm)

---

## License

MIT License.
