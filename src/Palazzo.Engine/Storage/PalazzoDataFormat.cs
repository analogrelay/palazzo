using System.Text.Json;

namespace Palazzo.Storage;

public interface IPalazzoDataFormat
{
    /// <summary>
    /// Gets the version of the Palazzo Data Format that this type can handle.
    /// </summary>
    Version Version { get; }

    /// <summary>
    /// Serialize an object to the provided stream.
    /// </summary>
    /// <param name="stream">The <see cref="Stream"/> to serialize the object to.</param>
    /// <param name="value">The object to serialize.</param>
    Task SerializeAsync(Stream stream, object value, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deserialize an object from the provided stream.
    /// </summary>
    /// <param name="stream">The <see cref="Stream"/> to deserialize the object to.</param>
    /// <typeparam name="T">The expected type of the content in the file.</typeparam>
    /// <returns>The deserialized value.</returns>
    ValueTask<T?> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default);
}

public class PalazzoDataFormat: IPalazzoDataFormat
{
    static readonly JsonSerializerOptions SerializerOptions = new()
    {
        AllowTrailingCommas = true,
        ReadCommentHandling = JsonCommentHandling.Skip,
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
    };

    public Version Version => new Version(0, 1);
    public Task SerializeAsync(Stream stream, object value, CancellationToken cancellationToken = default) => JsonSerializer.SerializeAsync(stream, value, SerializerOptions, cancellationToken);

    public ValueTask<T?> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default) =>
        JsonSerializer.DeserializeAsync<T>(stream, SerializerOptions, cancellationToken);
}