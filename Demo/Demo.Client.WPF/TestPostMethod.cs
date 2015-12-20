using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proxy;
using Xunit;

namespace Demo.Client.WPF
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
