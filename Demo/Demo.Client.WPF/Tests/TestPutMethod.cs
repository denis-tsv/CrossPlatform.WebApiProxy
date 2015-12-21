using System;
using Demo.Client.WPF.WebApiProxy;
using Xunit;

namespace Demo.Client.WPF.Tests
{
    public class TestPutMethod
    {
        ITestPutMethodClient client = new TestPutMethodClient(new Uri("http://localhost:36761"));

        private PropertiesComparer<User> _userComparer = new PropertiesComparer<User>();

        [Fact]
        public async void PutProxyParameterSource()
        {
            var user = new User
            {
                Id = 1, Name = "NAme", Code = "123"
            };
            var res = await client.PutProxyParameterSourceAsync(user);

            Assert.Equal(user, res, _userComparer);
        }

        [Fact]
        public async void PutVoid()
        {
            var user = new User
            {
                Id = 1,
                Name = "NAme",
                Code = "123"
            };
            await client.PutVoidAsync(user);
        }

        [Fact]
        public async void PutTask()
        {
            var user = new User
            {
                Id = 1,
                Name = "NAme",
                Code = "123"
            };
            await client.PutTaskAsync(user);
        }

        [Fact]
        public async void PutResponseTypeVoid()
        {
            var user = new User
            {
                Id = 1,
                Name = "NAme",
                Code = "123"
            };
            await client.PutResponseTypeVoidAsync(user.Id, user);
        }


    }
}
