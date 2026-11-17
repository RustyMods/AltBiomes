using BepInEx;

namespace AltBiomes;

public static partial class Commands
{
    private const string HEX_Gray = "#B2BEB5";
    private const string HEX_SoftBlue = "#60A5FA";
    private const string HEX_Green = "#34D399";
    private const string HEX_Amber = "#FBBF24";
    private const string HEX_Red = "#F87171";

    public static void LogInfo(this Terminal terminal, string msg) => terminal.Log(HEX_Green, msg);
    public static void LogWarning(this Terminal terminal, string msg) => terminal.Log(HEX_Amber, msg);
    public static void LogError(this Terminal terminal, string msg) => terminal.Log(HEX_Red, msg);
    public static void LogDebug(this Terminal terminal, string msg) => terminal.Log(HEX_SoftBlue, msg);
    public static void Log(this Terminal terminal, string hex, string msg) => terminal.AddString($"<color={hex}>{msg}</color>");

    public static string RemoveRootPath(this string filepath) =>
        filepath.Replace(Paths.ConfigPath, @"...\BepInEx\config\");
    
    public static void Init()
    {
        _ = new Command("export",
            $"<color={HEX_Gray}>[name]</color>: Export Alt Biome YML file",
            Export,
            GetExportOptions,
            descriptions: GetExportDescriptions);

        _ = new Command(
            "find", 
            $"<color={HEX_Gray}>[name]</color>: Find Alt Biome by name", 
            FindAltBiome, 
            optionsFetcher: GetFindAltBiomeOptions,
            adminOnly:true);
    }
}