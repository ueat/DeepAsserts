using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UEAT.DeepAsserts.Internals;

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
            deepDifference.Collect(typeof(T), expected, result);
        }
    }
}
