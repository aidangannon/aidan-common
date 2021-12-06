using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Aidan.Common.Core.Attributes;
using Aidan.Common.Core.Constants;
using Aidan.Common.Core.Enum;
using Aidan.Common.Core.Interfaces.Excluded;
using Microsoft.Extensions.DependencyInjection;

namespace Aidan.Common.DependencyInjection
{
    public static class ModuleExtensions
    {
        private static IServiceCollection Bind( IServiceCollection services, Type [ ] types, string rootNamespace,  string layer = "" )
        {
            RegisterServices( services, GetServiceTypes( GetInterfaces( types, rootNamespace, layer ), types ) );
            RegisterFactories( services, GetFactories( GetInterfaces( types , rootNamespace, layer ) ) );
            return services;
        }

        private static void RegisterViewModels( IServiceCollection services, Type [ ] types )
        {
            var viewModels = types.Where( x => x.Name.EndsWith( "ViewModel" ) );
            foreach( var viewModel in viewModels )
            {
                services.AddTransient( viewModel );
            }
        }
        
        private static void RegisterFactories( IServiceCollection services, Type [ ] factories )
        {
            foreach( var factory in factories ) services.AddFactory( factory );
        }

        private static void RegisterServices( IServiceCollection services,
            List<((Type contract, ServiceLifetime scope), Type implementation)> serviceTypes )
        {
            foreach( var ((contract, scope), implementation) in serviceTypes )
                services.Add( new ServiceDescriptor( contract, implementation, scope ) );
        }

        private static List<((Type contract, ServiceLifetime scope), Type implementation)> GetServiceTypes(
            Type [ ] interfaces, Type [ ] types )
        {
            var serviceTypes = new List<((Type contract, ServiceLifetime scope), Type implementation)>( );
            foreach( var iInterface in interfaces )
            {
                var lifetime = ServiceLifetime.Transient;
                try { AddService( types, iInterface, lifetime, serviceTypes ); }
                catch( Exception )
                {
                    // service not found
                }
            }

            return serviceTypes;
        }

        private static void AddService( Type [ ] types, Type iInterface, ServiceLifetime lifetime,
            List<((Type contract, ServiceLifetime scope), Type implementation)> serviceTypes )
        {
            var service = types
                .First( x =>
                    x.GetInterfaces( ).Any( type =>
                        $"{type.FullName}.{type.Name}" == $"{iInterface.FullName}.{iInterface.Name}" || $"{type.Namespace}.{type.Name}" == $"{iInterface.Namespace}.{iInterface.Name}" ) &&
                    x.IsClass );
            try
            {
                var serviceAttribute = iInterface
                    .GetCustomAttributes<ServiceAttribute>( )
                    .First( );
                lifetime = GetServiceLifetime( serviceAttribute, lifetime );
            }
            catch( Exception )
            {
                try
                {
                    var serviceAttribute = iInterface
                        .GetInterfaces( )
                        .Where( x =>
                        {
                            try
                            {
                                x.GetCustomAttributes<ServiceAttribute>( );
                                return true;
                            }
                            catch( Exception )
                            {
                                return false;
                            }
                        } )
                        .Select( x => x.GetCustomAttributes<ServiceAttribute>( ).First( ) )
                        .First( );
                    lifetime = GetServiceLifetime( serviceAttribute, lifetime );
                }
                catch( Exception )
                {
                    // no custom scope
                }
            }

            serviceTypes
                .Add( ( ( iInterface, lifetime ), service ) );
        }

        private static ServiceLifetime GetServiceLifetime( ServiceAttribute serviceAttribute, ServiceLifetime lifetime )
        {
            switch( serviceAttribute.Scope )
            {
                case ServiceLifetimeEnum.Scoped:
                    lifetime = ServiceLifetime.Scoped;
                    break;
                case ServiceLifetimeEnum.Singleton:
                    lifetime = ServiceLifetime.Singleton;
                    break;
                case ServiceLifetimeEnum.Transient:
                    lifetime = ServiceLifetime.Transient;
                    break;
            }

            return lifetime;
        }

        private static Type [ ] GetFactories( Type [ ] interfaces )
        {
            var factories = interfaces
                .Where( x => x.GetInterfaces( ).Any( subType => subType == typeof( IFactory ) ) )
                .ToArray( );
            return factories;
        }

        private static Type [ ] GetInterfaces( Type [ ] types, string rootNamespace, string layer = "" )
        {
            var interfaces = types
                .Where( type =>
                {
                    try
                    {
                        var namespaceStr = layer != ""
                            ? $"{rootNamespace}.{layer}.{ApplicationConstants.ContractNamespace}"
                            : $"{rootNamespace}.{ApplicationConstants.ContractNamespace}";
                        return type.FullName.Contains( namespaceStr ) || type.Namespace.Contains( namespaceStr ) &&
                               type.IsInterface;
                    }
                    catch( Exception ) { return false; }
                } )
                .ToArray( );
            return interfaces;
        }

        private static Type [ ] GetTypes( string rootNamespace, string layer = "" )
        {
            var types = AppDomain
                .CurrentDomain
                .GetAssemblies( )
                .SelectMany( x => x.GetTypes( ) )
                .Where( x =>
                {
                    var namespaceStr = layer != ""
                        ? $"{rootNamespace}.{layer}"
                        : $"{rootNamespace}";
                    try { return x.FullName.Contains( namespaceStr ) || x.Namespace.Contains( namespaceStr ); }
                    catch( Exception ) { return false; }
                } )
                .ToArray( );
            return types;
        }

        public static IServiceCollection BindServices( this IServiceCollection serviceCollection,
            ApplicationLayerEnum applicationLayer,
            Action [ ] initializers,
            string rootNamespace )
        {
            var types = GetTypes( rootNamespace, applicationLayer.ToString( ) );
            Bind( serviceCollection, types , rootNamespace, applicationLayer.ToString( ) );
            if( applicationLayer == ApplicationLayerEnum.UI )
            {
                RegisterViewModels( serviceCollection, types );
            }

            return serviceCollection;
        }

        public static IServiceCollection BindServices( this IServiceCollection serviceCollection,
            Action [ ] initializers,
            string rootNamespace )
        {
            var types = GetTypes( rootNamespace );
            Bind( serviceCollection, types,  rootNamespace );

            return serviceCollection;
        }
            
    }
}