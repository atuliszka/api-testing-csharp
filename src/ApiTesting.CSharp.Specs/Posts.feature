Feature: Posts
		In order to communicate with others
		As a service user
		I want to be able to receive and send posts

Scenario: Requesting all posts
		Given a user is using the posts resource
		When he requests all posts
		Then the response should contain 100 posts
		And all posts should contain required properties

Scenario: Requesting a specific post
		Given a user is using the posts resource
		When he requests any post between 1 and 100
		Then the response should contain corresponding post Id
		And the 'UserId' property should be present
		And the 'Title' property should be present
		And the body of post should be between 1 and 200 characters
