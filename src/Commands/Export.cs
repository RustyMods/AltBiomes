using System.Collections.Generic;
using System.IO;
using System.Linq;
using BepInEx;

namespace AltBiomes;

public static partial class Commands
{
    private static void Export(Terminal.ConsoleEventArgs args)
    {
        var type = args.GetString(2);
        var option = args.GetStringFrom(3);
        var dir = Path.Combine(Paths.ConfigPath, AltBiomesPlugin.ModName);
        Directory.CreateDirectory(dir);
        var folder = Path.Combine(dir, "Export");
        Directory.CreateDirectory(folder);
        switch (type)
        {
            case "AltBiome":
                if (option == "all")
                {
                    for (int i = 0; i < AltBiomeList.m_altBiomes.Count; ++i)
                    {
                        var alt =  AltBiomeList.m_altBiomes[i];
                        var reference = (AltBiomeRef)alt;
                        var yaml = Util.Serialize(reference);
                        var path = Path.Combine(folder, "AltBiome_" + alt.m_name + ".yml");
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
                        var path = Path.Combine(folder, "AltBiome_" + alt.m_name + ".yml");
                        File.WriteAllText(path, yaml);
                        args.Context.LogInfo($"Exported {alt.m_name} Alt Biome: " + path.RemoveRootPath());
                    }
                    else
                    {
                        args.Context.LogWarning("Failed to find Alt Biome: " + option);
                    }
                }
                break;
            case "Location":
                if (option == "all")
                {
                    for (int i = 0; i < ZoneSystem.instance.m_locations.Count; ++i)
                    {
                        var loc = ZoneSystem.instance.m_locations[i];
                        var reference = (ZoneLocationRef)loc;
                        var yaml = Util.Serialize(reference);
                        var path = Path.Combine(folder, "Location_" + loc.m_name + ".yml");
                        File.WriteAllText(path, yaml);
                        args.Context.LogInfo($"Exported {loc.m_name} Location: " + path.RemoveRootPath());
                    }
                }
                else
                {
                    var loc = ZoneSystem.instance.m_locations.FirstOrDefault(l => l.m_name == option);
                    if (loc != null)
                    {
                        var reference = (ZoneLocationRef)loc;
                        var yaml = Util.Serialize(reference);
                        var path = Path.Combine(folder, "Location_" + loc.m_name + ".yml");
                        File.WriteAllText(path, yaml);
                        args.Context.LogInfo($"Exported {loc.m_name} Location: " + path.RemoveRootPath());
                    }
                    else
                    {
                        args.Context.LogWarning("Failed to find Location: " + option);
                    }
                }
                break;
            case "Vegetation":
                if (option == "all")
                {
                    for (int i = 0; i < ZoneSystem.instance.m_vegetation.Count; ++i)
                    {
                        var veg = ZoneSystem.instance.m_vegetation[i];
                        var reference = (ZoneVegetationRef)veg;
                        var yaml = Util.Serialize(reference);
                        var path = Path.Combine(folder, "Vegetation_" + veg.m_name + ".yml");
                        File.WriteAllText(path, yaml);
                        args.Context.LogInfo($"Exported {veg.m_name} Vegetation: " + path.RemoveRootPath());
                    }
                }
                else
                {
                    var veg = ZoneSystem.instance.m_vegetation.FirstOrDefault(l => l.m_name == option);
                    if (veg != null)
                    {
                        var reference = (ZoneVegetationRef)veg;
                        var yaml = Util.Serialize(reference);
                        var path = Path.Combine(folder, "Vegetation_" + veg.m_name + ".yml");
                        File.WriteAllText(path, yaml);
                        args.Context.LogInfo($"Exported {veg.m_name} Vegetation: " + path.RemoveRootPath());
                    }
                    else
                    {
                        args.Context.LogWarning("Failed to find Vegetation: " + option);
                    }
                }
                break;
            default:
                args.Context.LogWarning("Invalid arguments");
                break;
        }
        

    }

    private static List<string> GetExportOptions(int index, string word)
    {
        switch (index)
        {
            case 2:
                return ["AltBiome", "Location", "Vegetation"];
            case 3:
                return word switch
                {
                    "AltBiome" => AltBiomeList.m_altBiomes.Select(b => b.m_name).Union(["all"]).ToList(),
                    "Location" => ZoneSystem.instance.m_locations.Select(l => l.m_name).Union(["all"]).ToList(),
                    "Vegetation" => ZoneSystem.instance.m_vegetation.Select(v => v.m_name).Union(["all"]).ToList(),
                    _ => []
                };
            default:
                return ["export"];
        }
    }

    private static string GetExportDescriptions(string[] strArray, string word)
    {
        return word switch
        {
            "AltBiome" => "Export Alt Biome YML",
            "Location" => "Export Location YML",
            "Vegetation" => "Export Vegetation YML",
            _ => "Export YML file"
        };
    }
}