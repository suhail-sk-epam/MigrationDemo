Feature: TestCafe to SpecFlow Migration

  Scenario: Text typing basics
    Given I have navigated to the test page
    When I type the name 'Peter' into the name input
    When I replace it with 'Parker'
    When I correct it to 'Parker' with a caret position of 2
    Then the name input should contain 'Parker'
