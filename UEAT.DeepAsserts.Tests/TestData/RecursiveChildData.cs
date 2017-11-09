namespace UEAT.DeepAsserts.Tests.TestData
{
    public class RecursiveChildData
    {
        public RecursiveParentData Parent { get; }

        public RecursiveChildData(RecursiveParentData parent)
        {
            Parent = parent;
        }
    }
}
