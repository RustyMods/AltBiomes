using HarmonyLib;
using UnityEngine;

namespace AltBiomes;

public static class Clutter
{
    public static Heightmap.Biome FindClutterOverride(Vector3 point)
    {
        if (WorldGenerator.instance == null) return Heightmap.Biome.None;
        var sector = WorldGenerator.instance.GetBiomeSector(point);
        foreach (var altBiome in sector.AltBiomes)
        {
            if (altBiome is not ExtendedAltBiome { m_clutterOverride: { } biomeClutter }) continue;
            if (biomeClutter == Heightmap.Biome.None) continue;
            return biomeClutter;
        }

        return Heightmap.Biome.None;
    }
    
    [HarmonyPatch(typeof(Heightmap), nameof(Heightmap.FindBiomeClutter))]
    public static class Heightmap_FindBiomeClutter_Patch
    {
        private static void Postfix(Vector3 point, ref Heightmap.Biome __result)
        {
            var clutterOverride = FindClutterOverride(point);
            if (clutterOverride != Heightmap.Biome.None)
            {
                __result = clutterOverride;
            }
        }
    }

    [HarmonyPatch(typeof(ClutterSystem), nameof(ClutterSystem.GetGroundInfo))]
    public static class ClutterSystem_GetGroundInfo_Patch
    {
        private static void Postfix(ref Vector3 point, ref Heightmap.Biome biome)
        {
            var clutterOverride = FindClutterOverride(point);
            if (clutterOverride != Heightmap.Biome.None)
            {
                biome = clutterOverride;
            }
        }
    }
}