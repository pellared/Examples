Feature: BowlingGame
	As a Player
	I want to to calculate my scorecard
	In order to know what is my score

Scenario: Sums points for knocked pins by default
	Given a bowling game 
	When the scorecard is -918273645546372819-
	Then the score is 90

Scenario: Two Rolls after Strike are counted twice
	Given a bowling game 
	When the scorecard is X9-9-9-9-9-9-9-9-9-
	Then the score is 100

Scenario: The non-Poinitng Bonus Rolls are counted once
	Given a bowling game 
	When the scorecard is 9-9-9-9-9-9-9-9-9-X11
	Then the score is 93