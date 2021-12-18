namespace Palazzo;

/// <summary>
/// Represents the database settings, as stored in the 'db.palazzo.json' file.
/// </summary>
public class DatabaseSettings
{
    public static readonly string FileName = "db.palazzo.json";

    /// <summary>
    /// The version number of the data stored in this database. This property must REMAIN in place with exactly the same name across all data format versions!
    /// </summary>
    public Version Version { get; set; } = new();
}