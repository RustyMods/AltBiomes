using System.Collections.Generic;
using System.IO;
using BepInEx;
using HarmonyLib;
using SoftReferenceableAssets;
using YamlDotNet.Serialization;

namespace AltBiomes;

public static class SoftReferenceHelper
{
    private static readonly Dictionary<string, AssetID> ObjectNameToAssetID = new Dictionary<string, AssetID>();
    
    public static bool TryGetAssetID(string assetName, out AssetID id)
    {
        if (ObjectNameToAssetID.TryGetValue(assetName, out id)) return true;
        if (AssetBundleLoader.Instance == null) return false;
        for (int i = 0; i < AssetBundleLoader.Instance.m_assetLoaders.Length; ++i)
        {
            AssetLoader loader = AssetBundleLoader.Instance.m_assetLoaders[i];
            loader.Load();
            if (loader.Asset != null && loader.Asset.name == assetName)
            {
                ObjectNameToAssetID[loader.Asset.name] = loader.m_assetID;
                id = loader.m_assetID;
                return true;
            }
            loader.Unload();
        }
        return false;
    }

    [HarmonyPatch(typeof(ZoneSystem), nameof(ZoneSystem.SetupLocations))]
    private static class ZoneSystem_SetupLocations_Patch
    {
        private static void Postfix(ZoneSystem __instance)
        {
            var dirPath = Path.Combine(Paths.ConfigPath, AltBiomesPlugin.ModName);
            Directory.CreateDirectory(dirPath);
            
            for (int i = 0; i < __instance.m_locations.Count; ++i)
            {
                ZoneLocationRef location = __instance.m_locations[i];
                if (location.m_prefabName == null || string.IsNullOrEmpty(location.m_prefabName)) continue;
                
                var path = Path.Combine(dirPath, location.m_prefabName + ".yml");
                var yml = ISerialize.Serialize(location);
                File.WriteAllText(path, yml);
            }
        }
    }
    
}