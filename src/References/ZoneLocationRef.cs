using SoftReferenceableAssets;
using UnityEngine;

namespace AltBiomes;

public class ZoneLocationRef
{
    public string? m_name;
    public bool? m_enable;
    public string? m_prefabName;
    // public string? m_prefab;
    public Heightmap.Biome? m_biome;
    public Heightmap.BiomeArea? m_biomeArea;
    public int? m_quantity;
    public bool? m_prioritized;
    public bool? m_centerFirst;
    public bool? m_unique;
    public string? m_group;
    public float? m_minDistanceFromSimilar;
    public string? m_groupMax;
    public float? m_maxDistanceFromSimilar;
    public bool? m_iconAlways;
    public bool? m_iconPlaced;
    public bool? m_randomRotation;
    public bool? m_slopeRotation;
    public bool? m_snapToWater;
    public float? m_interiorRadius;
    public bool? m_clearArea;
    public float? m_minTerrainDelta;
    public float? m_maxTerrainDelta;
    public float? m_minimumVegetation;
    public float? m_maximumVegetation;
    public bool? m_surroundCheckVegetation;
    public float? m_surroundCheckDistance;
    public int? m_surroundCheckLayers;
    public bool? m_inForest;
    public float? m_forestThresholdMin;
    public float? m_forestThresholdMax;
    public float? m_minDistanceFromCenter;
    public float? m_maxDistanceFromCenter;
    public float? m_minDistance;
    public float? m_maxDistance;
    public float? m_minAltitude;
    public float? m_maxAltitude;
    
    public static implicit operator ZoneSystem.ZoneLocation(ZoneLocationRef data)
    {
        var result = new ZoneSystem.ZoneLocation
        {
            m_name = data.m_name,
            m_enable = false,
            m_prefabName = data.m_prefabName ?? "",
            m_biome =  data.m_biome ?? Heightmap.Biome.None,
            m_biomeArea = data.m_biomeArea ?? Heightmap.BiomeArea.Everything,
            m_quantity =  data.m_quantity ?? 0,
            m_prioritized =  data.m_prioritized ?? false,
            m_centerFirst =  data.m_centerFirst ?? false,
            m_unique =  data.m_unique ?? false,
            m_group =  data.m_group ?? "",
            m_minDistanceFromSimilar =  data.m_minDistanceFromSimilar ?? 0,
            m_groupMax =   data.m_groupMax ?? "",
            m_maxDistanceFromSimilar =  data.m_maxDistanceFromSimilar ?? 0,
            m_iconAlways =   data.m_iconAlways ?? false,
            m_iconPlaced =  data.m_iconPlaced ?? false,
            m_randomRotation =  data.m_randomRotation ?? true,
            m_slopeRotation =  data.m_slopeRotation ?? false,
            m_snapToWater = data.m_snapToWater ?? false,
            m_interiorRadius =   data.m_interiorRadius ?? 0,
            m_clearArea =  data.m_clearArea ?? false,
            m_minTerrainDelta =  data.m_minTerrainDelta ?? 0,
            m_maxTerrainDelta =  data.m_maxTerrainDelta ?? 2f,
            m_minimumVegetation =  data.m_minimumVegetation ?? 0,
            m_maximumVegetation =  data.m_maximumVegetation ?? 1f,
            m_surroundCheckVegetation = data.m_surroundCheckVegetation ?? false,
            m_surroundCheckDistance =   data.m_surroundCheckDistance ?? 20f,
            m_surroundCheckLayers = data.m_surroundCheckLayers ?? 2,
            m_inForest =  data.m_inForest ?? false,
            m_forestTresholdMin =  data.m_forestThresholdMin ?? 0,
            m_forestTresholdMax =  data.m_forestThresholdMax ?? 1f,
            m_minDistanceFromCenter =  data.m_minDistanceFromCenter ?? 0,
            m_maxDistanceFromCenter =  data.m_maxDistanceFromCenter ?? 0,
            m_minDistance =   data.m_minDistance ?? 0,
            m_maxDistance =   data.m_maxDistance ?? 0,
            m_minAltitude =  data.m_minAltitude ?? -1000f,
            m_maxAltitude =  data.m_maxAltitude ?? 1000f,
        };
    
        if (string.IsNullOrEmpty(data.m_prefabName)) return result;
    
        if (Util.TryGetAssetID(data.m_prefabName, out var assetID))
        {
            result.m_prefab = new SoftReference<GameObject>(assetID);
            result.m_enable = data.m_enable ?? true;
        }
        else
        {
            AltBiomesPlugin.LogWarning($"Failed to find AssetID for {data.m_prefabName} for {data.m_name}");
        }
        
        return result;
    }

    public static implicit operator ZoneLocationRef(ZoneSystem.ZoneLocation location)
    {
        var result = new ZoneLocationRef
        {
            m_name = location.m_name,
            m_enable = location.m_enable,
            m_prefabName = location.m_prefabName,
            m_biome = location.m_biome,
            m_biomeArea = location.m_biomeArea,
            m_quantity = location.m_quantity,
            m_prioritized = location.m_prioritized,
            m_centerFirst = location.m_centerFirst,
            m_unique = location.m_unique,
            m_group = location.m_group,
            m_minDistanceFromSimilar = location.m_minDistanceFromSimilar,
            m_groupMax = location.m_groupMax,
            m_maxDistanceFromSimilar = location.m_maxDistanceFromSimilar,
            m_iconAlways = location.m_iconAlways,
            m_iconPlaced = location.m_iconPlaced,
            m_randomRotation = location.m_randomRotation,
            m_slopeRotation = location.m_slopeRotation,
            m_snapToWater = location.m_snapToWater,
            m_interiorRadius = location.m_interiorRadius,
            m_clearArea = location.m_clearArea,
            m_minTerrainDelta = location.m_minTerrainDelta,
            m_maxTerrainDelta = location.m_maxTerrainDelta,
            m_minimumVegetation = location.m_minimumVegetation,
            m_maximumVegetation = location.m_maximumVegetation,
            m_surroundCheckVegetation = location.m_surroundCheckVegetation,
            m_surroundCheckDistance = location.m_surroundCheckDistance, 
            m_surroundCheckLayers = location.m_surroundCheckLayers,
            m_inForest = location.m_inForest,
            m_forestThresholdMin = location.m_forestTresholdMin,
            m_forestThresholdMax = location.m_forestTresholdMax,
            m_minDistanceFromCenter = location.m_minDistanceFromCenter,
            m_maxDistanceFromCenter = location.m_maxDistanceFromCenter,
            m_minDistance = location.m_minDistance,
            m_maxDistance = location.m_maxDistance,
            m_minAltitude = location.m_minAltitude,
            m_maxAltitude = location.m_maxAltitude,
        };

        if (AssetBundleLoader.Instance.m_assetIDToLoaderIndex.ContainsKey(location.m_prefab.m_assetID))
        {
            // location.m_prefab.Load();
            // if (location.m_prefab.IsValid)
            // {
            //     result.m_prefabName = location.m_prefab.Asset.name;
            // }
            // location.m_prefab.Release();
        }
        else
        {
            AltBiomesPlugin.LogWarning($"Failed to find Asset Loader for {location.m_prefabName}, might not work");
        }
        
        return result;
    }
}

// [Warning: AltBiomes] Failed to find Asset Loader for Greydwarf_camp2
// [Warning: AltBiomes] Failed to find Asset Loader for Greydwarf_camp3
// [Warning: AltBiomes] Failed to find Asset Loader for Castle
// [Warning: AltBiomes] Failed to find Asset Loader for Fort1
// [Warning: AltBiomes] Failed to find Asset Loader for xmastree
// [Warning: AltBiomes] Failed to find Asset Loader for Pillar1
// [Warning: AltBiomes] Failed to find Asset Loader for Pillar2
// [Warning: AltBiomes] Failed to find Asset Loader for StoneHouse1
// [Warning: AltBiomes] Failed to find Asset Loader for StoneHouse2
// [Warning: AltBiomes] Failed to find Asset Loader for StoneHouse5
// [Warning: AltBiomes] Failed to find Asset Loader for GoblinCamp1
// [Warning: AltBiomes] Failed to find Asset Loader for StoneTower2
// [Warning: AltBiomes] Failed to find Asset Loader for StoneTower4
// [Warning: AltBiomes] Failed to find Asset Loader for TrollCave
// [Warning: AltBiomes] Failed to find Asset Loader for SunkenCrypt1
// [Warning: AltBiomes] Failed to find Asset Loader for SunkenCrypt2
// [Warning: AltBiomes] Failed to find Asset Loader for SunkenCrypt3
// [Warning: AltBiomes] Failed to find Asset Loader for MountainCave01
// [Warning: AltBiomes] Failed to find Asset Loader for Hugintest