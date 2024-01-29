//C# Program to apply Fire Emblem Engage palette files to corresponding sprites by u/eclogia

namespace Fire_Emblem_Engage_Coloring;
public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Please provide the following folders' path as arguments in order: raw (red scale) sprites, palettes, output");
            Console.WriteLine("You may specify if you'd like the sprite names to be output in the console by adding -v as fourth argument.");
            return;
        }
        Console.WriteLine("Starting.");
        if (args.Length >= 4 && args[3] == "-v")
            EngageColorer.ProcessFolders(args[0], args[1], args[2], true);
        else
            EngageColorer.ProcessFolders(args[0], args[1], args[2], false);
        Console.WriteLine("Finished.");
    }
}