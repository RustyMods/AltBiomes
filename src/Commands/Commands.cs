using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AltBiomes;
using BepInEx;
using BepInEx.Bootstrap;

namespace MonsterDB;

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
    }

    private static void Export(Terminal.ConsoleEventArgs args)
    {
        var option = args.GetStringFrom(2);
        if (option == "all")
        {
            for (int i = 0; i < AltBiomeList.m_altBiomes.Count; ++i)
            {
                var alt =  AltBiomeList.m_altBiomes[i];
                var reference = (AltBiomeRef)alt;
                var yaml = Util.Serialize(reference);
                var dir = Path.Combine(Paths.ConfigPath, AltBiomesPlugin.ModName);
                Directory.CreateDirectory(dir);
                var path = Path.Combine(dir, alt.m_name + ".yml");
                File.WriteAllText(path, yaml);
                args.Context.LogInfo($"Exported {alt.m_name} Alt Biome: " + path.RemoveRootPath());
            }
        }
        else
        {
            var alt = AltBiomeList.m_altBiomes.FirstOrDefault(b => b.m_name == option);
            if (alt != null)
            {
                var reference = (AltBiomeRef)alt;
                var yaml = Util.Serialize(reference);
                var dir = Path.Combine(Paths.ConfigPath, AltBiomesPlugin.ModName);
                Directory.CreateDirectory(dir);
                var path = Path.Combine(dir, alt.m_name + ".yml");
                File.WriteAllText(path, yaml);
                args.Context.LogInfo($"Exported {alt.m_name} Alt Biome: " + path.RemoveRootPath());
            }
        }
    }

    private static List<string> GetExportOptions(int index, string word)
    {
        switch (index)
        {
            case 2:
                return AltBiomeList.m_altBiomes.Select(b => b.m_name).Union(["all"]).ToList();
            default:
                return ["export"];
        }
    }

    private static string GetExportDescriptions(string[] strArray, string word)
    {
        return "Export Alt Biome file";
    }
}