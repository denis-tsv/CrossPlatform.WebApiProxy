using System;
using Xunit;

#if GENERATE_MODEL
using Demo.Client.GenerateModel.WebApiProxy;
#else
using Demo.Client.UseSharedModel.WebApiProxy;
using Demo.Model;
#endif

namespace Demo.Client.Tests
{
    public class TestPostMethod
    {
        ITestPostMethodClient client = new TestPostMethodClient(new Uri("http://localhost:36761"));
        
        [Fact]
        public async void PostX()
        {
            await client.PostXAsync(124);
        }

        [Fact]
        public async void PostId()
        {
            var res = await client.PostIdAsync(123, "Name 123");
            
            Assert.Equal(123, res);
        }
    }
}
