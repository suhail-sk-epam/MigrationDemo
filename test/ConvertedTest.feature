Feature: Converted Test

  Scenario: Text typing basics
    Given I navigate to the TestCafe example page
    When I type the name 'Peter' into the name input
    And I replace the name with 'Parker'
    And I correct the name to 'Parker'
    Then the name input should contain 'Parker'

  Scenario: Click an array of labels and then check their states
    Given I navigate to the TestCafe example page
    When I click all feature checkboxes
    Then all feature checkboxes should be checked

  Scenario: Dealing with text using keyboard
    Given I navigate to the TestCafe example page
    When I type 'Peter Parker' into the name input and move the caret position
    And I press the backspace key
    Then the name input should contain 'Pete Parker'
    When I press the home, right, dot, delete, delete, delete keys
    Then the name input should contain 'P. Parker'

  Scenario: Moving the slider
    Given I navigate to the TestCafe example page
    When I move the slider to 9

  Scenario: Dealing with text using selection
    Given I navigate to the TestCafe example page
    When I type 'Test Cafe' into the name input and select text
    And I press the delete key
    Then the name input should contain 'Tfe'

  Scenario: Handle native confirmation dialog
    Given I navigate to the TestCafe example page
    When I handle the native confirmation dialog
    And I click the submit button
    Then the results should contain 'Peter Parker'

  Scenario: Pick option from select
    Given I navigate to the TestCafe example page
    When I select 'Both' from the interface select
    Then the interface select should have 'Both' selected

  Scenario: Filling a form
    Given I navigate to the TestCafe example page
    When I fill the form with 'Bruce Wayne' and submit
    Then the results should contain 'Bruce Wayne'