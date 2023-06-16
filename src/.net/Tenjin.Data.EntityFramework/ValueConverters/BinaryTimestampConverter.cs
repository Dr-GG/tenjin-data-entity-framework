using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Tenjin.Data.EntityFramework.ValueConverters;

/// <summary>
/// An entity framework ValueConverter that converts to and from a Microsoft SQL Server timestamp.
/// </summary>
public class BinaryTimestampConverter : ValueConverter<byte[], string>
{
    /// <summary>
    /// Creates a new default instance.
    /// </summary>
    public BinaryTimestampConverter() : base(
        v => ToDatabase(v),
        v => FromDatabase(v))
    { }

    private static byte[] FromDatabase(string value) =>
        value.Select(c => (byte)c).ToArray();

    private static string ToDatabase(IEnumerable<byte> value) =>
        new(value.Select(b => (char)b).ToArray());
}