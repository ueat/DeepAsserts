namespace UEAT.DeepAsserts.Tests.TestData
{
    public class RecursiveParentData
    {
        public RecursiveChildData Child { get; set; }

        public RecursiveParentData()
        {
            Child = new RecursiveChildData(this);
        }
    }
}
