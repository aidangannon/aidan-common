using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

namespace Aidan.Common.Utils.Web
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection BindJsonNamingPolicy( this IServiceCollection serviceCollection ) =>
            serviceCollection.AddTransient<JsonNamingPolicy, SnakeCaseJsonNamingPolicy>( );

        public static JsonNamingPolicy ResolveJsonNamingPolicy( this IServiceCollection serviceCollection ) =>
            serviceCollection.BuildServiceProvider().GetService<JsonNamingPolicy>( );
    }
}