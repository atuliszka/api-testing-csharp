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
        private IRestResponse<List<Post>> response;

        [When(@"I request posts")]
        public void WhenIRequestPosts()
        {
            this.response = PostsClient.Get();
        }
        
        [Then(@"the response should contain all posts")]
        public void ThenTheResponseShouldContainAllPosts()
        {
            Assert.IsTrue(response.Data.Count.Equals(100));
        }
    }
}
