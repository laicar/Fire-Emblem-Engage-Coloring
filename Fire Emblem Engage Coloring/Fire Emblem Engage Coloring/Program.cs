//C# Program to apply Fire Emblem Engage palette files to corresponding sprites by u/eclogia

namespace Fire_Emblem_Engage_Coloring;
public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length != 3)
        {
            Console.WriteLine("Please provide the following folders' path as arguments in order: raw (red scale) sprites, palettes, output");
            return;
        }
        EngageColorer.ProcessFolders(args[0], args[1], args[2]);
        Console.WriteLine("Finished.");
    }
}