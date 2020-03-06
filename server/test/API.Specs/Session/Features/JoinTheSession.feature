Feature: JoinTheSession
	In order to join the session
	As a user
	I want to participate to a team barometer cerimony

Scenario: Join the session successfully
	Given I am a user
	And The session was created
	When I request to join the session
	Then I should join the session successfully

Scenario: I am not the facilitator
	Given I am a user
	And The session was created
	When I request to join the session
	Then I should not be the facilitator
