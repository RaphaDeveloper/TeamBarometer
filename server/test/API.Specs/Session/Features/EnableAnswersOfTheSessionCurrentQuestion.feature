Feature: EnableAnswersOfTheSessionCurrentQuestion
	In order to start the answering
	As a facilitator
	I want to enable the answer of the session current question

Scenario: Notify the users
	Given I am a user
	And The session was created
	When Enable the answers of the session current question
	Then The users should be notified