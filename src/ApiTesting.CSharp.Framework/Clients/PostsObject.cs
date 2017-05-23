using System.Collections.Generic;
using ApiTesting.CSharp.Framework.BaseObjects;
using ApiTesting.CSharp.Framework.Models;
using NLog;
using RestSharp;

namespace ApiTesting.CSharp.Framework.Clients
{
    public class PostsObject : ResourceObject
    {
        private const string GetAllPostsPath = "posts";
        private const string GetPostPath = "posts/{id}";

        private new static readonly RestClient RestClient = new RestClient(GlobalConfiguration.Url);
        private new static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public PostsObject() : base(RestClient, Logger)
        {
        }

        public IRestResponse<List<Post>> GetAllPosts()
        {
            RestRequest request = new RestRequest(GetAllPostsPath, Method.GET);
            IRestResponse<List<Post>> response = Execute<List<Post>>(request);

            return response;
        }

        public IRestResponse<Post> GetPost(string postId)
        {
            RestRequest request = new RestRequest(GetPostPath, Method.GET);
            request.AddUrlSegment("id", postId);
            IRestResponse<Post> response = Execute<Post>(request);

            return response;
        }

        public static void Post()
        {

        }
    }
}
