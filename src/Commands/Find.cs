using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AltBiomes;

public static partial class Commands
{
    private static void FindAltBiome(Terminal.ConsoleEventArgs args)
    {
        foreach (var pin in args.Context.m_findPins)
        {
            Minimap.instance.RemovePin(pin);
        }
        args.Context.m_findPins.Clear();
            
        var option = args.GetStringFrom(2);
        var match = AltBiomeList.m_altBiomes.FirstOrDefault(b => b.m_name == option);
        if (match == null) return;

        for (int i = 0; i < match.Sectors.Count; ++i)
        {
            BiomeSector sector =  match.Sectors[i];
                
            var pin = Minimap.instance.AddPin(
                new Vector3(sector.Center.x, 0f, sector.Center.y),
                Minimap.PinType.Hildir1, 
                match.m_name, 
                false, 
                false);
            args.Context.m_findPins.Add(pin);
        }
        
        args.Context.LogInfo($"Found {match.Sectors.Count} {option} sectors");
    }

    private static List<string> GetFindAltBiomeOptions(int index, string word)
    {
        switch (index)
        {
            case 2:
                return AltBiomeList.m_altBiomes.Select(b => b.m_name).ToList();
            default:
                return [];
        }
    }
}