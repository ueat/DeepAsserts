using System;

namespace UEAT.DeepAsserts
{
    /// <inheritdoc />
    /// <summary>
    /// Deep equality has failed. The details of the diff is within the exception message.
    /// </summary>
    public class DeepAssertException : Exception
    {
        internal DeepAssertException(string message)
            : base(message)
        {
            
        }
    }
}
