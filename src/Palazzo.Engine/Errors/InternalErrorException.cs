using System.Net;

namespace Palazzo.Errors;

public class InternalErrorException: Exception, IProblemException
{
    readonly string _instance;

    public InternalErrorException(string message, string instance) : base(message)
    {
        _instance = instance;
    }

    public string? Title => "An internal error occurred";
    public string? Detail => Message;
    public string? Type => $"{Problems.BaseUrl}/internal-error";
    public int? StatusCode => (int)HttpStatusCode.InternalServerError;
    public string? Instance => $"{Type}/{_instance}";
}