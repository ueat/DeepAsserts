using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UEAT.DeepAsserts.Internals
{
    internal class DifferenceCollector
    {
        private readonly IList<Tuple<object, object>> _treated = new List<Tuple<object, object>>();
        private readonly IList<Difference> _rootDiff = new List<Difference>();
        private readonly Type _assertType;

        public DifferenceCollector(Type assertType)
        {
            _assertType = assertType;
        }

        public void Collect(Type objectType, object expected, object result)
        {
            InnerCollect(objectType, expected, result, null, null);
            Verify();
        }

        private void InnerCollect(Type objectType, object expected, object result, ObjectDifference parent, string path)
        {
            if (TypeHelper.IsPrimitive(objectType))
            {
                CollectPrimitive(objectType, expected, result, parent, path);
            }
            else
            {
                if (!Begin(expected, result))
                {
                    return;
                }

                var difference = new ObjectDifference(parent, objectType, expected, result, path);
                if (parent == null)
                {
                    _rootDiff.Add(difference);
                }

                if (expected is IList expectedList && result is IList resultList)
                {
                    CollectList(expectedList, resultList, difference);
                }
                else
                {
                    CollectProperties(objectType, expected, result, difference);
                }

                End(expected, result);
            }
        }

        private bool Begin(object expected, object result)
        {
            var tuple = new Tuple<object, object>(expected, result);

            if (_treated.Contains(tuple))
            {
                return false;
            }

            _treated.Add(tuple);

            return true;
        }

        private void End(object expected, object result)
        {
            var tuple = new Tuple<object, object>(expected, result);
            _treated.Remove(tuple);
        }

        private void CollectPrimitive(Type objectType, object expected, object result, ObjectDifference parent, string path)
        {
            var isDiff = false;
            if (expected == null || result == null)
            {
                if (!(expected == null && result == null))
                {
                    isDiff = true;
                }
            }
            else if (!expected.Equals(result))
            {
                isDiff = true;
            }

            if (isDiff)
            {
                var diff = new PrimitiveDifference(parent, objectType, expected, result, path);

                if (parent == null) 
                {
                    _rootDiff.Add(diff);
                }

            }
        }

        private void CollectList(IList expectedList, IList resultList, ObjectDifference parent)
        {
            CollectPrimitive(typeof(int), expectedList.Count, resultList.Count, parent, "Count");

            var type = TypeHelper.GetListType(expectedList);

            for (int i = 0; i < expectedList.Count; ++i)
            {
                InnerCollect(type, expectedList[i], resultList[i], parent, $"[{i}]");
            }
        }

        private void CollectProperties(Type objectType, object expected, object result, ObjectDifference parent)
        {
            List<PropertyInfo> properties = objectType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead)
                .Where(p => !p.GetCustomAttributes(typeof(DeepAssertIgnore), false).Any())
                .Where(p => p.GetIndexParameters().Length == 0)
                .ToList();

            foreach (var property in properties)
            {
                var expectedValue = expected == null ? null : property.GetValue(expected);
                var resultValue = result == null ? null : property.GetValue(result);
                InnerCollect(property.PropertyType, expectedValue, resultValue, parent, property.Name);
            }
        }

        private void Verify()
        {
            var diffWithPrimitives = _rootDiff.Where(diff => diff.HasPrimitiveDifference()).ToList();

            if (!diffWithPrimitives.Any())
            {
                return;
            }

            var message = $"{_assertType.Name} is not as expected:" + Environment.NewLine + Environment.NewLine;

            foreach (var diff in diffWithPrimitives)
            {
                message += diff + Environment.NewLine;
            }

            throw new DeepAssertException(message);
        }
    }
}
