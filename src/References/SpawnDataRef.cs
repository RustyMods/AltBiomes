using System.Collections.Generic;
using UnityEngine;

namespace AltBiomes;

public class SpawnDataRef
{
    public string? m_name;
    public bool? m_enabled;
    public bool? m_devDisabled;
    public string? m_prefab;
    public Heightmap.Biome? m_biome;
    public Heightmap.BiomeArea? m_biomeArea;
    public int? m_maxSpawned;
    public float? m_spawnInterval;
    public float? m_spawnChance;
    public float? m_spawnDistance;
    public float? m_spawnRadiusMin;
    public float? m_spawnRadiusMax;
    public string? m_requiredGlobalKey;
    public List<string>? m_requiredEnvironments;
    public string? m_requiredPersistentEvent;
    public int? m_groupSizeMin;
    public int? m_groupSizeMax;
    public float? m_groupRadius;
    public bool? m_spawnAtNight;
    public bool? m_spawnAtDay;
    public float? m_minAltitude;
    public float? m_maxAltitude;
    public float? m_minTilt;
    public float? m_maxTilt;
    public bool? m_inForest;
    public bool? m_outsideForest;
    public bool? m_inLava;
    public bool? m_outsideLava;
    public bool? m_canSpawnCloseToPlayer;
    public bool? m_insidePlayerBase;
    public float? m_minOceanDepth;
    public float? m_maxOceanDepth;
    public bool? m_huntPlayer;
    public float? m_groundOffset;
    public float? m_groundOffsetRandom;
    public float? m_minDistanceFromCenter;
    public float? m_maxDistanceFromCenter;
    public int? m_maxLevel;
    public int? m_minLevel;
    public float? m_overrideLevelupChance;
    
    public static implicit operator SpawnSystem.SpawnData(SpawnDataRef data)
    {
        var result = new SpawnSystem.SpawnData
        {
            m_name = data.m_name,
            m_devDisabled = data.m_devDisabled ?? false,
            m_biome = data.m_biome ?? Heightmap.Biome.None,
            m_biomeArea = data.m_biomeArea ?? Heightmap.BiomeArea.Everything,
            m_maxSpawned = data.m_maxSpawned ?? 1,
            m_spawnInterval = data.m_spawnInterval ?? 4f,
            m_spawnChance = data.m_spawnChance ?? 100f,
            m_spawnDistance = data.m_spawnDistance ?? 10f,
            m_spawnRadiusMin = data.m_spawnRadiusMin ?? 0f,
            m_spawnRadiusMax = data.m_spawnRadiusMax ?? 0f,
            m_requiredGlobalKey = data.m_requiredGlobalKey ?? string.Empty,
            m_requiredEnvironments = data.m_requiredEnvironments ?? [],
            m_requiredPersistentEvent = data.m_requiredPersistentEvent ?? string.Empty,
            m_groupSizeMin = data.m_groupSizeMin ?? 1,
            m_groupSizeMax = data.m_groupSizeMax ?? 1,
            m_groupRadius = data.m_groupRadius ?? 3f,
            m_spawnAtNight = data.m_spawnAtNight ?? true,
            m_spawnAtDay = data.m_spawnAtDay ?? true,
            m_minAltitude = data.m_minAltitude ?? -1000f,
            m_maxAltitude = data.m_maxAltitude ?? 1000f,
            m_minTilt = data.m_minTilt ?? 0f,
            m_maxTilt = data.m_maxTilt ?? 35f,
            m_inForest = data.m_inForest ?? true,
            m_outsideForest = data.m_outsideForest ?? true,
            m_inLava = data.m_inLava ?? false,
            m_outsideLava = data.m_outsideLava ?? true,
            m_canSpawnCloseToPlayer = data.m_canSpawnCloseToPlayer ?? false,
            m_insidePlayerBase = data.m_insidePlayerBase ?? false,
            m_minOceanDepth = data.m_minOceanDepth ?? 0f,
            m_maxOceanDepth = data.m_maxOceanDepth ?? 0f,
            m_huntPlayer = data.m_huntPlayer ?? false,
            m_groundOffset = data.m_groundOffset ?? 0.5f,
            m_groundOffsetRandom = data.m_groundOffsetRandom ?? 0f,
            m_minDistanceFromCenter = data.m_minDistanceFromCenter ?? 0f,
            m_maxDistanceFromCenter = data.m_maxDistanceFromCenter ?? 0f,
            m_maxLevel = data.m_maxLevel ?? 1,
            m_minLevel = data.m_minLevel ?? 1,
            m_overrideLevelupChance = data.m_overrideLevelupChance ?? -1f,
        };

        if (string.IsNullOrEmpty(data.m_prefab)) return result;

        if (Util.TryGetPrefab(data.m_prefab, out GameObject prefab))
        {
            result.m_prefab = prefab;
            result.m_enabled = data.m_enabled ?? true;
        }
        else
        {
            AltBiomesPlugin.LogWarning($"Failed to find spawn prefab {data.m_prefab} for {data.m_name}");
        }

        return result;
    }

    public static implicit operator SpawnDataRef(SpawnSystem.SpawnData data)
    {
        var result = new SpawnDataRef
        {
            m_name = data.m_name,
            m_enabled = data.m_enabled,
            m_devDisabled = data.m_devDisabled,
            m_prefab = data.m_prefab.name,
            m_biome =  data.m_biome,
            m_biomeArea = data.m_biomeArea,
            m_maxSpawned = data.m_maxSpawned,
            m_spawnInterval = data.m_spawnInterval,
            m_spawnChance = data.m_spawnChance,
            m_spawnDistance = data.m_spawnDistance,
            m_spawnRadiusMin = data.m_spawnRadiusMin,
            m_spawnRadiusMax = data.m_spawnRadiusMax,
            m_requiredGlobalKey = data.m_requiredGlobalKey,
            m_requiredEnvironments = data.m_requiredEnvironments,
            m_requiredPersistentEvent = data.m_requiredPersistentEvent,
            m_groupSizeMin = data.m_groupSizeMin,
            m_groupSizeMax = data.m_groupSizeMax,
            m_groupRadius = data.m_groupRadius,
            m_spawnAtNight = data.m_spawnAtNight,
            m_spawnAtDay = data.m_spawnAtDay,
            m_minAltitude = data.m_minAltitude,
            m_maxAltitude = data.m_maxAltitude,
            m_minTilt = data.m_minTilt,
            m_maxTilt = data.m_maxTilt,
            m_inForest = data.m_inForest,
            m_outsideForest = data.m_outsideForest,
            m_inLava = data.m_inLava,
            m_outsideLava = data.m_outsideLava,
            m_canSpawnCloseToPlayer = data.m_canSpawnCloseToPlayer,
            m_insidePlayerBase = data.m_insidePlayerBase,
            m_minOceanDepth = data.m_minOceanDepth,
            m_maxOceanDepth = data.m_maxOceanDepth,
            m_huntPlayer = data.m_huntPlayer,
            m_groundOffset = data.m_groundOffset,
            m_groundOffsetRandom = data.m_groundOffsetRandom,
            m_minDistanceFromCenter = data.m_minDistanceFromCenter,
            m_maxDistanceFromCenter = data.m_maxDistanceFromCenter,
            m_maxLevel = data.m_maxLevel,
            m_minLevel = data.m_minLevel,
            m_overrideLevelupChance = data.m_overrideLevelupChance,
        };

        return result;
    }
}