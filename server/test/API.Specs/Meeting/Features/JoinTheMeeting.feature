Feature: JoinTheMeeting
	In order to join the meeting
	As a user
	I want to participate to a team barometer cerimony

Scenario: Join the meeting successfully
	Given I am a user
	And The meeting was created
	When I request to join the meeting
	Then I should join the meeting successfully

Scenario: I am not the facilitator
	Given I am a user
	And The meeting was created
	When I request to join the meeting
	Then I should not be the facilitator
