namespace UEAT.DeepAsserts.Tests.TestData
{
    public class PrivatePropertyData
    {
        // Don't remove get property, we can see it through reflection
        private int Value { get; }

        public PrivatePropertyData(int value)
        {
            Value = value;
        }
    }
}
