using SoftReferenceableAssets;

namespace AltBiomes;

public static partial class Commands
{
    private static void Reload(Terminal.ConsoleEventArgs args)
    {
        if (!ZoneSystem.instance || 
            WorldGenerator.instance == null || 
            WorldGenerator.instance.m_world == null || 
            WorldGenerator.instance.m_world.m_biomeData == null) return;

        foreach (var data in AltManager.AltBiomes.Values)
        {
            if (data.m_addLocations != null)
            {
                ZoneSystem.instance.m_locations
                    .RemoveAll(l => data.m_addLocations.Contains(l));
            }

            if (data.m_addVegetation != null)
            {
                ZoneSystem.instance.m_vegetation
                    .RemoveAll(v  => data.m_addVegetation.Contains(v));
            }
        }
        
        AltManager.ReadFiles();
        AltManager.Load();

        var referenceHolder = ZoneSystem.instance.GetComponent<ReferenceHolder>();
        bool holdReference = referenceHolder != null && Settings.AssetMemoryUsagePolicy.HasFlag(AssetMemoryUsagePolicy.KeepAsynchronousLoadedBit);
        foreach (var altBiome in AltBiomeList.m_altBiomes)
        {
            foreach (var l in altBiome.m_addLocations)
            {
                ZoneSystem.instance.m_locations.Add(l);
                l.AltBiomeParent = altBiome.m_name;
                if (holdReference && l.m_enable)
                {
                    l.m_prefab.Load();
                    referenceHolder?.HoldReferenceTo(l.m_prefab);
                    l.m_prefab.Release();
                }
            }

            foreach (var v in altBiome.m_addVegetation)
            {
                ZoneSystem.instance.m_vegetation.Add(v);
                v.AltBiomeParent = altBiome.m_name;
            }
        }
        
        WorldGenerator.instance.m_world.m_biomeData.GenerateAltBiomes();
    }    
}