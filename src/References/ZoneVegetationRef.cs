using UnityEngine;

namespace AltBiomes;

public class ZoneVegetationRef
{
    public string? m_name;
    public string? m_prefab;
    public bool? m_enable;
    public float? m_min;
    public float? m_max;
    public bool? m_forcePlacement;
    public float? m_scaleMin;
    public float? m_scaleMax;
    public float? m_randTilt;
    public float? m_chanceToUseGroundTilt;
    public Heightmap.Biome? m_biome;
    public Heightmap.BiomeArea? m_biomeArea;
    public bool? m_blockCheck;
    public bool? m_snapToStaticSolid;
    public float? m_minAltitude;
    public float? m_maxAltitude;
    public float? m_minVegetation;
    public float? m_maxVegetation;
    public bool? m_surroundCheckVegetation;
    public float? m_surroundCheckDistance;
    public int? m_surroundCheckLayers;
    public float? m_surroundBetterThanAverage;
    public float? m_minOceanDepth;
    public float? m_maxOceanDepth;
    public float? m_minTilt;
    public float? m_maxTilt;
    public float? m_terrainDeltaRadius;
    public float? m_maxTerrainDelta;
    public float? m_minTerrainDelta;
    public bool? m_snapToWater;
    public float? m_groundOffset;
    public int? m_groundSizeMin;
    public int? m_groupSizeMax;
    public float? m_groupRadius;
    public float? m_minDistanceFromCenter;
    public float? m_maxDistanceFromCenter;
    public bool? m_inForest;
    public float? m_forestThresholdMin;
    public float? m_forestThresholdMax;
    
    public static implicit operator ZoneSystem.ZoneVegetation(ZoneVegetationRef data)
    {
        var result = new ZoneSystem.ZoneVegetation
        {
            m_name = data.m_name ?? "veg",
            m_enable = false,
            m_min = data.m_min ?? 0f,
            m_max = data.m_max ?? 10f,
            m_forcePlacement = data.m_forcePlacement ?? false,
            m_scaleMin = data.m_scaleMin ?? 1f,
            m_scaleMax = data.m_scaleMax ?? 1f,
            m_randTilt = data.m_randTilt ?? 0f,
            m_chanceToUseGroundTilt =  data.m_chanceToUseGroundTilt ?? 0f,
            m_biome = data.m_biome ?? Heightmap.Biome.None,
            m_biomeArea = data.m_biomeArea ?? Heightmap.BiomeArea.Everything,
            m_blockCheck = data.m_blockCheck ?? true,
            m_snapToStaticSolid =  data.m_snapToStaticSolid ?? false,
            m_minAltitude = data.m_minAltitude ?? -1000f,
            m_maxAltitude = data.m_maxAltitude ?? 1000f,
            m_minVegetation = data.m_minVegetation ?? 0f,
            m_maxVegetation = data.m_maxVegetation ?? 0f,
            m_surroundCheckVegetation =  data.m_surroundCheckVegetation ?? false,
            m_surroundCheckDistance = data.m_surroundCheckDistance ?? 20f,
            m_surroundCheckLayers =  data.m_surroundCheckLayers ?? 2,
            m_surroundBetterThanAverage =  data.m_surroundBetterThanAverage ?? 0f,
            m_minOceanDepth = data.m_minOceanDepth ?? 0f,
            m_maxOceanDepth = data.m_maxOceanDepth ?? 0f,
            m_minTilt = data.m_minTilt ?? 0f,
            m_maxTilt = data.m_maxTilt ?? 90f,
            m_terrainDeltaRadius = data.m_terrainDeltaRadius ?? 0f,
            m_maxTerrainDelta = data.m_maxTerrainDelta ?? 2f,
            m_minTerrainDelta = data.m_minTerrainDelta ?? 0f,
            m_snapToWater =   data.m_snapToWater ?? false,
            m_groundOffset =  data.m_groundOffset ?? 0f,
            m_groupSizeMin = data.m_groundSizeMin ?? 1,
            m_groupSizeMax = data.m_groupSizeMax ?? 1,
            m_groupRadius = data.m_groupRadius ?? 0f,
            m_minDistanceFromCenter = data.m_minDistanceFromCenter ?? 0f,
            m_maxDistanceFromCenter = data.m_maxDistanceFromCenter ?? 0f,
            m_inForest =  data.m_inForest ?? false,
            m_forestTresholdMin = data.m_forestThresholdMin ?? 0f,
            m_forestTresholdMax = data.m_forestThresholdMax ?? 1f,
        };
        if (data.m_prefab == null || string.IsNullOrEmpty(data.m_prefab))
        {
            AltBiomesPlugin.LogWarning($"{result.m_name} prefab is null, skipping");
            return result;
        }
        
        if (!PrefabManager.TryGetPrefab(data.m_prefab, out GameObject prefab))
        {
            AltBiomesPlugin.LogWarning($"Failed to find {data.m_prefab} for {result.m_name} Zone Vegetation");
            return result;
        }
        result.m_prefab = prefab;
        result.m_enable = data.m_enable ?? true;
        return result;
    }

    public static implicit operator ZoneVegetationRef(ZoneSystem.ZoneVegetation data)
    {
        return new ZoneVegetationRef
        {
            m_name = data.m_name,
            m_prefab = data.m_prefab.name,
            m_enable = data.m_enable,
            m_min = data.m_min,
            m_max = data.m_max,
            m_forcePlacement = data.m_forcePlacement,
            m_scaleMin = data.m_scaleMin,
            m_scaleMax = data.m_scaleMax,
            m_randTilt = data.m_randTilt,
            m_chanceToUseGroundTilt =  data.m_chanceToUseGroundTilt,
            m_biome = data.m_biome,
            m_biomeArea = data.m_biomeArea,
            m_blockCheck = data.m_blockCheck,
            m_snapToStaticSolid =  data.m_snapToStaticSolid,
            m_minAltitude = data.m_minAltitude,
            m_maxAltitude = data.m_maxAltitude,
            m_minVegetation = data.m_minVegetation,
            m_maxVegetation = data.m_maxVegetation,
            m_surroundCheckVegetation =  data.m_surroundCheckVegetation,
            m_surroundCheckDistance = data.m_surroundCheckDistance,
            m_surroundCheckLayers = data.m_surroundCheckLayers,
            m_surroundBetterThanAverage = data.m_surroundBetterThanAverage,
            m_minOceanDepth = data.m_minOceanDepth,
            m_maxOceanDepth = data.m_maxOceanDepth,
            m_minTilt = data.m_minTilt,
            m_maxTilt = data.m_maxTilt,
            m_terrainDeltaRadius = data.m_terrainDeltaRadius,
            m_maxTerrainDelta = data.m_maxTerrainDelta,
            m_minTerrainDelta = data.m_minTerrainDelta,
            m_snapToWater = data.m_snapToWater,
            m_groundOffset =   data.m_groundOffset,
            m_groundSizeMin = data.m_groupSizeMin,
            m_groupSizeMax = data.m_groupSizeMax,
            m_groupRadius = data.m_groupRadius,
            m_minDistanceFromCenter = data.m_minDistanceFromCenter,
            m_maxDistanceFromCenter = data.m_maxDistanceFromCenter,
            m_inForest = data.m_inForest,
            m_forestThresholdMin = data.m_forestTresholdMin,
            m_forestThresholdMax = data.m_forestTresholdMax,
        };
    }
}