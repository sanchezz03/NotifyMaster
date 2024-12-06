using System.Reflection;

namespace NotifyMaster.Common.Helpers;

public static class TypesHelper
{
    #region Public methods

    /// <summary>
    /// Returns target types from all assemblies by current
    /// </summary>
    /// <param name="targetType"></param>
    /// <returns>List of types</returns>
    public static List<Type> GetTypesFromAllAssembliesByGenericInterface(Type targetType)
    {
        return GetAllAssembly()
            .SelectMany(s => s.GetTypes())
            .Where(p => p.GetInterfaces().Any(i => i.Name == targetType.Name) && p.FullName!.StartsWith("NotifyMaster"))
            .ToList();
    }

    /// <summary>
    /// Returns target types from all assemblies by current
    /// </summary>
    /// <typeparam name="T">target type</typeparam>
    /// <returns>List of types</returns>
    public static IList<Type> GetTypesFromAllAssemblies<T>()
    {
        var type = typeof(T);

        return GetAllAssembly()
            .SelectMany(s => s.GetTypes())
            .Where(p => type.IsAssignableFrom(p) && !p.Name.Equals(type.Name))
            .ToList();
    }

    /// <summary>
    /// Returns derived types from assembly
    /// </summary>
    /// <param name="targetType"></param>
    /// <returns>List of types</returns>
    public static IList<Type> GetDerivedTypesFromAssembly(Type targetType)
    {
        return targetType
            .Assembly
            .GetTypes()
            .Where(t => IsSubclassOfRawGeneric(targetType, t))
            .ToList();
    }

    #endregion


    #region Private methods

    private static List<Assembly> GetAllAssembly()
    {
        List<Assembly> allAssemblies = new List<Assembly>();
        var location = Assembly.GetExecutingAssembly().Location;
        string path = Path.GetDirectoryName(location);
        foreach (string dll in Directory.GetFiles(path, "*.dll"))
        {
            allAssemblies.Add(Assembly.LoadFile(dll));
        }

        return allAssemblies;
    }

    private static bool IsSubclassOfRawGeneric(Type baseType, Type derivedType)
    {
        while (derivedType != null && derivedType != typeof(object) && derivedType != baseType)
        {
            var currentType = derivedType.IsGenericType ? derivedType.GetGenericTypeDefinition() : derivedType;
            if (baseType == currentType)
            {
                return true;
            }

            derivedType = derivedType.BaseType;
        }
        return false;
    }

    #endregion
}
