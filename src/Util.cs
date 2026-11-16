using System.Collections.Generic;
using System.IO;
using System.Linq;
using HarmonyLib;
using SoftReferenceableAssets;
using UnityEngine;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

#nullable disable

namespace AltBiomes;

public static class Util
{
    private static readonly FieldPrefixStrippingNamingConvention NamingConvention = new ("m_");
    private static readonly ISerializer serializer = new SerializerBuilder()
        .DisableAliases()
        .WithNamingConvention(NamingConvention)
        .WithTypeConverter(InvariantCultureFloatConverter.Instance)
        .Build();

    private static readonly IDeserializer deserializer = new DeserializerBuilder()
        .IgnoreUnmatchedProperties()
        .WithNamingConvention(NamingConvention)
        .WithTypeConverter(InvariantCultureFloatConverter.Instance)
        .Build();
    private static readonly Dictionary<string, AssetID> ObjectNameToAssetID = new Dictionary<string, AssetID>();
    private static ZNetScene _scene;

    [HarmonyPatch(typeof(FejdStartup), nameof(FejdStartup.Awake))]
    private static class FejdStartup_Awake_Patch
    {
        private static void Prefix(FejdStartup __instance)
        {
            _scene = __instance.m_objectDBPrefab.GetComponent<ZNetScene>();
        }
    }

    [HarmonyPatch(typeof(AssetBundleLoader), nameof(AssetBundleLoader.OnInitCompleted))]
    private static class Cache_Asset_Name_To_AssetID
    {
        private static void Postfix(AssetBundleLoader __instance)
        {
            for (int i = 0; i < __instance.m_assetLoaders.Length; ++i)
            {
                AssetLoader loader = __instance.m_assetLoaders[i];
                var path = loader.m_assetPathInBundle;
                var filename = Path.GetFileNameWithoutExtension(path);
                ObjectNameToAssetID[filename] = loader.m_assetID;
            }
        }
    }

    public static bool TryGetPrefab(string assetName, out GameObject prefab)
    {
        if (ZNetScene.instance)
        {
            prefab = ZNetScene.instance.GetPrefab(assetName);
            return prefab != null;
        }
        prefab = _scene.m_prefabs.FirstOrDefault(p => p.name == assetName);
        return prefab != null;
    }
    
    public static bool TryGetAssetID(string assetName, out AssetID id)
    {
        if (ObjectNameToAssetID.TryGetValue(assetName, out id)) return true;
        if (AssetBundleLoader.Instance == null) return false;
        for (int i = 0; i < AssetBundleLoader.Instance.m_assetLoaders.Length; ++i)
        {
            AssetLoader loader = AssetBundleLoader.Instance.m_assetLoaders[i];
            var path = loader.m_assetPathInBundle;
            var filename = Path.GetFileNameWithoutExtension(path);
            if (filename == assetName)
            {
                ObjectNameToAssetID[filename] = id;
                id = loader.m_assetID;
                return true;
            }
        }
        return false;
    }
    
    public static T Deserialize<T>(string data)
    {
        try
        {
            return deserializer.Deserialize<T>(data);
        }
        catch (YamlException ex)
        {
            AltBiomesPlugin.LogWarning($"Line {ex.Start.Line}, Col {ex.Start.Column}: {ex.InnerException?.Message ?? ex.Message}");
            return default;
        }
    }
    
    public static string Serialize<T>(T obj, bool full = true)
    {
        if (obj == null) return "";
        using StringWriter sw = new StringWriter();
        serializer.Serialize(sw, obj, obj.GetType());
        return sw.ToString();
    }
    
}