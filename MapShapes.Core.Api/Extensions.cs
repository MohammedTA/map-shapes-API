namespace MapShapes.Core.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;

    public static class Extensions
    {
        private static readonly Lazy<Assembly[]> AssembliesWithRequestHandlers = new Lazy<Assembly[]>(GetAssembliesWithRequestHandlers);

        public static void ConfigureMediatr(this IServiceCollection services)
        {
            // Register all other referenced assemblies.
            services.AddMediatR(AssembliesWithRequestHandlers.Value);
        }

        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
            }
        }

        public static IEnumerable<Type> GetBaseClasses(this Type type)
        {
            var current = type;
            while (current.BaseType != typeof(object) && current.BaseType != null)
            {
                yield return current.BaseType;
                current = current.BaseType;
            }
        }

        public static bool ImplementsGenericType(this Type me, Type genericType)
        {
            if (!genericType.IsGenericType)
            {
                throw new ArgumentException("Supplied argument is not a generic type", nameof(genericType));
            }

            return me.GetBaseClasses().Any(b => b.IsConstructedGenericType && b.GetGenericTypeDefinition() == genericType);
        }

        private static Assembly[] GetAssembliesWithRequestHandlers()
        {
            var assemblies =
                Assembly.GetExecutingAssembly()
                    .GetReferencedAssemblies()
                    .SelectMany(name => Assembly.Load(name).GetTypes())
                    .Union(
                        AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(a => a.GetTypes()))
                    .Where(t => t.FullName != null && t.FullName.Contains("MapShapes"))
                    .Select(t => t.Assembly)
                    .Distinct()
                    .ToArray();
            return assemblies;
        }
    }
}