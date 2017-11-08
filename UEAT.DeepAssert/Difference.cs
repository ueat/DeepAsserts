using System;

namespace UEAT.DeepAssert
{
    internal class Difference
    {
        public object Expected { get; }
        public object Result { get; }
        public string Path { get; }
        public Type Type { get; }

        public Difference(Type type, object expected, object result, string path)
        {
            Type = type;
            Expected = expected;
            Result = result;
            Path = path;
        }

        public override string ToString()
        {
            return $"{Path} ({Type.Name}) // Expected: {Expected} // Result: {Result}";
        }
    }
}
