Feature: Posts
		In order to communicate with others
		As a user of JSONPlaceholder service
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
		And the 'UserId' property should be present in post
		And the 'Title' property should be present in post
		And the body of post should be between 1 and 250 characters

Scenario: Requesting posts of a specific user
		Given a user is using the posts resource
		When he requests posts of a user with Id between 1 and 10
		Then the response should contain 10 posts
		And all posts should contain required properties

Scenario: Sending a post
		Given a user is using the posts resource
		And his userId is between 1 and 100
		When he sends a post with following title and body:
| Title                     | Body                                       |
| An interesting post title | Completely reasonable argument in the body |
		Then response contains post Id '101'
		And response contains correct title, body and userId

Scenario: Updating a post body
		Given a user is using the posts resource
		When he sends a new 'body' with value 'new body' for any post between 1 and 100
		Then response contains new 'body'
		And response has 200 status code

Scenario: Updating a post title
		Given a user is using the posts resource
		When he sends a new 'title' with value 'new title' for any post between 1 and 100
		Then response contains new 'title'
		And response has 200 status code

Scenario: Replacing a post
		Given a user is using the posts resource
		When he replaces a post between 1 and 100 with values:
| UserId | Title                     | Body                                       |
| 15     | An interesting post title | Completely reasonable argument in the body |
		Then response contains correct title, body and userId

Scenario: Deleting a post
		Given a user is using the posts resource
		When he deletes a post between 1 and 100
		Then response has 200 status code