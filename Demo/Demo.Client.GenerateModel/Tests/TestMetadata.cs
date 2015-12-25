using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Demo.Client.GenerateModel.WebApiProxy;
using Xunit;

namespace Demo.Client.GenerateModel.Tests
{
    public class TestMetadata
    {
        [Fact]
        public void GetMetadataModel()
        {
            var type = typeof (MetadataModel);

            var required = type.GetProperty("Requred").GetCustomAttribute<RequiredAttribute>();
            Assert.NotNull(required);

            var intRange = type.GetProperty("IntRange").GetCustomAttribute<RangeAttribute>();
            Assert.NotNull(intRange);
            Assert.Equal(100, intRange.Maximum);
            Assert.Equal(1, intRange.Minimum);

            var doubleRange = type.GetProperty("DoubleRange").GetCustomAttribute<RangeAttribute>();
            Assert.NotNull(doubleRange);
            Assert.Equal(1.0, doubleRange.Maximum);
            Assert.Equal(0.5, doubleRange.Minimum);

            var maxLen = type.GetProperty("MaxLength").GetCustomAttribute<MaxLengthAttribute>();
            Assert.NotNull(maxLen);
            Assert.Equal(1024, maxLen.Length);

            var minLen = type.GetProperty("MinLength").GetCustomAttribute<MinLengthAttribute>();
            Assert.NotNull(minLen);
            Assert.Equal(32, minLen.Length);

            var strLen = type.GetProperty("StringLength").GetCustomAttribute<StringLengthAttribute>();
            Assert.NotNull(strLen);
            Assert.Equal(1024, strLen.MaximumLength);

            var strnLenWithMin = type.GetProperty("StringLengthWithMinimum").GetCustomAttribute<StringLengthAttribute>();
            Assert.NotNull(strnLenWithMin);
            Assert.Equal(100, strnLenWithMin.MaximumLength);
            Assert.Equal(1, strnLenWithMin.MinimumLength);

        }
    }
}
