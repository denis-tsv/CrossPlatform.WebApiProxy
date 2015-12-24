using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Demo.Client.GenerateModel.WebApiProxy;
using Xunit;

namespace Demo.Client.GenerateModel.Tests
{
    public class TestIgnoreModel
    {
        [Fact]
        public void GetIgnoredModel()
        {
            var generatedName = "Demo.Client.GenerateModel.WebApiProxy.IgnoredModel";
            var existingName = "Demo.Model.IgnoredModel";

            var genType = Type.GetType(generatedName);
            Assert.Null(genType);

            var existsType = Type.GetType(existingName);
            Assert.NotNull(existsType);
        }

        [Fact]
        public void GetModelWithIgnoredProperties()
        {
            var type = typeof (ModelWithIgnoredProperties);

            var proxyIgnore = type.GetProperty("ProxyIgnore");
            Assert.Null(proxyIgnore);

            var jsonIgnore = type.GetProperty("JsonIgnore");
            Assert.Null(jsonIgnore);

            var xmlIgnore = type.GetProperty("XmlIgnore");
            Assert.Null(xmlIgnore);

            var ignoreDataMember = type.GetProperty("IgnoreDataMember");
            Assert.Null(ignoreDataMember);

            var apiExplorerSettings = type.GetProperty("ApiExplorerSettings");
            Assert.Null(apiExplorerSettings);

            var notIgnored = type.GetProperty("NotIgnored");
            Assert.NotNull(notIgnored);
        }

        [Fact]
        public void GetDataContractModel()
        {
            var type = typeof (DataContractModel);

            var noDataMember = type.GetProperty("NoDataMember");
            Assert.Null(noDataMember);

            var hasDataMember = type.GetProperty("HasDataMember");
            Assert.NotNull(hasDataMember);
        }

        [Fact]
        public void GetDataContractEnumModel()
        {
            var names = Enum.GetNames(typeof (DataContractEnumModel));

            Assert.True(names.Contains("HasEnumMember"));
            Assert.False(names.Contains("NoEnumMember"));
        }

    }
}
