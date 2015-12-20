﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proxy;
using Xunit;

namespace Demo.Client.WPF
{
    public class TestDeleteMethod
    {
        ITestDeleteMethodClient client = new TestDeleteMethodClient(new Uri("http://localhost:36761"));

        private PropertiesComparer<DataAnnotationsModel> _formatPropertiesComparer = new PropertiesComparer<DataAnnotationsModel>();

        [Fact]
        public async void Delete()
        {
            await client.DeleteAsync(5);
        }

        [Fact]
        public async void DeleteArray()
        {
            var array = new string[] {"1", "2", "3"};
            var res = await client.DeleteArrayAsync(array);

            Assert.Equal(3, res);
        }

        [Fact]
        public async void DeleteDateTimeOffset()
        {
            var d = new DateTimeOffset(2015, 12, 31, 23, 59, 59, TimeSpan.FromHours(5));
            var res = await client.DeleteDateTimeOffsetAsync(d);

            Assert.Equal(d, res);
        }

        [Fact]
        public async void DeleteDateTime()
        {
            var d = new DateTime(2015, 12, 31, 23, 59, 59);
            var res = await client.DeleteDateTimeAsync(d);

            Assert.Equal(d, res);
        }

        [Fact]
        public async void DeleteFormatPropertyModel()
        {
            var model = new DataAnnotationsModel
            {
                DateTime = new DateTime(2015, 12, 31, 23, 59, 59),
                DateTimeOffset = new DateTimeOffset(2015, 12, 31, 23, 59, 59, TimeSpan.FromHours(5)),
            };

            var res = await client.DeleteDataAnnotationsModelAsync(model);

            Assert.Equal(model, res, _formatPropertiesComparer);
        }
    }
}
