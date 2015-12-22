using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xunit;

#if NETFX_CORE
using Demo.Client.Universal.WebApiProxy;
using Demo.Model;
using System.Reflection;
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
    public class PropertiesComparer<T> : IEqualityComparer<T>
    {
        public bool Equals(T x, T y)
        {
            foreach (var property in typeof(T).GetProperties())
            {
                var xval = property.GetValue(x);
                var yval = property.GetValue(y);
                if (xval == null && yval == null) continue;

                if (!xval.Equals(yval)) return false;
            }

            return true;
        }

        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }
    }

    public class TestGetMethod
    {
        ITestGetMethodClient client = new TestGetMethodClient(new Uri("http://localhost:36761"));

        private PropertiesComparer<AllDataTypesModel> _allDataComparer = new PropertiesComparer<AllDataTypesModel>();
        private PropertiesComparer<User> _userComparer = new PropertiesComparer<User>();

        #region Single Return values

        [Fact]
        public async void GetSimple()
        {
            var res = await client.GetSimpleAsync();

            Assert.Equal(123, res);
        }

        [Fact]
        public async void GetClass()
        {
            var res = await client.GetClassAsync();

            Assert.Equal(AllDataTypesModel.FilledAllPropertiesInstance, res, new PropertiesComparer<AllDataTypesModel>());
        }

        [Fact]
        public async void GetStructure()
        {
            var res = await client.GetStructureAsync();

            Assert.Equal(987, res.X);
        }

        [Fact]
        public async void GetEnum()
        {
            var res = await client.GetEnumAsync();

            Assert.Equal(EnumModel.Value2, res);
        }

        [Fact]
        public async void GetInretitanceHierarchy()
        {
            var res = await client.GetInretitanceHierarchyAsync();

            var expected = new User
            {
                Id = 1024,
                Name = "My User"
            };

            Assert.Equal(expected, res, new PropertiesComparer<User>());
        }

        [Fact]
        public async void GetCycle()
        {
            var res = await client.GetCycleAsync();

            var expected = new MyTask
            {
                Name = "First",
                ParenTask = new MyTask
                {
                    Name = "Second",
                }
            };

            Assert.Equal(expected.Name, res.Name);
            Assert.Equal(expected.ParenTask.Name, res.ParenTask.Name);
        }

        #endregion

        #region Data structures return values

        [Fact]
        public async void GetIntArray()
        {
            var res = await client.GetIntArrayAsync();

            Assert.Equal(new int[] {1, 2, 3}, res);
        }

        [Fact]
        public async void GetStringList()
        {
            var res = await client.GetStringListAsync();

            Assert.Equal(new List<string> { "1", "2", "3" }, res);
        }

        [Fact]
        public async void GetCustomIEnumerable()
        {
            var res = (await client.GetCustomIEnumerableAsync()).ToList();

            var expected = new List<AllDataTypesModel>
            {
                AllDataTypesModel.FilledAllPropertiesInstance,
                AllDataTypesModel.FilledAllPropertiesInstance,
                AllDataTypesModel.FilledAllPropertiesInstance,
            };

            Assert.Equal(expected.Count, res.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i], res[i], new PropertiesComparer<AllDataTypesModel>());
            }
        }

        [Fact]
        public async void GetCustomArray()
        {
            var res = await client.GetCustomArrayAsync();

            var expected = new AllDataTypesModel[]
            {
                AllDataTypesModel.FilledAllPropertiesInstance,
                AllDataTypesModel.FilledAllPropertiesInstance,
            };

            Assert.Equal(expected.Length, res.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], res[i], new PropertiesComparer<AllDataTypesModel>());
            }
        }

        [Fact]
        public async void GetSimpleDictionary()
        {
            var res = await client.GetSimpleDictionaryAsync();

            var expected = new Dictionary<int, string>
            {
                [1] = "1",
                [2] = "2",
                [3] = "3"
            };

            Assert.Equal(expected, res);
        }

        [Fact]
        public async void GetCustomDictionary()
        {
            var res = await client.GetCustomDictionaryAsync();

            var expected = new Dictionary<long, AllDataTypesModel>
            {
                [1] = AllDataTypesModel.FilledAllPropertiesInstance,
                [2] = AllDataTypesModel.FilledAllPropertiesInstance,
            };
            
            Assert.Equal(2, res.Count);
            Assert.Equal(expected[1], res[1], _allDataComparer);
            Assert.Equal(expected[2], res[2], _allDataComparer);
        }


        #endregion

        #region Params

        [Fact]
        public async void GetWithOneSimpleParam()
        {
            int param = 1024;

            var res = await client.GetWithOneSimpleParamAsync(param);

            Assert.Equal(param + 10, res);
        }

        [Fact]
        public async void GetWithTwoSimpleParam()
        {
            int x = -1024;
            double y = 0.0001;
            var res = await client.GetWithTwoSimpleParamAsync(x, y);

            Assert.Equal(new double[] {x + 10, y + 20}, res);
        }

        [Fact]
        public async void GetWithOneUserParam()
        {
            var user1 = new User
            {
                Id = -12,
                Name = "132"
            };
            var res1 = await client.GetWithOneCustomParamAsync(user1);
            Assert.Equal(user1, res1, _userComparer);

            var user2 = new User
            {
                Code = "Code"
            };
            var res2 = await client.GetWithOneCustomParamAsync(user2);
            Assert.Equal(user2, res2, _userComparer);
        }

        [Fact]
        public async void GetWithOneStringParam()
        {
            var str = "1232";
            var res = await client.GetWithTwoDifferentParamAsync(str, new User {Name = "N"});

            Assert.Equal(str + "1", res);
        }

        [Fact]
        public async void GetWithTwoCustomParam()
        {
            var res = await client.GetWithTwoCustomParamAsync(
                AllDataTypesModel.FilledAllPropertiesInstance, 
                new User { Name = "Name 123", Code = " 1 2 3" }, 
                -1024);

            Assert.Equal("True", res);
        }

        [Fact]
        public async void GetWithNestedArrayParam()
        {
            var filter = new NestedArrayFilter
            {
                Data = "123",
                IntList = new List<int> { 1, 2, 3},
                EnumArray = new EnumModel[] {EnumModel.Value1, EnumModel.Value2, EnumModel.Value1}
            };
            var res = await client.GetWithNestedArrayParamAsync("str", filter);

            Assert.Equal(filter.Data, res.Data);
            Assert.Equal(filter.IntList, res.IntList);
            Assert.Equal(filter.EnumArray, res.EnumArray);
        }
        
        [Fact]
        public async void GetWithSimpleArrayParam()
        {
            var arg = new List<int> {1, 2, 3};
            var res = await client.GetWithSimpleArrayParamAsync(arg.ToArray());
            arg.Add(1);

            Assert.Equal(arg, res);
        }

        [Fact]
        public async void GetWithTwoSimpleArrayParam()
        {
            var numbers = new List<int> { 1, 2, 3 };
            var names = new List<string> {"A", "B", "C"};
            var res = await client.GetWithTwoIEnumerableParamAsync(numbers, names);

            var res1 = new List<string>();
            for (int i = 0; i < names.Count; i++)
            {
                res1.Add(names[i] + numbers[i]);
            }

            Assert.Equal(res1, res);
        }

        #endregion


        #region Params with default values

        [Fact]
        public async void GetOneSimpleWithDefault()
        {
            var res1 = await client.GetOneSimpleWithDefaultAsync();
            Assert.Equal(1024, res1);

            var res2 = await client.GetOneSimpleWithDefaultAsync(-100);
            Assert.Equal(-100, res2);
        }

        [Fact]
        public async void GetManySimpleWithDefault()
        {
            int x1 = 100;
            string s1 = "str 123";
            double d1 = -0.0001;

            var res1 = await client.GetManySimpleWithDefaultAsync(x1, s1, d1);
            Assert.Equal($"{x1}{s1}{d1.ToString(CultureInfo.InvariantCulture)}", res1);

            var res2 = await client.GetManySimpleWithDefaultAsync(x1, s1);
            Assert.Equal($"{x1}{s1}123.456", res2);

            var res3 = await client.GetManySimpleWithDefaultAsync(x1);
            Assert.Equal($"{x1}132123.456", res3);
        }

        #endregion

        /*
        #region Http return value

        [Fact]
        public async void GetIHttpActionResult()
        {
            var res = await client.GetIHttpActionResultAsync();
            Assert.Equal("1000", res);
        }

        [Fact]
        public async void GetHttpResponseMessage()
        {
            var res = await client.GetHttpResponseMessageAsync();
            Assert.Equal("Message", res);
        }


        #endregion
            */

        #region Http return value with ResponseType attribute

        [Fact]
        public async void GetIHttpActionResultWithResponseType()
        {
            var res = await client.GetIHttpActionResultWithResponseTypeAsync();
            Assert.Equal(1024, res);
        }

        [Fact]
        public async void GetHttpResponseMessageWithResponseType()
        {
            var res = await client.GetHttpResponseMessageWithResponseTypeAsync();

            Assert.Equal(AllDataTypesModel.FilledAllPropertiesInstance, res, _allDataComparer);
        }

        #endregion

        #region Task return value

        [Fact]
        public async void GetTaskSimple()
        {
            var res = await client.GetTaskSimpleAsync();
            Assert.Equal(-111, res);
        }

        /*
        [Fact]
        public async void GetTaskHttpResponseMessage()
        {
            var res = await client.GetTaskHttpResponseMessageAsync();
            Assert.Equal("Test", res);
        }
        */

        [Fact]
        public async void GetTaskIHttpActionResultWithResponseType()
        {
            var res = await client.GetTaskIHttpActionResultWithResponseTypeAsync();
            Assert.Equal(AllDataTypesModel.FilledAllPropertiesInstance, res, _allDataComparer);
        }

        #endregion

    }
}
