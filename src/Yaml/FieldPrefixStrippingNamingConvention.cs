using YamlDotNet.Serialization;

namespace AltBiomes;

public sealed class FieldPrefixStrippingNamingConvention(string prefix) : INamingConvention
{
    public string Apply(string value)
    {
        return value.StartsWith(prefix) ? value.Substring(prefix.Length) : value;
    }

    public string Reverse(string value)
    {
        return prefix + value;
    }
}