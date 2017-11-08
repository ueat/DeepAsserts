namespace UEAT.DeepAsserts.Tests.TestData
{
    public class IgnorePropertyData
    {
        [DeepAssertIgnore]
        public int Value { get; set; }
    }
}
