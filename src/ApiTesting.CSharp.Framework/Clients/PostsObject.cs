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

        public IRestResponse<Post> SendPost(Post post)
        {
            RestRequest request = new RestRequest(PostsRoute, Method.POST);
            request.AddParameter("userId", post.UserId);
            request.AddParameter("title", post.Title);
            request.AddParameter("body", post.Body);

            return Execute<Post>(request);
        }

        public IRestResponse<Post> UpdatePost(string postId, string propertyName, string propertyValue)
        {
            RestRequest request = new RestRequest(PostByIdRoute, Method.PATCH);
            request.AddUrlSegment("id", postId);
            request.AddParameter(propertyName, propertyValue);

            return Execute<Post>(request);
        }

        public IRestResponse<Post> ReplacePost(Post post)
        {
            RestRequest request = new RestRequest(PostByIdRoute, Method.PUT);
            request.AddUrlSegment("id", post.Id.ToString());
            request.AddParameter("userId", post.UserId);
            request.AddParameter("title", post.Title);
            request.AddParameter("body", post.Body);

            return Execute<Post>(request);
        }
    }
}
