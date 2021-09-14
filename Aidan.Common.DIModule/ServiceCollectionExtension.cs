using System;
using Microsoft.Extensions.DependencyInjection;
using Aidan.Common.Configuration;
using Aidan.Common.Core;
using Aidan.Common.Core.Enum;
using Aidan.Common.Utils;
using Aidan.Common.Web;

namespace Aidan.Common.DIModule
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection BindCrosscuttingLayer( this IServiceCollection serviceCollection, string rootNamespace ) =>
            serviceCollection.BindServices( ApplicationLayerEnum.Crosscutting, new Action[]
            {
                MainInitializer.Initialize,
                UtilsInitializer.Initialize,
                WebUtilsInitializer.Initialize
            }, rootNamespace );
    }
}