using System;
using System.Collections.Generic;
using System.Linq;

namespace UEAT.DeepAssert
{
    internal class DifferenceCollector
    {
        private readonly IList<Difference> _diff = new List<Difference>();
        private readonly Type _assertType;

        public DifferenceCollector(Type assertType)
        {
            _assertType = assertType;
        }

        public void Add(Difference diff)
        {
            _diff.Add(diff);
        }

        public void Verify()
        {
            if (!_diff.Any())
            {
                return;
            }

            var message = $"{_assertType.Name} is not as expected:" + Environment.NewLine + Environment.NewLine;

            foreach (var diff in _diff)
            {
                message += diff + Environment.NewLine;
            }

            throw new DeepEqualException(message);
        }
    }
}
