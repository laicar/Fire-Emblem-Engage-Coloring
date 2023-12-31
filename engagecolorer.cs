//C# Program to apply Fire Emblem Engage palette files to corresponding sprites by u/eclogia
//Doesn't actually compile as-is. This is more of a draft right now.

namespace Engage_Coloring;

using System.IO;
using System.Drawing;
using System.Text.RegularExpressions;

public static class EngageColorer {

    //Read the palette image file to learn the palette's colors.
    //Colors may have an alpha component.
    private static Color[] ReadPaletteFile(string paletteFilePath) {
        Color[] palette = new Color[256];
        Bitmap paletteImage = new Bitmap(paletteFilePath);
        for (int i = 0; i < 512; i ++) {
            palette[i] = paletteImage.GetPixel(i*2,0);
        }
        return palette;
    }

    //Read the "raw" red-scale sprite and apply a palette to it using the value of the red channel as index. 
    private static Bitmap RecolorSprite(Color[] palette, string rawSpritePath) {
        Bitmap rawspriteImage = new Bitmap(rawSpritePath);
        for (int x = 0; x < rawspriteImage.Width; x++) {
            for (int y = 0; y < rawspriteImage.Height; y++) {
                int paletteIndex = rawspriteImage.GetPixel(x, y).R;
                rawspriteImage.setPixel(x, y, palette[paletteIndex]);
            }
        }
        return rawspriteImage;
    }

    //Go through the given folders and match palette files with "raw" sprite files.
    //Call EngageColorer.RecolorSprite() then output the result in a file of the same name as "raw" sprite, but in specified output folder.
    public static void ProcessFolders(string rawSpriteFolderPath, string paletteFolderPath, string spriteOutputFolderPath) {
        if (!Directory.Exists(paletteFolderPath)) {
            Console.WriteLine("{0} is not a valid file or directory.", paletteFolderPath);
            return;
        }
        if (!Directory.Exists(rawSpriteFolderPath)) {
            Console.WriteLine("{0} is not a valid file or directory.", rawSpriteFolderPath);
            return;
        }
        if (!Directory.Exists(spriteOutputFolderPath)) {
            Console.WriteLine("Creating directory {0}.", rawSpriteFolderPath);
            Directory.CreateDirectory(rawSpriteFolderPath);
            return;
        }
        string[] paletteFilePaths;
        paletteFilePaths = Directory.GetFiles(paletteFolderPath);
        foreach (string palettePath in paletteFilePaths){
            Color[] unitPalette = EngageColorer.ReadPaletteFile(palettePath);
            string unitId = Path.GetFileNameWithoutExtension(palettePath);
            string[] unitRawSpriteFilePaths;
            string regexPattern = "^" + unitId + "_";
            unitRawSpriteFilePaths = Directory.GetFiles(rawSpriteFolderPath).Where(filePath => Regex.IsMatch(filePath, regexPattern));
            foreach (string rawSpritePath in unitRawSpriteFilePaths) {
                Bitmap coloredSprite = EngageColorer.RecolorSprite(unitPalette, rawSpritePath);
                string outputFilePath = spriteOutputFolderPath + Path.DirectorySeparatorChar + rawSpritePath;
                coloredSprite.Save(outputFilePath, ImageFormat.Png);
            }
        }
    }
}

class Program
{
    static void Main(string[] args) {
    if(args.Length != 3) {
        Console.WriteLine("Please provide the following folders' path as arguments in order: raw (red scale) sprites, palettes, output");
        return;
    }
    EngageColorer.ProcessFolders(args[1], args[2], args[3]);
    }
}
