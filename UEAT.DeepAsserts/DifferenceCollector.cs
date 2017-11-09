using System;
using System.Collections.Generic;
using System.Linq;

namespace UEAT.DeepAsserts
{
    internal class DifferenceCollector
    {
        private readonly IList<object> _treated = new List<object>();
        private readonly IList<Difference> _diff = new List<Difference>();
        private readonly Type _assertType;

        public DifferenceCollector(Type assertType)
        {
            _assertType = assertType;
        }

        public bool TryCollect(object expected, object result)
        {
            if (_treated.Any(t => t == expected || t == result))
            {
                return false;
            }

            _treated.Add(expected);
            _treated.Add(result);

            return true;
        }

        public void AddDifference(Difference diff)
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

            throw new DeepAssertException(message);
        }
    }
}
