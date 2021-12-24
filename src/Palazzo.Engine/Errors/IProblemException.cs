namespace Palazzo.Errors;

public interface IProblemException
{
    string? Title { get; }
    string? Detail { get; }
    string? Type { get; }
    int? StatusCode { get; }
    string? Instance { get; }
}