using System.Collections.Generic;
using ApiTesting.CSharp.Framework.Clients;
using ApiTesting.CSharp.Framework.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using TechTalk.SpecFlow;

namespace ApiTesting.CSharp.Specs
{
    [Binding]
    public class PostsSteps
    {
        private IRestResponse<List<Post>> Response;
        private readonly PostsObject PostsObject = new PostsObject();

        [When(@"I request all posts")]
        public void WhenIRequestPosts()
        {
            Response = PostsObject.Get();
        }
        
        [Then(@"the response should contain all posts")]
        public void ThenTheResponseShouldContainAllPosts()
        {
            Assert.IsTrue(Response.Data.Count.Equals(100));
        }
    }
}
