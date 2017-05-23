using System;
using System.Collections.Generic;
using ApiTesting.CSharp.Framework.Clients;
using ApiTesting.CSharp.Framework.Models;
using RestSharp;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace ApiTesting.CSharp.Specs
{
    [Binding]
    public class PostsSteps
    {
        private PostsObject PostsObject;
        private int ExpectedPostId;

        [Given(@"a user is using the posts resource")]
        public void GivenAUserIsUsingThePostsResource()
        {
            PostsObject = new PostsObject();
        }

        [When(@"he requests all posts")]
        public void WhenHeRequestsAllPosts()
        {
            ScenarioContext.Current.Add("Response", PostsObject.GetAllPosts());
        }

        [Then(@"the response should contain (.*) posts")]
        public void ThenTheResponseShouldContainPosts(int postsCount)
        {
            var response = ScenarioContext.Current.Get<IRestResponse<List<Post>>>("Response");
            Assert.IsTrue(response.Data.Count.Equals(postsCount));
        }

        [Then(@"all posts should contain required properties")]
        public void ThenAllPostsShouldContainRequiredProperties()
        {
            var response = ScenarioContext.Current.Get<IRestResponse<List<Post>>>("Response");
            foreach (var post in response.Data)
            {
                Assert.Multiple(() =>
                {
                    Assert.IsNotNull(post.Id);
                    Assert.IsNotNull(post.UserId);
                    Assert.IsNotNull(post.Title);
                    Assert.IsNotNull(post.Body);
                });
            }
        }

        [When(@"he requests any post between (.*) and (.*)")]
        public void WhenHeRequestsAnyPostBetweenAnd(int minPostId, int maxPostId)
        {
            ExpectedPostId = new Random().Next(minPostId, maxPostId + 1);
            ScenarioContext.Current.Add("Response", PostsObject.GetPost(ExpectedPostId.ToString()));
        }

        [Then(@"the response should contain corresponding post Id")]
        public void ThenTheResponseShouldContainCorrectId()
        {
            var postId = ScenarioContext.Current.Get<IRestResponse<Post>>("Response");
            Assert.AreEqual(ExpectedPostId, postId.Data.Id);
        }

        [Then(@"the '(.*)' property should be present")]
        public void ThenTheUserIdPropertyShouldBePresent(string propertyName)
        {
            var response = ScenarioContext.Current.Get<IRestResponse<Post>>("Response");
            Assert.IsNotEmpty(GetPropValue(response.Data, propertyName).ToString());
        }

        private static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        [Then(@"the body of post should be between (.*) and (.*) characters")]
        public void ThenTheBodyOfPostShouldBeBetweenAndCharacters(int minChars, int maxChars)
        {
            var response = ScenarioContext.Current.Get<IRestResponse<Post>>("Response");
            Assert.That(response.Data.Body.Length, Is.InRange(minChars, maxChars));
        }
    }
}
