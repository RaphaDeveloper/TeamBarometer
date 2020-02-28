Feature: CreateSession
	In order to create a session
	As a team member
	I want to create a session to run a team barometer cerimony

Scenario: Create session successfully
	Given I am a user
	When I request the creation
	Then The session should be created successfully

Scenario: I am the facilitator
	Given I am a user
	When I request the creation
	Then I should be the facilitator

Scenario: Session should has an Id
	Given I am a user
	When I request the creation
	Then The created session should has an Id