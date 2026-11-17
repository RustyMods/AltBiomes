using System.Collections.Generic;
using System.IO;
using System.Linq;
using BepInEx;

namespace AltBiomes;

public static partial class Commands
{
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