using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UEAT.DeepAsserts
{
    /// <summary>
    /// Compare two object with a deep equality.
    /// </summary>
    public static class DeepAssert
    {
        /// <summary>
        /// Compare an expected result with the actual result using a deep equality. 
        /// If there is a difference, a clear diff will be thrown.
        /// </summary>
        /// <typeparam name="T">Type to compare</typeparam>
        /// <param name="expected">Expected result</param>
        /// <param name="result">Actual result</param>
        public static void Equals<T>(T expected, T result)
        {
            DifferenceCollector deepDifference = new DifferenceCollector(typeof(T));
            RecursiveAssert(typeof(T), expected, result, string.Empty, deepDifference);

            deepDifference.Verify();
        }

        private static void RecursiveAssert(Type objectType, object expected, object result, string path, DifferenceCollector differenceCollector)
        {
            if (IsPrimitive(objectType))
            {
                if (expected == null || result == null)
                {
                    if (!(expected == null && result == null))
                    {
                        differenceCollector.AddDifference(new Difference(objectType, expected, result, path));
                    }
                }
                else if (!expected.Equals(result))
                {
                    differenceCollector.AddDifference(new Difference(objectType, expected, result, path));
                }

                return;
            }

            if (!differenceCollector.TryCollect(expected, result))
            {
                return;
            }

            if (expected is IList expectedList && result is IList resultList)
            {
                RecursiveAssert(typeof(int), expectedList.Count, resultList.Count, AddToPath(path, "Count"), differenceCollector);

                var type = HeuristicallyDetermineType(expectedList);

                for (int i = 0; i < expectedList.Count; ++i)
                {
                    RecursiveAssert(type, expectedList[i], resultList[i], path + $"[{i}]", differenceCollector);
                }

                return;
            }

            List<PropertyInfo> properties = objectType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead)
                .Where(p => !p.GetCustomAttributes(typeof(DeepAssertIgnore), false).Any())
                .ToList();

            foreach (var property in properties)
            {
                var expectedValue = expected == null ? null : property.GetValue(expected);
                var resultValue = result == null ? null : property.GetValue(result);
                RecursiveAssert(property.PropertyType, expectedValue, resultValue, AddToPath(path, property.Name), differenceCollector);
            }
        }
        
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

        private static bool IsPrimitive(Type type)
        {
            return
                ((IList) PrimitiveTypes).Contains(type) ||
                Convert.GetTypeCode(type) != TypeCode.Object;
        }

        private static string AddToPath(string path, string name)
        {
            if (string.IsNullOrEmpty(path))
            {
                return name;
            }

            return $"{path}.{name}";
        }

        private static Type HeuristicallyDetermineType(IList myList)
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
