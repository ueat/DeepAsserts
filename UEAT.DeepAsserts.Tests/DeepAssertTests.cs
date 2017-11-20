using UEAT.DeepAsserts.Tests.TestData;
using Xunit;

namespace UEAT.DeepAsserts.Tests
{
    public class DeepAssertTests
    {
        [Fact]
        public void Primitive_DontThrowIfEqual()
        {
            var expected = new IntegerData {Value = 1};
            var result = new IntegerData { Value = 1 };

            DeepAssert.Equals(expected, result);
        }

        [Fact]
        public void Primitive_ThrowIfDifferent()
        {
            var expected = new IntegerData { Value = 1 };
            var result = new IntegerData { Value = 2 };

            Assert.Throws<DeepAssertException>(() =>
            {
                DeepAssert.Equals(expected, result);
            });
        }

        [Fact]
        public void NullablePrimitive_ThrowIfDifferent()
        {
            int? expected = 5;
            int? result = 3;

            Assert.Throws<DeepAssertException>(() =>
            {
                DeepAssert.Equals(expected, result);
            });
        }

        [Fact]
        public void NullablePrimitive_DontThrowIfSame()
        {
            int? expected = 5;
            int? result = 5;

            DeepAssert.Equals(expected, result);
        }

        [Fact]
        public void NullablePrimitive_DontThrowIfBothNull()
        {
            int? expected = null;
            int? result = null;

            DeepAssert.Equals(expected, result);
        }

        [Fact]
        public void NullablePrimitive_ThrowIfExpectedOnlyNull()
        {
            int? expected = null;
            int? result = 5;

            Assert.Throws<DeepAssertException>(() =>
            {
                DeepAssert.Equals(expected, result);
            });
        }

        [Fact]
        public void NullablePrimitive_ThrowIfResultOnlyNull()
        {
            int? expected = 5;
            int? result = null;

            Assert.Throws<DeepAssertException>(() =>
            {
                DeepAssert.Equals(expected, result);
            });
        }

        [Fact]
        public void SubObject_DontThrowIfEqual()
        {
            var expected = new SubObject { Data1 = new IntegerData { Value = 1 } };
            var result = new SubObject { Data1 = new IntegerData { Value = 1 } };

            DeepAssert.Equals(expected, result);
        }

        [Fact]
        public void SubObject_ThrowIfDifferent()
        {
            var expected = new SubObject { Data1 = new IntegerData { Value = 1 }, Data2 = new IntegerData { Value = 1 } };
            var result = new SubObject { Data1 = new IntegerData { Value = 2 }, Data2 = new IntegerData { Value = 2 } };

            Assert.Throws<DeepAssertException>(() =>
            {
                DeepAssert.Equals(expected, result);
            });
        }

        [Fact]
        public void List_ThrowIfExpecterShorter()
        {
            var expected = new[] { 1 };
            var result   = new[] { 1, 2 };

            Assert.Throws<DeepAssertException>(() =>
            {
                DeepAssert.Equals(expected, result);
            });
        }

        [Fact]
        public void List_ThrowIfExpectedLonger()
        {
            var expected = new[] { 1, 2 };
            var result = new[] { 1 };

            Assert.Throws<DeepAssertException>(() =>
            {
                DeepAssert.Equals(expected, result);
            });
        }

        [Fact]
        public void List_ThrowIfDifferentContent()
        {
            var expected = new[] { 1 };
            var result = new[] { 2 };

            Assert.Throws<DeepAssertException>(() =>
            {
                DeepAssert.Equals(expected, result);
            });
        }

        [Fact]
        public void List_DontThrowIfSameContent()
        {
            var expected = new[] { 1 };
            var result = new[] { 1 };
            DeepAssert.Equals(expected, result);
        }

        [Fact]
        public void IgnoreAttribute_DontThrowForIgnoredProperties()
        {
            var expected = new IgnorePropertyData { Value = 1 };
            var result = new IgnorePropertyData { Value = 2 };
            DeepAssert.Equals(expected, result);
        }

        [Fact]
        public void PrivateProperties_DontThrowIfDifferent()
        {
            var expected = new PrivatePropertyData(1);
            var result = new PrivatePropertyData(2);
            DeepAssert.Equals(expected, result);
        }

        [Fact]
        public void StaticProperties_DontThrowIfDifferent()
        {
            var expected = new StaticPropertyData();
            var result = new StaticPropertyData();
            DeepAssert.Equals(expected, result);
        }

        [Fact]
        public void Recursion_AreIgnored()
        {
            var expected = new RecursiveParentData();
            var result = new RecursiveParentData();
            DeepAssert.Equals(expected, result);
        }

        [Fact]
        public void Ignore_IndexedProperties()
        {
            var expected = new IndexedPropertyTestData();
            var result = new IndexedPropertyTestData();
            DeepAssert.Equals(expected, result);
        }
    }
}
