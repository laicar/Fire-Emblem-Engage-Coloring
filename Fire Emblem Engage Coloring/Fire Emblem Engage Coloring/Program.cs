//C# Program to apply Fire Emblem Engage palette files to corresponding sprites by u/eclogia

using static Fire_Emblem_Engage_Coloring.EngageColorer;

if(Environment.GetCommandLineArgs().Length != 4) {
    Console.WriteLine("Please provide the following folders' path as arguments in order: raw (red scale) sprites, palettes, output");
    return;
}
ProcessFolders(Environment.GetCommandLineArgs()[1], Environment.GetCommandLineArgs()[2], Environment.GetCommandLineArgs()[3]);