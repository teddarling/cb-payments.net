Feature: Get an Error When Retrieving Orders With Invalid Credentials
	In order to know that I need to provide credentials
	As an API user
	I want to receive an error

@mytag
Scenario: No Credentials provided
	Given I try to access the ClickBank Order API without credentials
	When I call the list portion of the orders service
	Then an ArgumentException should occur
	And I should receive an Exception message 'Access Denied'

Scenario: Invalid credentials provided
	Given I try to access the ClickBank Order API with invalid credentials
	When I call the list portion of the orders service
	Then an ArgumentException should occur
	And I should receive an Exception message 'Access Denied'