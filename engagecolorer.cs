//C# Program by u/eclogia

using System.IO;
using System.Drawing;
using System.Text.RegularExpressions;

public static class EngageColorer {

    private static Color[] ReadPaletteFile(string paletteFilePath) {
        Color[] palette = new Color[256];
        Bitmap img = new Bitmap(paletteFilePath);
        for (int i = 0; i < 512; i ++) {
            palette[i] = img.GetPixel(i*2,0);
        }
        return palette;
    }

    private static Bitmap ColorSprite(Color[] palette) {
//TODO
    }

    private static void OutputSpriteFile(Bitmap sprite) {
//TODO
    }

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
            Color[] palette = this.readPaletteFile(palettePath);
            string unitId = Path.GetFileNameWithoutExtension(palettePath);
            string[] unitRawSpriteFilePaths;
            string regexPattern = "^" + unitId + "_";
            unitRawSpriteFilePaths = Directory.GetFiles(rawSpriteFolderPath).Where(filePath => Regex.IsMatch(filePath, regexPattern));
//TODO
            
        }
    }
}

public static void Main (string[] args) {
    if(args.Length != 3) {
        Console.WriteLine("Please provide the following folders' path as arguments in order: raw (red scale) sprites, palettes, output");
        return;
    }
    EngageColorer.ProcessFolders(args[0], args[1], args[2]);
}