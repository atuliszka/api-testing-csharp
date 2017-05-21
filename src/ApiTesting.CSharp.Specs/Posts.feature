Feature: Posts
		In order to communicate with other users
		As a service user
		I want to be able to receive and send posts

Scenario: Get posts
		When I request posts
		Then the response should contain all posts