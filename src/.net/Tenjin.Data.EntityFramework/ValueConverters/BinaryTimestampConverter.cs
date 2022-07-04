using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Tenjin.Data.EntityFramework.ValueConverters;

public class BinaryTimestampConverter : ValueConverter<byte[], string>
{
    public BinaryTimestampConverter() : base(
        v => ToDatabase(v),
        v => FromDatabase(v))
    { }

    private static byte[] FromDatabase(string value) =>
        value.Select(c => (byte)c).ToArray();

    private static string ToDatabase(IEnumerable<byte> value) =>
        new(value.Select(b => (char)b).ToArray());
}