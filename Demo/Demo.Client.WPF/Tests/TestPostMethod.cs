using System;
using Xunit;

#if NETFX_CORE
using Demo.Client.Universal.WebApiProxy;
using Demo.Model;
#else
using Demo.Client.WPF.WebApiProxy;
using Demo.Model;
#endif

#if NETFX_CORE
namespace Demo.Client.Universal.Tests
#else
namespace Demo.Client.WPF.Tests
#endif
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
