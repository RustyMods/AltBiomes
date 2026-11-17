using System.Collections.Generic;
using System.Linq;
using YamlDotNet.Serialization;

namespace AltBiomes;

[YamlSerializable]
public class AltBiomeRef
{
    [YamlMember(Description = "Unique Identifier")]
    public string? m_name;
    public bool? m_enabled;
    [YamlMember(Description = "Allowed biomes, multiple separated by comma")]
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
    [YamlMember(Description = "If empty, will use default Biome vegetation, else will override")]
    public List<ZoneVegetationRef>? m_addVegetation;
    [YamlMember(Description = "If empty, will use default biome locations, else will override")]
    public List<ZoneLocationRef>? m_addLocations;
    [YamlMember(Description = "List of locations blocked from spawning in Alt Biome")]
    public List<string>? m_blockLocationNames;
    [YamlMember(Description = "Override terrain color")]
    public Heightmap.Biome? m_terrainTextureOverride;
    [YamlMember(Description = "Override clutter grass")]
    public Heightmap.Biome? m_clutterOverride;
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

    [YamlIgnore] 
    private AltBiome? AltBiome;

    public static implicit operator AltBiome(AltBiomeRef data)
    {
        if (data.AltBiome != null)
        {
            return data.AltBiome;
        }
        
        var result = new ExtendedAltBiome
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
            m_clutterOverride = data.m_clutterOverride ?? Heightmap.Biome.None,
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
            result.m_addEnvironments
                .AddRange(data.m_addEnvironments
                .Select(e => (EnvEntry)e));
        }
        if (data.m_spawn != null)
        {
            result.m_spawn
                .AddRange(data.m_spawn
                .Select(s => (SpawnSystem.SpawnData)s));
        }

        if (data.m_addVegetation != null)
        {
            result.m_addVegetation
                .AddRange(data.m_addVegetation
                .Select(v => (ZoneSystem.ZoneVegetation)v));
        }

        if (data.m_addLocations != null)
        {
            result.m_addLocations
                .AddRange(data.m_addLocations
                .Select(l => (ZoneSystem.ZoneLocation)l));
        }

        data.AltBiome = result;
        
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
            m_clutterOverride = data is ExtendedAltBiome e ? e.m_clutterOverride : Heightmap.Biome.None,
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