using System;
using System.Collections.Generic;
using System.Reflection;

namespace LethalVibrations.Utils;

public class ReflectionUtility
{
    public static ValueTuple<MethodInfo, Attribute>[] GetMethodsWithAttribute<T>() where T : Attribute
    {
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var list = new List<ValueTuple<MethodInfo, Attribute>>();
        foreach (var t in assemblies)
        {
            var types = t.GetTypes();
            foreach (var t1 in types)
            {
                foreach (var methodInfo in t1.GetMethods())
                {
                    var customAttribute = methodInfo.GetCustomAttribute<T>();
                    if (customAttribute != null)
                    {
                        list.Add(new ValueTuple<MethodInfo, Attribute>(methodInfo, customAttribute));
                    }
                }
            }
        }
        return list.ToArray();
    }
}