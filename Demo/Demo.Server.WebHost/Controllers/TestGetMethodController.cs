using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;
using System.Web.Http.Results;
using Demo.Model;
using WebApiProxy.Common.DataAnnotations;

namespace Demo.Server.WebHost.Controllers
{
    //[ProxyIgnore]
    [RoutePrefix("api/TestGetMethod")]
    public class TestGetMethodController : ApiController
    {
        #region Single Return values

        [Route("GetSimple")]
        public int GetSimple()
        {
            return 123;
        }

        [Route("GetClass")]
        public AllDataTypesModel GetClass()
        {
            return AllDataTypesModel.FilledAllPropertiesInstance;
        }

        [Route("GetStructure")]
        public MyPointStruct GetStructure()
        {
            return new MyPointStruct
            {
                X = 987
            };
        }

        [Route("GetEnum")]
        public EnumModel GetEnum()
        {
            return EnumModel.Value2;
        }

        [Route("GetInretitanceHierarchy")]
        public User GetInretitanceHierarchy()
        {
            return new User
            {
                Id = 1024,
                Name = "My User"
            };
        }

        [Route("GetCycle")]
        public MyTask GetCycle()
        {
            return new MyTask
            {
                Name = "First",
                ParenTask = new MyTask
                {
                    Name = "Second",
                }
            };
        }

        #endregion

        #region Data structures return values

        [Route("GetIntArray")]
        public int[] GetIntArray()
        {
            return new[] { 1, 2, 3 };
        }

        [Route("GetStringList")]
        public List<string> GetStringList()
        {
            return new List<string>
            {
                "1", "2", "3"
            };
        }


        [Route("GetCustomIEnumerable")]
        public IEnumerable<AllDataTypesModel> GetCustomIEnumerable()
        {
            return new List<AllDataTypesModel>
            {
                AllDataTypesModel.FilledAllPropertiesInstance,
                AllDataTypesModel.FilledAllPropertiesInstance,
                AllDataTypesModel.FilledAllPropertiesInstance,
            };
        }

        [Route("GetCustomArray")]
        public AllDataTypesModel[] GetCustomArray()
        {
            return new AllDataTypesModel[]
            {
                AllDataTypesModel.FilledAllPropertiesInstance,
                AllDataTypesModel.FilledAllPropertiesInstance,
            };
        }

        [Route("GetSimpleDictionary")]
        public Dictionary<int, string> GetSimpleDictionary()
        {
            return new Dictionary<int, string>
            {
                [1] = "1",
                [2] = "2",
                [3] = "3"
            };
        }

        [Route("GetCustomDictionary")]
        public Dictionary<long, AllDataTypesModel> GetCustomDictionary()
        {
            return new Dictionary<long, AllDataTypesModel>
            {
                [1] = AllDataTypesModel.FilledAllPropertiesInstance,
                [2] = AllDataTypesModel.FilledAllPropertiesInstance,
            };
        }

        #endregion

        #region Params

        [Route("GetWithOneSimpleParam")]
        public int GetWithOneSimpleParam(int x)
        {
            return x + 10;
        }

        [Route("GetWithTwoSimpleParam")]
        public double[] GetWithTwoSimpleParam(int x, double y)
        {
            return new double[] { x + 10, y + 20 };
        }

        [Route("GetWithOneCustomParam")]
        public User GetWithOneCustomParam([FromUri]User u)
        {
            return u;
        }

        [Route("GetWithTwoDiffenentStringParam")]
        public string GetWithTwoDifferentParam(string str, [FromUri]User u)
        {
            return str + "1";
        }

        [Route("GetWithNestedArrayParam")]
        public NestedArrayFilter GetWithNestedArrayParam(string str, [FromUri]NestedArrayFilter filter)
        {
            return filter;
        }
        
        [Route("GetWithTwoCustomParam")]
        public string GetWithTwoCustomParam([FromUri]AllDataTypesModel x, [FromUri]User u, int intParam)
        {
            var e1 = new PropertiesComparer<AllDataTypesModel>().Equals(x, AllDataTypesModel.FilledAllPropertiesInstance);
            var e2 = new PropertiesComparer<User>().Equals(u, new User { Name = "Name 123", Code = " 1 2 3" });
            var e3 = intParam == -1024;
            return (e1 && e2 && e3).ToString();
        }

        [Route("GetWithSimpleArrayParam")]
        public List<int> GetWithSimpleArrayParam([ModelBinder]int[] numbers)
        {
            var res = numbers.ToList();
            res.Add(1);
            return res;
        }

        [Route("GetWithTwoSimpleArrayParam")]
        public List<string> GetWithTwoIEnumerableParam([ModelBinder]IEnumerable<int> numbers, [ModelBinder]List<string> names)
        {
            var res = new List<string>();
            var numList = numbers.ToList();
            for (int i = 0; i < names.Count; i++)
            {
                res.Add(names[i] + numList[i]);
            }
            return res;
        }

        #endregion

        #region Params with default values

        [Route("GetOneSimpleWithDefault")]
        public int GetOneSimpleWithDefault(int x = 1024)
        {
            return x;
        }

        [Route("GetManySimpleWithDefault")]
        public string GetManySimpleWithDefault(int x, string str = "132", double d = 123.456)
        {
            return $"{x}{str}{d.ToString(CultureInfo.InvariantCulture)}";
        }

        #endregion

        #region Http return value

        [Route("GetIHttpActionResult")]
        public IHttpActionResult GetIHttpActionResult()
        {
            return Ok(1000);
        }

        [Route("GetHttpResponseMessage")]
        public HttpResponseMessage GetHttpResponseMessage()
        {
            return Request.CreateResponse(HttpStatusCode.Accepted, "Message");
        }

        #endregion

        #region Http return value with ResponseType attribute

        [Route("GetIHttpActionResultWithResponseType")]
        [ResponseType(typeof(int))]
        public IHttpActionResult GetIHttpActionResultWithResponseType()
        {
            return Ok(1024);
        }

        [Route("GetHttpResponseMessageWithResponseType")]
        [ResponseType(typeof(AllDataTypesModel))]
        public HttpResponseMessage GetHttpResponseMessageWithResponseType()
        {
            return Request.CreateResponse(AllDataTypesModel.FilledAllPropertiesInstance);
        }

        #endregion

        #region Task return value

        [Route("GetTaskSimple")]
        public Task<int> GetTaskSimple()
        {
            return Task.FromResult(-111);
        }

        [Route("GetTaskHttpResponseMessage")]
        public async Task<HttpResponseMessage> GetTaskHttpResponseMessage()
        {
            return Request.CreateResponse("Test");
        }

        [Route("GetTaskIHttpActionResultWithResponseType")]
        [ResponseType(typeof(AllDataTypesModel))]
        public Task<IHttpActionResult> GetTaskIHttpActionResultWithResponseType()
        {
            IHttpActionResult res;
            res = Ok(AllDataTypesModel.FilledAllPropertiesInstance);
            return Task.FromResult(res);
        }
        #endregion

    }

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
}