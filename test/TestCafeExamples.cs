using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using TechTalk.SpecFlow;
using System.Threading.Tasks;

namespace TestCafeExamples
{
    [Binding]
    public class TestCafeExamplesSteps
    {
        private IPage _page;
        private IBrowser _browser;
        private IPlaywright _playwright;

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });
            _page = await _browser.NewPageAsync();
            await _page.GotoAsync("https://devexpress.github.io/testcafe/example/");
        }

        [AfterScenario]
        public async Task AfterScenario()
        {
            await _browser.CloseAsync();
            _playwright.Dispose();
        }

        [Given(@"I am on the TestCafe example page")]
        public async Task GivenIAmOnTheTestCafeExamplePage()
        {
            var url = await _page.Url;
            url.Should().Be("https://devexpress.github.io/testcafe/example/");
        }

        [When(@"I type '(.*)' into the name input")]
        public async Task WhenITypeIntoTheNameInput(string name)
        {
            await _page.FillAsync("#developer-name", name);
        }

        [Then(@"the name input should contain '(.*)'")]
        public async Task ThenTheNameInputShouldContain(string expectedName)
        {
            var actualName = await _page.InputValueAsync("#developer-name");
            actualName.Should().Be(expectedName);
        }

        [When(@"I click all feature checkboxes")]
        public async Task WhenIClickAllFeatureCheckboxes()
        {
            var features = await _page.QuerySelectorAllAsync(".feature-label");
            foreach (var feature in features)
            {
                await feature.ClickAsync();
            }
        }

        [Then(@"all feature checkboxes should be checked")]
        public async Task ThenAllFeatureCheckboxesShouldBeChecked()
        {
            var checkboxes = await _page.QuerySelectorAllAsync(".feature-input");
            foreach (var checkbox in checkboxes)
            {
                var isChecked = await checkbox.IsCheckedAsync();
                isChecked.Should().BeTrue();
            }
        }

        [When(@"I move the slider to '(.*)'")]
        public async Task WhenIMoveTheSliderTo(string value)
        {
            await _page.FillAsync("#tried-test-cafe", "true");
            await _page.FillAsync("#slider", value);
        }

        [Then(@"the slider value should be greater than the initial value")]
        public async Task ThenTheSliderValueShouldBeGreaterThanTheInitialValue()
        {
            var sliderValue = await _page.InputValueAsync("#slider");
            int.Parse(sliderValue).Should().BeGreaterThan(0);
        }

        [When(@"I select text in the name input from position (.*) to (.*)")]
        public async Task WhenISelectTextInTheNameInputFromPositionTo(int start, int end)
        {
            await _page.FocusAsync("#developer-name");
            await _page.EvaluateAsync($"el => el.setSelectionRange({start}, {end})", await _page.QuerySelectorAsync("#developer-name"));
        }

        [When(@"I press the '(.*)' key")]
        public async Task WhenIPressTheKey(string key)
        {
            await _page.PressAsync("#developer-name", key);
        }

        [When(@"I click the populate button")]
        public async Task WhenIClickThePopulateButton()
        {
            _page.Dialog += (_, dialog) => dialog.AcceptAsync();
            await _page.ClickAsync("#populate");
        }

        [Then(@"a dialog with the text '(.*)' should have been shown")]
        public async Task ThenADialogWithTheTextShouldHaveBeenShown(string expectedText)
        {
            var dialogs = _page.Dialog;
            dialogs.Should().NotBeNull();
        }

        [When(@"I select '(.*)' from the interface select")]
        public async Task WhenISelectFromTheInterfaceSelect(string option)
        {
            await _page.SelectOptionAsync("#preferred-interface", option);
        }

        [Then(@"the selected interface option should be '(.*)'")]
        public async Task ThenTheSelectedInterfaceOptionShouldBe(string expectedOption)
        {
            var selectedOption = await _page.InputValueAsync("#preferred-interface");
            selectedOption.Should().Be(expectedOption);
        }

        [When(@"I fill out the form with name '(.*)'")]
        public async Task WhenIFillOutTheFormWithName(string name)
        {
            await _page.FillAsync("#developer-name", name);
            await _page.ClickAsync("#macos");
            await _page.ClickAsync("#tried-test-cafe");
            await _page.FillAsync("#comments", "It's awesome!!!");
        }

        [When(@"I submit the form")]
        public async Task WhenISubmitTheForm()
        {
            await _page.ClickAsync("#submit-button");
        }

        [Then(@"the result should contain '(.*)'")]
        public async Task ThenTheResultShouldContain(string expectedText)
        {
            var resultText = await _page.TextContentAsync("#article-header");
            resultText.Should().Contain(expectedText);
        }
    }

    [TestClass]
    public class TestCafeExamplesFeature
    {
        [TestMethod]
        public void TextTypingBasics()
        {
            var runner = new SpecFlowProjectRunner();
            var result = runner.RunFeature(@"
                Feature: Text Typing Basics
                Scenario: Type and modify text in input field
                    Given I am on the TestCafe example page
                    When I type 'Peter' into the name input
                    And I type 'Paker' into the name input
                    And I type 'r' into the name input at position 2
                    Then the name input should contain 'Parker'
            ");
            result.Should().Be(0);
        }

        [TestMethod]
        public void ClickArrayOfLabelsAndCheckStates()
        {
            var runner = new SpecFlowProjectRunner();
            var result = runner.RunFeature(@"
                Feature: Click Array of Labels and Check States
                Scenario: Click all feature checkboxes and verify they are checked
                    Given I am on the TestCafe example page
                    When I click all feature checkboxes
                    Then all feature checkboxes should be checked
            ");
            result.Should().Be(0);
        }

        [TestMethod]
        public void DealingWithTextUsingKeyboard()
        {
            var runner = new SpecFlowProjectRunner();
            var result = runner.RunFeature(@"
                Feature: Dealing with Text Using Keyboard
                Scenario: Modify text using keyboard actions
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
            ");
            result.Should().Be(0);
        }

        [TestMethod]
        public void MovingTheSlider()
        {
            var runner = new SpecFlowProjectRunner();
            var result = runner.RunFeature(@"
                Feature: Moving the Slider
                Scenario: Move slider and verify its new position
                    Given I am on the TestCafe example page
                    When I move the slider to '9'
                    Then the slider value should be greater than the initial value
            ");
            result.Should().Be(0);
        }

        [TestMethod]
        public void DealingWithTextUsingSelection()
        {
            var runner = new SpecFlowProjectRunner();
            var result = runner.RunFeature(@"
                Feature: Dealing with Text Using Selection
                Scenario: Modify text using selection and deletion
                    Given I am on the TestCafe example page
                    When I type 'Test Cafe' into the name input
                    And I select text in the name input from position 7 to 1
                    And I press the 'Delete' key
                    Then the name input should contain 'Tfe'
            ");
            result.Should().Be(0);
        }

        [TestMethod]
        public void HandleNativeConfirmationDialog()
        {
            var runner = new SpecFlowProjectRunner();
            var result = runner.RunFeature(@"
                Feature: Handle Native Confirmation Dialog
                Scenario: Trigger and handle a native confirmation dialog
                    Given I am on the TestCafe example page
                    When I click the populate button
                    Then a dialog with the text 'Reset information before proceeding?' should have been shown
                    When I submit the form
                    Then the result should contain 'Peter Parker'
            ");
            result.Should().Be(0);
        }

        [TestMethod]
        public void PickOptionFromSelect()
        {
            var runner = new SpecFlowProjectRunner();
            var result = runner.RunFeature(@"
                Feature: Pick Option from Select
                Scenario: Select an option from a dropdown
                    Given I am on the TestCafe example page
                    When I select 'Both' from the interface select
                    Then the selected interface option should be 'Both'
            ");
            result.Should().Be(0);
        }

        [TestMethod]
        public void FillingAForm()
        {
            var runner = new SpecFlowProjectRunner();
            var result = runner.RunFeature(@"
                Feature: Filling a Form
                Scenario: Fill out and submit a form
                    Given I am on the TestCafe example page
                    When I fill out the form with name 'Bruce Wayne'
                    And I submit the form
                    Then the result should contain 'Bruce Wayne'
            ");
            result.Should().Be(0);
        }
    }
}