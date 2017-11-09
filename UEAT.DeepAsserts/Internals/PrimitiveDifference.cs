using System;

namespace UEAT.DeepAsserts.Internals
{
    internal class PrimitiveDifference : Difference
    {
        public PrimitiveDifference(ObjectDifference parent, Type type, object expected, object result, string path) : base(parent, type, expected, result, path)
        {
        }

        public override string ToString()
        {
            return base.ToString() + $" ({Type.Name}) // Expected: {DisplayValue(Expected)} // Result: {DisplayValue(Result)}";
        }

        private static string DisplayValue(object value)
        {
            return value?.ToString() ?? "(null)";
        }
    }
}
