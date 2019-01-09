using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Blog.Services.CustomAttributes
{
   public class NotNullOrWhiteSpace : Attribute
    {
        public static bool IsAnyNullOrEmpty(object obj)
        {
            if (Object.ReferenceEquals(obj, null))
                return false;

            return obj.GetType().GetProperties()
                .Any(x => IsNullOrEmpty(x.GetValue(obj)));
        }

        private static bool IsNullOrEmpty(object value)
        {
            if (Object.ReferenceEquals(value, null))
                return false;

            var type = value.GetType();
            return type.IsValueType
                   && Object.Equals(value, Activator.CreateInstance(type));
        }
    }
}
