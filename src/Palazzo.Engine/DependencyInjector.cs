using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Palazzo;
using Palazzo.Configuration;
using Palazzo.Storage;

namespace Microsoft.Framework.DependencyInjection;

public static class DependencyInjector
{
    public static void AddPalazzoNode(this IServiceCollection services, IConfiguration configSection)
    {
        services.Configure<NodeOptions>(configSection.GetSection(NodeOptions.SectionName));
        services.AddSingleton<INodeContext, NodeContext>();
        services.AddSingleton<IPalazzoDataFormat, PalazzoDataFormat>();
    }
}