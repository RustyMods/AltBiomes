namespace AltBiomes;

public class EnvEntryRef
{
    public string? m_environment;
    public float? m_weight;
    public bool? m_ashlandsOverride;
    public bool? m_deepnorthOverride;
    
    public static implicit operator EnvEntry(EnvEntryRef data)
    {
        var result = new EnvEntry
        {
            m_environment = data.m_environment ?? "",
            m_ashlandsOverride = data.m_ashlandsOverride ?? false,
            m_deepnorthOverride = data.m_deepnorthOverride ?? false,
        };

        if (string.IsNullOrEmpty(data.m_environment)) return result;

        if (EnvMan.instance.GetEnv(data.m_environment) is { } env)
        {
            result.m_env = env;
            result.m_weight = data.m_weight ?? 1f;
        }
        
        return result;
    }

    public static implicit operator EnvEntryRef(EnvEntry data)
    {
        var result = new EnvEntryRef
        {
            m_environment = data.m_environment,
            m_weight = data.m_weight,
            m_ashlandsOverride = data.m_ashlandsOverride,
            m_deepnorthOverride = data.m_deepnorthOverride,
        };

        return result;
    }
}