using System.Collections.Generic;
using ApiTesting.CSharp.Framework.BaseObjects;
using ApiTesting.CSharp.Framework.Models;
using NLog;
using RestSharp;

namespace ApiTesting.CSharp.Framework.Clients
{
    public class PostsObject : RestObjectBase
    {
        private new static readonly RestClient RestClient = new RestClient(GlobalConfiguration.Url);
        private new static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public PostsObject() : base(RestClient, Logger)
        {
        }

        public IRestResponse<List<Post>> Get()
        {
            RestRequest request = new RestRequest("posts", Method.GET);
            IRestResponse<List<Post>> response = Execute<List<Post>>(request);

            return response;
        }

        public static void Post()
        {

        }
    }
}
