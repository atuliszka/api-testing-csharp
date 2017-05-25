using System;
using System.Collections.Generic;
using System.Reflection;
using ApiTesting.CSharp.Framework.Clients;
using ApiTesting.CSharp.Framework.Models;
using RestSharp;
using TechTalk.SpecFlow;
using NUnit.Framework;
using TechTalk.SpecFlow.Assist;

namespace ApiTesting.CSharp.Specs
{
    [Binding]
    public class PostsSteps
    {
        private UserContext UserContext;
        private PostsObject PostsObject;

        public PostsSteps(UserContext userContext)
        {
            UserContext = userContext;
        }

        [Given(@"a user is using the posts resource")]
        public void GivenAUserIsUsingThePostsResource()
        {
            PostsObject = new PostsObject();
        }

        [When(@"he requests all posts")]
        public void WhenHeRequestsAllPosts()
        {
            ScenarioContext.Current.Add("Response", PostsObject.GetPosts());
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
            UserContext.Post.Id = GetRandomIntBetween(minPostId, maxPostId);
            ScenarioContext.Current.Add("Response", PostsObject.GetPost(UserContext.Post.Id.ToString()));
        }

        [Then(@"the response should contain corresponding post Id")]
        public void ThenTheResponseShouldContainCorrectId()
        {
            var postId = ScenarioContext.Current.Get<IRestResponse<Post>>("Response");
            Assert.AreEqual(UserContext.Post.Id, postId.Data.Id);
        }

        [Then(@"the '(.*)' property should be present in post")]
        public void ThenTheUserIdPropertyShouldBePresent(string propertyName)
        {
            var response = ScenarioContext.Current.Get<IRestResponse<Post>>("Response");
            Assert.IsNotEmpty(GetPropertyValue(response.Data, propertyName).ToString());
        }

        [Then(@"the body of post should be between (.*) and (.*) characters")]
        public void ThenTheBodyOfPostShouldBeBetweenAndCharacters(int minChars, int maxChars)
        {
            var response = ScenarioContext.Current.Get<IRestResponse<Post>>("Response");
            Assert.That(response.Data.Body.Length, Is.InRange(minChars, maxChars));
        }

        [When(@"he requests posts of a user with Id between (.*) and (.*)")]
        public void WhenHeRequestsPostsOfAUserWithIdBetweenAnd(int minUserId, int maxUserId)
        {
            UserContext.Post.UserId = GetRandomIntBetween(minUserId, maxUserId);
            ScenarioContext.Current.Add("Response", PostsObject.GetPosts(UserContext.Post.UserId.ToString()));
        }

        [Given(@"his userId is between (.*) and (.*)")]
        public void GivenHisUserIdIsBetweenAnd(int minUserId, int maxUserId)
        {
            UserContext.Post.UserId = GetRandomIntBetween(minUserId, maxUserId);
        }

        [When(@"he sends a post with following title and body:")]
        public void WhenHeSendsAPostWithFollowingTitleAndBody(Table table)
        {
            UserContext.Post = table.CreateInstance<Post>();
            ScenarioContext.Current.Add("Response", PostsObject.SendPost(UserContext.Post));
        }

        [Then(@"response contains post Id '(.*)'")]
        public void ThenTheResponseContainsPostId(int postId)
        {
            var response = ScenarioContext.Current.Get<IRestResponse<Post>>("Response");
            Assert.AreEqual(response.Data.Id, postId);
        }

        [Then(@"response contains correct title, body and userId")]
        public void ThenCorrectAndUserId()
        {
            var response = ScenarioContext.Current.Get<IRestResponse<Post>>("Response");
            Assert.Multiple(() =>
                {
                    Assert.AreEqual(response.Data.UserId, UserContext.Post.UserId);
                    Assert.AreEqual(response.Data.Title, UserContext.Post.Title);
                    Assert.AreEqual(response.Data.Body, UserContext.Post.Body);
                });
        }

        [When(@"he sends a new '(.*)' with value '(.*)' for any post between (.*) and (.*)")]
        public void WhenHeSendsANewForAnyPostBetweenAnd(
            string propertyName, string propertyValue, int minPostId, int maxPostId)
        {
            UserContext.Post.Id = GetRandomIntBetween(minPostId, maxPostId);
            TrySetProperty(UserContext.Post, propertyName, propertyValue);
            ScenarioContext.Current.Add("Response", PostsObject.UpdatePost(
                UserContext.Post.Id.ToString(), propertyName, "newProperty"));
        }

        [Then(@"response contains new '(.*)'")]
        public void ThenTheResponseContainsNew(string propertyName)
        {
            var response = ScenarioContext.Current.Get<IRestResponse<Post>>("Response");
            Assert.AreEqual(GetPropertyValue(response, propertyName), GetPropertyValue(UserContext.Post, propertyName));
        }

        [When(@"he replaces a post between (.*) and (.*) with values:")]
        public void WhenHeReplacesAPostBetweenAndWithValues(int minPostId, int maxPostId, Table table)
        {
            UserContext.Post = table.CreateInstance<Post>();
            UserContext.Post.Id = GetRandomIntBetween(minPostId, maxPostId);
            ScenarioContext.Current.Add("Response", PostsObject.ReplacePost(UserContext.Post));
        }

        [When(@"he deletes a post between (.*) and (.*)")]
        public void WhenHeDeletesAPostBetweenAnd(int minPostId, int maxPostId)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"response has (.*) status code")]
        public void ThenTheResponseHasStatusCode(int expectedStatusCode)
        {
            ScenarioContext.Current.Pending();
        }

        private static int GetRandomIntBetween(int min, int max)
        {
            var random = new Random();
            return random.Next(min, max);
        }

        private static object GetPropertyValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName)?.GetValue(src, null);
        }

        private static void TrySetProperty(object obj, string property, object value)
        {
            var prop = obj.GetType().GetProperty(property, BindingFlags.Public | BindingFlags.Instance);
            if (prop != null && prop.CanWrite)
                prop.SetValue(obj, value, null);
        }
    }
}
