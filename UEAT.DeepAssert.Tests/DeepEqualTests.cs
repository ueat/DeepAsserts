using UEAT.DeepAssert.Tests.TestData;
using Xunit;

namespace UEAT.DeepAssert.Tests
{
    public class DeepEqualTests
    {
        [Fact]
        public void Primitive_DontThrowIfEqual()
        {
            var expected = new IntegerData {Value = 1};
            var result = new IntegerData { Value = 1 };

            DeepEqual.Assert(expected, result);
        }

        [Fact]
        public void Primitive_ThrowIfDifferent()
        {
            var expected = new IntegerData { Value = 1 };
            var result = new IntegerData { Value = 2 };

            Assert.Throws<DeepEqualException>(() =>
            {
                DeepEqual.Assert(expected, result);
            });
        }

        [Fact]
        public void NullablePrimitive_ThrowIfDifferent()
        {
            int? expected = 5;
            int? result = 3;

            Assert.Throws<DeepEqualException>(() =>
            {
                DeepEqual.Assert(expected, result);
            });
        }

        [Fact]
        public void NullablePrimitive_DontThrowIfSame()
        {
            int? expected = 5;
            int? result = 5;

            DeepEqual.Assert(expected, result);
        }

        [Fact]
        public void NullablePrimitive_DontThrowIfBothNull()
        {
            int? expected = null;
            int? result = null;

            DeepEqual.Assert(expected, result);
        }

        [Fact]
        public void NullablePrimitive_ThrowIfExpectedOnlyNull()
        {
            int? expected = null;
            int? result = 5;

            Assert.Throws<DeepEqualException>(() =>
            {
                DeepEqual.Assert(expected, result);
            });
        }

        [Fact]
        public void NullablePrimitive_ThrowIfResultOnlyNull()
        {
            int? expected = 5;
            int? result = null;

            Assert.Throws<DeepEqualException>(() =>
            {
                DeepEqual.Assert(expected, result);
            });
        }

        [Fact]
        public void SubObject_DontThrowIfEqual()
        {
            var expected = new SubObject { Data = new IntegerData { Value = 1 } };
            var result = new SubObject { Data = new IntegerData { Value = 1 } };

            DeepEqual.Assert(expected, result);
        }

        [Fact]
        public void SubObject_ThrowIfDifferent()
        {
            var expected = new SubObject { Data = new IntegerData { Value = 1 } };
            var result = new SubObject { Data = new IntegerData { Value = 2 } };

            Assert.Throws<DeepEqualException>(() =>
            {
                DeepEqual.Assert(expected, result);
            });
        }

        [Fact]
        public void List_ThrowIfDifferentLength()
        {
            var expected = new[] { 1 };
            var result   = new[] { 1, 2 };

            Assert.Throws<DeepEqualException>(() =>
            {
                DeepEqual.Assert(expected, result);
            });
        }

        [Fact]
        public void List_ThrowIfDifferentContent()
        {
            var expected = new[] { 1 };
            var result = new[] { 2 };

            Assert.Throws<DeepEqualException>(() =>
            {
                DeepEqual.Assert(expected, result);
            });
        }

        [Fact]
        public void List_DontThrowIfSameContent()
        {
            var expected = new[] { 1 };
            var result = new[] { 1 };
            DeepEqual.Assert(expected, result);
        }
    }
}
