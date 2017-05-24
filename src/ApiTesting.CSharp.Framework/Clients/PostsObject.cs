using System.Collections.Generic;
using ApiTesting.CSharp.Framework.BaseObjects;
using ApiTesting.CSharp.Framework.Models;
using NLog;
using RestSharp;

namespace ApiTesting.CSharp.Framework.Clients
{
    public class PostsObject : ResourceObject
    {
        private const string PostsRoute = "posts";
        private const string PostByIdRoute = "posts/{id}";

        private new static readonly RestClient RestClient = new RestClient(GlobalConfiguration.Url);
        private new static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public PostsObject() : base(RestClient, Logger)
        {
        }

        public IRestResponse<List<Post>> GetPosts()
        {
            RestRequest request = new RestRequest(PostsRoute, Method.GET);

            return Execute<List<Post>>(request);
        }

        public IRestResponse<List<Post>> GetPosts(string userId)
        {
            RestRequest request = new RestRequest(PostsRoute, Method.GET);
            request.AddParameter("userId", userId);

            return Execute<List<Post>>(request);
        }

        public IRestResponse<Post> GetPost(string postId)
        {
            RestRequest request = new RestRequest(PostByIdRoute, Method.GET);
            request.AddUrlSegment("id", postId);

            return Execute<Post>(request);
        }

        public IRestResponse<Post> SendPost(int userId, string title, string body)
        {
            RestRequest request = new RestRequest(PostsRoute, Method.POST);
            request.AddParameter("userId", userId);
            request.AddParameter("title", title);
            request.AddParameter("body", body);

            return Execute<Post>(request);
        }

    }
}
