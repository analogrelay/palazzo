namespace Palazzo.Configuration;

/// <summary>
/// Options to configure a Palazzo node.
/// </summary>
public class NodeOptions
{
    public static readonly string SectionName = "Node";

    /// <summary>
    /// The root directory in which to store node data.
    /// </summary>
    public string StorageRoot { get; set; } = "";
}