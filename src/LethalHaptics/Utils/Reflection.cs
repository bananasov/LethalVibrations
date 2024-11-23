using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LethalHaptics.Utils;

public static class Reflection
{
    public static ValueTuple<MethodInfo, Attribute>[] GetMethodsWithAttribute<T>() where T : Attribute
    {
        var currentAssembly = Assembly.GetExecutingAssembly();
        var types = currentAssembly.GetTypes();
        var list = new List<ValueTuple<MethodInfo, Attribute>>();
        
        // TODO: Find a way to make this cleaner.
        foreach (var type in types)
        {
            foreach (var methodInfo in type.GetMethods())
            {
                var customAttribute = methodInfo.GetCustomAttribute<T>();
                if (customAttribute != null)
                {
                    list.Add(new ValueTuple<MethodInfo, Attribute>(methodInfo, customAttribute));
                }
            }
        }
        
        return list.ToArray();
    }
}