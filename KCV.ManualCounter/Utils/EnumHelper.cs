using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Gizeta.KCV.ManualCounter.Utils
{
    public static class EnumHelper
    {
        public static T GetEnumAttribute<T>(this Enum source) where T : Attribute
        {
            Type type = source.GetType();
            string sourceName = Enum.GetName(type, source);
            FieldInfo field = type.GetField(sourceName);
            object[] attributes = field.GetCustomAttributes(typeof(T), false);
            foreach (var o in attributes)
            {
                if (o is T)
                    return (T)o;
            }
            return null;
        }

        public static string GetDescription(this Enum source)
        {
            var desc = source.GetEnumAttribute<DescriptionAttribute>();
            if (desc == null)
                return null;
            return desc.Description;
        }
    }
}
