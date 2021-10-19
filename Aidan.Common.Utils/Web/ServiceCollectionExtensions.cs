using System;
using System.Text.Json;
using Aidan.Common.Core.Enum;
using Aidan.Common.Core.Interfaces.Contract;
using Aidan.Common.Utils.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace Aidan.Common.Utils.Web
{
    public static class ServiceCollectionExtensions
    {
        private static JsonNamingPolicy ResolveJsonNamingPolicy( this IServiceCollection serviceCollection ) =>
            serviceCollection.BuildServiceProvider().GetService<JsonNamingPolicy>( );

        public static IMvcBuilder BindJsonOptions( this IMvcBuilder mvcBuilder, CaseEnum caseEnum )
        {
            switch( caseEnum )
            {
                case CaseEnum.Snake:
                    mvcBuilder.Services
                        .AddTransient<JsonNamingPolicy, SnakeCaseJsonNamingPolicy>( )
                        .AddTransient<ISerializer, JsonSnakeCaseSerialzier>( );
                    return mvcBuilder.AddJsonOptions( x => x
                        .JsonSerializerOptions
                        .PropertyNamingPolicy = mvcBuilder
                        .Services
                        .BuildServiceProvider( )
                        .GetService<JsonNamingPolicy>( ) );
                case CaseEnum.Pascal:
                    mvcBuilder.Services
                        .AddTransient<ISerializer, JsonCamelAndPascalCaseSerializer>( );
                    return mvcBuilder;
                case CaseEnum.Camel:
                    mvcBuilder.Services
                        .AddTransient<ISerializer, JsonCamelAndPascalCaseSerializer>( );
                    return mvcBuilder
                        .AddJsonOptions( x => x
                            .JsonSerializerOptions
                            .PropertyNamingPolicy = JsonNamingPolicy
                                .CamelCase );
                default:
                    throw new ArgumentException( "invalid case" );
            }
        }
    }
}