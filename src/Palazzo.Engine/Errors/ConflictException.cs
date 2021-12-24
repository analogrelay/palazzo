using System.Net;

namespace Palazzo.Errors;

public class ConflictException: Exception, IProblemException
{
    readonly string _instance;

    public ConflictException(string message, string instance) : base(message)
    {
        _instance = instance;
    }

    public string? Title => "A conflict occurred";
    public string? Detail => Message;
    public string? Type => $"{Problems.BaseUrl}/conflict";
    public int? StatusCode => (int)HttpStatusCode.Conflict;
    public string? Instance => $"{Type}/{_instance}";
}