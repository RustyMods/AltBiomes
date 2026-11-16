using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BepInEx;
using HarmonyLib;

namespace AltBiomes;

public static class AltManager
{
    private static readonly Dictionary<string, AltBiomeRef> AltBiomes = [];

    [HarmonyPatch(typeof(ZoneSystem), nameof(ZoneSystem.Awake))]
    private static class ZoneSystem_Awake_Patch
    {
        private static void Prefix()
        {
            AltBiomeList.m_altBiomes.Clear();
        }
    }
    
    [HarmonyPatch(typeof(ZoneSystem), nameof(ZoneSystem.Start))]
    private static class ZoneSystem_Start_Patch
    {
        private static void Prefix(ZoneSystem __instance)
        {
            ReadFiles();
        }
    }
    
    [HarmonyPatch(typeof(AltBiomeWorldData), nameof(AltBiomeWorldData.GenerateAltBiomes))]
    private static class AltBiomeWorldData_GenerateAltBiomes_Patch
    {
        private static void Prefix(AltBiomeWorldData __instance)
        {
            Load();
        }
    }
    
    private static void Load()
    {
        StringBuilder sb = new StringBuilder();
        foreach (KeyValuePair<string, AltBiomeRef> kvp in AltBiomes)
        {
            sb.Clear();
            AltBiome alt = kvp.Value;

            int i = AltBiomeList.m_altBiomes.FindIndex(b => b.m_name == kvp.Key);
            if (i != -1)
            {
                sb.Append("Replaced ");
                AltBiomeList.m_altBiomes[i] = alt;
            }
            else
            {
                sb.Append("Added ");
                AltBiomeList.m_altBiomes.Add(alt);
            }

            sb.Append($"Alt Biome: {kvp.Key}");
            AltBiomesPlugin.LogDebug(sb.ToString());
        }
    }
    
    public static void ReceiveAltBiomeFiles(string yml)
    {
        try
        {
            var files = Util.Deserialize<List<AltBiomeRef>>(yml);
            AltBiomes.Clear();
            for (int i = 0; i < files.Count; ++i)
            {
                AltBiomeRef? file = files[i];
                if (file.m_name == null || string.IsNullOrEmpty(file.m_name)) continue;
                AltBiomes[file.m_name] = file;
            }
            AltBiomesPlugin.LogDebug($"Received {files.Count} Alt Biome files from server");
        }
        catch
        {
            AltBiomesPlugin.LogWarning("Failed to deserialize server Alt Biome files");
        }
    }

    public static string Serialize()
    {
        var files = AltBiomes.Values.ToList();
        var yml = Util.Serialize(files);
        return yml;
    }
    
    public static void ReadFiles()
    {
        AltBiomes.Clear();
        var dirPath = Path.Combine(Paths.ConfigPath, AltBiomesPlugin.ModName);
        Directory.CreateDirectory(dirPath);
        var paths = Directory.GetFiles(dirPath, "*.yml",  SearchOption.AllDirectories);
        for (int i = 0; i < paths.Length; ++i)
        {
            var path = paths[i];
            var raw = File.ReadAllText(path);
            try
            {
                AltBiomeRef alt = Util.Deserialize<AltBiomeRef>(raw);
                if (alt.m_name == null || string.IsNullOrEmpty(alt.m_name))
                {
                    AltBiomesPlugin.LogWarning(Path.GetFileName(path) + 
                                               " missing field name value, skipping.");
                    continue;
                }
                AltBiomes[alt.m_name] = alt;
                
                AltBiomesPlugin.LogDebug($"Loaded {AltBiomes.Count} Alt Biome files");
            }
            catch
            {
                AltBiomesPlugin.LogWarning("Failed to deserialize: " + Path.GetFileName(path));
            }
        }

        if (AltBiomes.Count == 0)
        {
            AltBiomesPlugin.LogDebug("No Alt Biome files found");
        }
    }
}