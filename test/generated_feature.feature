Feature: TestCafe to SpecFlow Migration

Scenario: Text typing basics
	Given I navigate to "https://devexpress.github.io/testcafe/example/"
	When I type "Peter" into the "nameInput"
	And I replace the text with "Paker"
	And I correct the text to "Parker"
	Then the "nameInput" should have value "Parker"

Scenario: Click an array of labels and then check their states
	Given I navigate to "https://devexpress.github.io/testcafe/example/"
	When I click on each label in the "featureList"
	Then each "checkbox" should be checked

Scenario: Dealing with text using keyboard
	Given I navigate to "https://devexpress.github.io/testcafe/example/"
	When I type "Peter Parker" into the "nameInput"
	And I move the caret position to 5
	And I press the "backspace" key
	Then the "nameInput" should have value "Pete Parker"
	When I press the keys "home right . delete delete delete"
	Then the "nameInput" should have value "P. Parker"

Scenario: Moving the slider
	Given I navigate to "https://devexpress.github.io/testcafe/example/"
	When I click on the "triedTestCafeCheckbox"
	And I drag the "slider.handle" to the "slider.tick" with text "9"
	Then the "slider.handle" should have moved

Scenario: Dealing with text using selection
	Given I navigate to "https://devexpress.github.io/testcafe/example/"
	When I type "Test Cafe" into the "nameInput"
	And I select the text from position 7 to 1
	And I press the "delete" key
	Then the "nameInput" should have value "Tfe"

Scenario: Handle native confirmation dialog
	Given I navigate to "https://devexpress.github.io/testcafe/example/"
	When I set the native dialog handler to accept
	And I click on the "populateButton"
	Then the native dialog history should contain "Reset information before proceeding?"
	When I click on the "submitButton"
	Then the "results" should contain "Peter Parker"

Scenario: Pick option from select
	Given I navigate to "https://devexpress.github.io/testcafe/example/"
	When I click on the "interfaceSelect"
	And I select the option "Both"
	Then the "interfaceSelect" should have value "Both"

Scenario: Filling a form
	Given I navigate to "https://devexpress.github.io/testcafe/example/"
	When I type "Bruce Wayne" into the "nameInput"
	And I click on the "macOSRadioButton"
	And I click on the "triedTestCafeCheckbox"
	And I type "It's..." into the "commentsTextArea"
	And I wait for 500 milliseconds
	And I type "\ngood" into the "commentsTextArea"
	And I wait for 500 milliseconds
	And I select the text area content from position 1 to 0
	And I press the "delete" key
	And I type "awesome!!!" into the "commentsTextArea"
	And I wait for 500 milliseconds
	And I click on the "submitButton"
	Then the "results" should contain "Bruce Wayne"