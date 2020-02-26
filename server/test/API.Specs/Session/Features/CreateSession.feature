Feature: CreateSession
	In order to create a session
	As a team member
	I want to create a session to run a team barometer cerimony

@mytag
Scenario: Create session successfully
	Given I am creating a session
	When I request the creation
	Then Session should be created successfully