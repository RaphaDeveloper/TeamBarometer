Feature: CreateMeeting
	In order to create a meeting
	As a user
	I want to create a meeting to run a team barometer cerimony

Scenario: Create meeting successfully
	Given I am a user
	When I request the creation
	Then The meeting should be created successfully

Scenario: I am the facilitator
	Given I am a user
	When I request the creation
	Then I should be the facilitator

Scenario: Meeting should has an Id
	Given I am a user
	When I request the creation
	Then The created meeting should has an Id