Feature: Converted Test

  Scenario: Text typing basics
    Given I navigate to the example page
    When I type the name 'Peter' in the name input
    And I type the name 'Paker' in the name input
    And I type the name 'r' in the name input
    Then the name input should have value 'Parker'

  Scenario: Click an array of labels and then check their states
    Given I navigate to the example page
    When I click the 'remote-testing' feature
    Then the 'remote-testing' feature should be checked

  Scenario: Dealing with text using keyboard
    Given I navigate to the example page
    When I type the name 'Peter Parker' in the name input
    And I press the 'backspace' key
    Then the name input should have value 'Pete Parker'
    When I press the 'home right . delete delete delete' key
    Then the name input should have value 'P. Parker'

  Scenario: Moving the slider
    Given I navigate to the example page
    When I move the slider to '9'
    Then the slider value should be greater than '0'

  Scenario: Dealing with text using selection
    Given I navigate to the example page
    When I type the name 'Test Cafe' in the name input
    And I select text from '7' to '1'
    And I press the 'delete' key
    Then the name input should have value 'Tfe'

  Scenario: Handle native confirmation dialog
    Given I navigate to the example page
    When I handle the native confirmation dialog
    Then the dialog text should be 'Reset information before proceeding?'

  Scenario: Pick option from select
    Given I navigate to the example page
    When I pick the 'Both' option from the select
    Then the select value should be 'Both'

  Scenario: Filling a form
    Given I navigate to the example page
    When I fill the form with name 'Bruce Wayne', OS 'macos', and comments 'awesome!!!'
    And I submit the form
    Then the results should contain 'Bruce Wayne'