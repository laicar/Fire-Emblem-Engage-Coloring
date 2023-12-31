using SkiaSharp;

namespace Fire_Emblem_Engage_Coloring
{
    internal static class EngageColorer
    {
        //Read the palette image file to learn the palette's colors.
        //Colors may have an alpha component.
        private static SKColor[] ReadPaletteFile(string paletteFilePath)
        {
            SKBitmap paletteImage = SKBitmap.Decode(paletteFilePath);
            SKColor[] palette = new SKColor[256];
            for (int i = 0; i < 256; i++)
            {
                palette[i] = paletteImage.GetPixel((i * 2), 0);
            }
            return palette;
        }

        //Read the "raw" red-scale sprite and apply a palette to it using the value of the red channel as index. 
        private static SKBitmap RecolorSprite(SKColor[] palette, string rawSpritePath)
        {
            SKBitmap rawSpriteImage = SKBitmap.Decode(rawSpritePath);
            for (int x = 0; x < rawSpriteImage.Width; x++)
            {
                for (int y = 0; y < rawSpriteImage.Height; y++)
                {
                    int paletteIndex = rawSpriteImage.GetPixel(x, y).Red;
                    rawSpriteImage.SetPixel(x, y, palette[paletteIndex]);
                }
            }
            return rawSpriteImage;
        }

        //Go through the given folders and match palette files with "raw" sprite files.
        //Call EngageColorer.RecolorSprite() then output the result in a file of the same name as "raw" sprite, but in specified output folder.
        public static void ProcessFolders(string rawSpriteFolderPath, string paletteFolderPath, string spriteOutputFolderPath)
        {
            if (!Directory.Exists(paletteFolderPath))
            {
                Console.WriteLine("{0} is not a valid file or directory.", paletteFolderPath);
                return;
            }
            if (!Directory.Exists(rawSpriteFolderPath))
            {
                Console.WriteLine("{0} is not a valid file or directory.", rawSpriteFolderPath);
                return;
            }
            if (!Directory.Exists(spriteOutputFolderPath))
            {
                Console.WriteLine("Creating directory {0}.", spriteOutputFolderPath);
                Directory.CreateDirectory(spriteOutputFolderPath);
            }
            string[] paletteFilePaths;
            paletteFilePaths = Directory.GetFiles(paletteFolderPath);
            foreach (string palettePath in paletteFilePaths)
            {
                SKColor[] unitPalette = EngageColorer.ReadPaletteFile(palettePath);
                string unitId = Path.GetFileNameWithoutExtension(palettePath);
                string[] unitRawSpriteFilePaths;
                unitRawSpriteFilePaths = Directory.GetFiles(rawSpriteFolderPath).Where(f => Path.GetFileName(f).StartsWith(unitId + "_")).ToArray();
                foreach (string rawSpritePath in unitRawSpriteFilePaths)
                {
                    SKBitmap coloredSprite = EngageColorer.RecolorSprite(unitPalette, rawSpritePath);
                    string outputPath = spriteOutputFolderPath + Path.DirectorySeparatorChar + Path.GetFileName(rawSpritePath);
                    SKFileWStream fs = new(outputPath);
                    coloredSprite.Encode(fs, SKEncodedImageFormat.Png, quality: 100);
                }
            }
        }
    }
}
