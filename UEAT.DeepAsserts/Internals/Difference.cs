using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UEAT.DeepAsserts.Internals
{
    internal abstract class Difference
    {
        public ObjectDifference Parent { get; }
        public IList<Difference> Children { get; } = new List<Difference>();

        public Type Type { get; }
        public object Expected { get; }
        public object Result { get; }
        public string Path { get; }


        public int Deep
        {
            get
            {
                Difference parent = Parent;
                int count = 0;

                while (parent != null)
                {
                    ++count;
                    parent = parent.Parent;
                }

                return count;
            }
        }

        protected Difference(ObjectDifference parent, Type type, object expected, object result, string path)
        {
            Parent = parent;
            Type = type;
            Expected = expected;
            Result = result;
            Path = path;

            Parent?.Children.Add(this);
        }

        public bool HasPrimitiveDifference()
        {
            return this is PrimitiveDifference || Children.Any(child => child.HasPrimitiveDifference());
        }
        public override string ToString()
        {
            return new String('\t', Deep) + $"{Path}";
        }
    }
}
