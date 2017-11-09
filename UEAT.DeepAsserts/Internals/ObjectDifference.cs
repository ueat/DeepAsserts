using System;
using System.Linq;

namespace UEAT.DeepAsserts.Internals
{
    internal class ObjectDifference : Difference
    {

        public ObjectDifference(ObjectDifference parent, Type type, object expected, object result, string path) : base(parent, type, expected, result, path)
        {
        }

        public override string ToString()
        {
            return base.ToString() + Environment.NewLine + string.Join(Environment.NewLine, Children.Select(child => child.ToString()));
        }
    }
}
