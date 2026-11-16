using System;
using System.Globalization;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace AltBiomes;

public sealed class InvariantCultureFloatConverter : IYamlTypeConverter
{
    public static readonly InvariantCultureFloatConverter Instance = new();

    public bool Accepts(Type type) =>
        type == typeof(float) || type == typeof(double) || type == typeof(decimal);

    public object? ReadYaml(IParser parser, Type type, ObjectDeserializer rootDeserializer)
    {
        Scalar scalar = parser.Consume<Scalar>();
        return type switch
        {
            _ when type == typeof(float)   => float.Parse(scalar.Value, CultureInfo.InvariantCulture),
            _ when type == typeof(double)  => double.Parse(scalar.Value, CultureInfo.InvariantCulture),
            _ when type == typeof(decimal) => decimal.Parse(scalar.Value, CultureInfo.InvariantCulture),
            _ => throw new NotSupportedException()
        };    }

    public void WriteYaml(IEmitter emitter, object? value, Type type, ObjectSerializer serializer)
    {
        string str = value switch
        {
            float f   => f.ToString(CultureInfo.InvariantCulture),
            double d  => d.ToString(CultureInfo.InvariantCulture),
            decimal m => m.ToString(CultureInfo.InvariantCulture),
            _ => throw new NotSupportedException()
        };
        emitter.Emit(new Scalar(str));    
    }
}