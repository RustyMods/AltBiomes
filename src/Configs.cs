using BepInEx.Configuration;

namespace AltBiomes;
#nullable disable
public static class Configs
{
    private static ConfigEntry<Toggle> removeOriginals;

    public static bool RemoveOriginals => removeOriginals.Value is Toggle.On;

    public static void Init()
    {
        removeOriginals = ConfigManager.config("Settings", "Remove Originals", Toggle.Off,
            "If on, will remove all vanilla alt biome settings");
    }
}