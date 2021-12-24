using Microsoft.AspNetCore.Mvc;

namespace Palazzo.Server;

class Problems
{
    static readonly string BaseUrl = "https://palazzo.analogrelay.net/errors";

    public static ProblemDetails MissingRequiredArgument(string name) =>
        Create(nameof(MissingRequiredArgument), "Missing Required Argument",
            $"The required argument '{name}' was not specified.");

    public static ProblemDetails ObjectNotFound(string type, string name) =>
        Create(nameof(ObjectNotFound), $"{type} not found",
            $"{type} '{name}' was not found.");

    private static ProblemDetails Create(string name, string title, string detail) => new()
    {
        Type = $"{BaseUrl}/{name}",
        Title = title,
        Detail = detail,
    };
}