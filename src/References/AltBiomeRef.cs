using System.Collections.Generic;
using System.Linq;

namespace AltBiomes;

public class AltBiomeRef
{
    public string? m_name;
    public bool? m_enabled;
    public Heightmap.Biome? m_biome;
    public string? m_namePrefix;
    public string? m_nameSuffix;
    public string? m_nameOverride;
    public float? m_levelUpChanceMultiplier;
    public string? m_forceMusic;
    public string? m_forceEnvironment;
    public List<EnvEntryRef>? m_addEnvironments;
    public List<string>? m_blockEnvironments;
    public List<SpawnDataRef>? m_spawn;
    public List<string>? m_blockSpawnNames;
    public List<ZoneVegetationRef>? m_addVegetation;
    public List<ZoneLocationRef>? m_addLocations;
    public List<string>? m_blockLocationNames;
    public Heightmap.Biome? m_terrainTextureOverride;
    public float? m_baseHeightMultiplier;
    public float? m_baseHeightOffset;
    public float? m_heightMapMultiplier;
    public float? m_heightMapOffset;
    public Heightmap.Biome? m_heightMapBiomeOverride;
    public Heightmap.HeightmapExtras? m_customGenerator;
    public float? m_minDistanceFromCenter;
    public int? m_minAmountSpawned;
    public int? m_maxAmountSpawned;
    public float? m_chance;
    public Heightmap.Biome? m_requireNeighbor;
    public Heightmap.Biome? m_notNeighbor;
    public List<string>? m_incompatibleAltBiomes;
    public int? m_minEdgeSize;
    public int? m_maxEdgeSize;
    public float? m_minAvgHeight;
    public float? m_maxAvgHeight;
    public float? m_belowWorldX;
    public float? m_aboveWorldX;
    public float? m_belowWorldY;
    public float? m_aboveWorldY;
    // public List<BiomeSector>? Sectors; // this gets added in alt biome generator when valid 

    public static implicit operator AltBiome(AltBiomeRef data)
    {
        var result = new AltBiome
        {
            m_name = data.m_name ?? "",
            m_enabled = data.m_enabled ?? true,
            m_biome = data.m_biome ?? Heightmap.Biome.None,
            m_namePrefix =  data.m_namePrefix ?? "",
            m_nameSuffix =  data.m_nameSuffix ?? "",
            m_nameOverride =  data.m_nameOverride ?? "",
            m_levelUpChanceMultiplier = data.m_levelUpChanceMultiplier ?? 0f,
            m_forceMusic =  data.m_forceMusic ?? "",
            m_forceEnvironment =  data.m_forceEnvironment ?? "",
            m_addEnvironments = [],
            m_blockEnvironments = data.m_blockEnvironments ?? [],
            m_spawn = [],
            m_blockSpawnNames = data.m_blockSpawnNames ?? [],
            m_addVegetation = [],
            m_addLocations = [],
            m_blockLocationNames =  data.m_blockLocationNames ?? [],
            m_terrainTextureOverride = data.m_terrainTextureOverride ?? Heightmap.Biome.None,
            m_baseHeightMultiplier = data.m_baseHeightMultiplier ?? 0f,
            m_baseHeightOffset = data.m_baseHeightOffset ?? 0f,
            m_heightMapMultiplier = data.m_heightMapMultiplier ?? 0f,
            m_heightMapOffset = data.m_heightMapOffset ?? 0f,
            m_heightMapBiomeOverride =  data.m_heightMapBiomeOverride ?? Heightmap.Biome.None,
            m_customGenerator = data.m_customGenerator ?? Heightmap.HeightmapExtras.None,
            m_minDistanceFromCenter = data.m_minDistanceFromCenter ?? 0f,
            m_minAmountSpawned = data.m_minAmountSpawned ?? 0,
            m_maxAmountSpawned = data.m_maxAmountSpawned ?? 0,
            m_chance = data.m_chance ?? 0f,
            m_requireNeighbor = data.m_requireNeighbor ?? Heightmap.Biome.None,
            m_notNeighbor = data.m_notNeighbor ?? Heightmap.Biome.None,
            m_incompatibleAltBiomes = data.m_incompatibleAltBiomes ?? [],
            m_minEdgeSize = data.m_minEdgeSize ?? 0,
            m_maxEdgeSize = data.m_maxEdgeSize ?? 0,
            m_minAvgHeight = data.m_minAvgHeight ?? 0f,
            m_maxAvgHeight = data.m_maxAvgHeight ?? 0f,
            m_belowWorldX = data.m_belowWorldX ?? 0f,
            m_aboveWorldX = data.m_aboveWorldX ?? 0f,
            m_belowWorldY = data.m_belowWorldY ?? 0f,
            m_aboveWorldY = data.m_aboveWorldY ?? 0f,
        };

        if (data.m_addEnvironments != null)
        {
            result.m_addEnvironments = data.m_addEnvironments
                .Select(e => (EnvEntry)e)
                .ToList();
        }
        if (data.m_spawn != null)
        {
            result.m_spawn = data.m_spawn
                .Select(s => (SpawnSystem.SpawnData)s)
                .ToList();
        }

        if (data.m_addVegetation != null)
        {
            result.m_addVegetation = data.m_addVegetation
                .Select(v => (ZoneSystem.ZoneVegetation)v)
                .ToList();
        }

        if (data.m_addLocations != null)
        {
            result.m_addLocations = data.m_addLocations
                .Select(l => (ZoneSystem.ZoneLocation)l)
                .ToList();
        }
        
        
        return result;
    }

    public static implicit operator AltBiomeRef(AltBiome data)
    {
        var result = new AltBiomeRef
        {
            m_name = data.m_name,
            m_enabled = data.m_enabled,
            m_biome = data.m_biome,
            m_namePrefix = data.m_namePrefix,
            m_nameSuffix = data.m_nameSuffix,
            m_nameOverride = data.m_nameOverride,
            m_levelUpChanceMultiplier = data.m_levelUpChanceMultiplier,
            m_forceMusic = data.m_forceMusic,
            m_forceEnvironment = data.m_forceEnvironment,
            m_addEnvironments = data.m_addEnvironments.Select(e => (EnvEntryRef)e).ToList(),
            m_blockEnvironments = data.m_blockEnvironments,
            m_spawn = data.m_spawn.Select(s => (SpawnDataRef)s).ToList(),
            m_blockSpawnNames = data.m_blockSpawnNames,
            m_addVegetation = data.m_addVegetation.Select(v => (ZoneVegetationRef)v).ToList(),
            m_addLocations = data.m_addLocations.Select(l => (ZoneLocationRef)l).ToList(),
            m_blockLocationNames = data.m_blockLocationNames,
            m_terrainTextureOverride = data.m_terrainTextureOverride,
            m_baseHeightMultiplier = data.m_baseHeightMultiplier,
            m_baseHeightOffset = data.m_baseHeightOffset,
            m_heightMapMultiplier = data.m_heightMapMultiplier,
            m_heightMapOffset = data.m_heightMapOffset,
            m_heightMapBiomeOverride = data.m_heightMapBiomeOverride,
            m_customGenerator = data.m_customGenerator,
            m_minDistanceFromCenter = data.m_minDistanceFromCenter,
            m_minAmountSpawned = data.m_minAmountSpawned,
            m_maxAmountSpawned = data.m_maxAmountSpawned,
            m_chance = data.m_chance,
            m_requireNeighbor = data.m_requireNeighbor,
            m_notNeighbor = data.m_notNeighbor,
            m_incompatibleAltBiomes =  data.m_incompatibleAltBiomes,
            m_minEdgeSize = data.m_minEdgeSize,
            m_maxEdgeSize = data.m_maxEdgeSize,
            m_minAvgHeight = data.m_minAvgHeight,
            m_maxAvgHeight = data.m_maxAvgHeight,
            m_belowWorldX = data.m_belowWorldX,
            m_aboveWorldX = data.m_aboveWorldX,
            m_belowWorldY = data.m_belowWorldY,
            m_aboveWorldY =  data.m_aboveWorldY,
        };
        
        return result;
    }
}