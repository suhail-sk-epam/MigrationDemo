Feature: TestCafe Examples

Scenario: Text typing basics
  Given I am on the TestCafe example page
  When I type 'Peter' into the name input
  And I type 'Paker' into the name input
  And I type 'r' into the name input at position 2
  Then the name input should contain 'Parker'

Scenario: Click an array of labels and check their states
  Given I am on the TestCafe example page
  When I click all feature checkboxes
  Then all feature checkboxes should be checked

Scenario: Dealing with text using keyboard
  Given I am on the TestCafe example page
  When I type 'Peter Parker' into the name input
  And I select text in the name input from position 5 to 5
  And I press the 'Backspace' key
  Then the name input should contain 'Pete Parker'
  When I press the 'Home' key
  And I press the 'ArrowRight' key
  And I type '.'
  And I press the 'Delete' key
  And I press the 'Delete' key
  And I press the 'Delete' key
  Then the name input should contain 'P. Parker'

Scenario: Moving the slider
  Given I am on the TestCafe example page
  When I move the slider to '9'
  Then the slider value should be greater than the initial value

Scenario: Dealing with text using selection
  Given I am on the TestCafe example page
  When I type 'Test Cafe' into the name input
  And I select text in the name input from position 7 to 1
  And I press the 'Delete' key
  Then the name input should contain 'Tfe'

Scenario: Handle native confirmation dialog
  Given I am on the TestCafe example page
  When I click the populate button
  Then a dialog with the text 'Reset information before proceeding?' should have been shown
  When I submit the form
  Then the result should contain 'Peter Parker'

Scenario: Pick option from select
  Given I am on the TestCafe example page
  When I select 'Both' from the interface select
  Then the selected interface option should be 'Both'

Scenario: Filling a form
  Given I am on the TestCafe example page
  When I fill out the form with name 'Bruce Wayne'
  And I submit the form
  Then the result should contain 'Bruce Wayne'