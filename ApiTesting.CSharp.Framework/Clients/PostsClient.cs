using System.Collections.Generic;
using ApiTesting.CSharp.Framework.Models;
using RestSharp;

namespace ApiTesting.CSharp.Framework.Clients
{
    public static class PostsClient
    {
        private static readonly RestClient Client = new RestClient(GlobalConfiguration.Url);

        public static IRestResponse<List<Post>> Get()
        {
            RestRequest request = new RestRequest("posts", Method.GET);
            IRestResponse<List<Post>> response = Client.ExecuteAndLog<List<Post>>(request);

            return response;
        }

        public static void Post()
        {

        }
    }
}
