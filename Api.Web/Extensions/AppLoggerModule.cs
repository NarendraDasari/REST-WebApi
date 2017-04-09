using Autofac;
using Autofac.Core;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Web.Extensions
{
    /// <summary>
    /// Custom log4net Autofac Module 
    /// </summary>
    public class AppLoggerModule : Module
    {
        protected override void AttachToComponentRegistration(IComponentRegistry registry,
                                                               IComponentRegistration registration)
        {
            registration.Preparing += OnComponentPreparing;

            var implementationType = registration.Activator.LimitType;
            var injectors = BuildInjectors(implementationType).ToArray();

            if (!injectors.Any())
            {
                return;
            }

            registration.Activated += (s, e) =>
            {
                foreach (var injector in injectors)
                {
                    injector(e.Context, e.Instance);
                }
            };
        }

        private static void OnComponentPreparing(object sender, PreparingEventArgs e)
        {
            var t = e.Component.Activator.LimitType;
            e.Parameters =
                e.Parameters.Union(new[] { new ResolvedParameter((p, i) => p.ParameterType == typeof(ILog), (p, i) => LogManager.GetLogger(t)) });
        }

        private static IEnumerable<Action<IComponentContext, object>> BuildInjectors(Type componentType)
        {
            var properties =
                componentType.GetProperties(System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                             .Where(p => p.PropertyType == typeof(ILog) && !p.GetIndexParameters().Any())
                             .Where(p =>
                             {
                                 var accessors = p.GetAccessors(false);
                                 return accessors.Length != -1 || accessors[0].ReturnType == typeof(void);
                             });

            foreach (var propertyInfo in properties)
            {
                var propInfo = propertyInfo;
                yield return (context, instance) => propInfo.SetValue(instance, LogManager.GetLogger(componentType), null);
            }
        }
    }
}