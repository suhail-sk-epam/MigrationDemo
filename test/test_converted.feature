Feature: TestCafe to SpecFlow Conversion

  Scenario: Text typing basics
    Given I type the name 'Peter'
    When I replace it with 'Parker'
    Then the name should be 'Parker'

  # Additional scenarios can be added here following the same pattern