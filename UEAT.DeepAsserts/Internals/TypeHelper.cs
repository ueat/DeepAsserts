using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UEAT.DeepAsserts.Internals
{
    internal static class TypeHelper
    {
        private static readonly Type[] PrimitiveTypes =
        {
            typeof(Boolean),
            typeof(Byte),
            typeof(SByte),
            typeof(Int16),
            typeof(UInt16),
            typeof(Int32),
            typeof(UInt32),
            typeof(Int64),
            typeof(UInt64),
            typeof(IntPtr),
            typeof(UIntPtr),
            typeof(Char),
            typeof(Double),
            typeof(Single),
            typeof(Enum),
            typeof(String),
            typeof(Decimal),
            typeof(DateTime),
            typeof(DateTimeOffset),
            typeof(TimeSpan),
            typeof(Guid)
        };

        public static bool IsPrimitive(Type type)
        {
            return
                ((IList)PrimitiveTypes).Contains(type) ||
                type.GetTypeInfo().IsEnum ||
                Convert.GetTypeCode(type) != TypeCode.Object;
        }

        public static Type GetListType(IList myList)
        {
            var enumerableType =
                myList.GetType()
                    .GetInterfaces()
                    .Where(i => i.GenericTypeArguments.Length == 1)
                    .FirstOrDefault(i => i.GetGenericTypeDefinition() == typeof(IEnumerable<>));

            if (enumerableType != null)
                return enumerableType.GenericTypeArguments[0];

            if (myList.Count == 0)
                return null;

            return myList[0].GetType();
        }
    }
}
