using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using TechTalk.SpecFlow;
using Microsoft.Playwright;
using System.Threading.Tasks;

namespace TestProject
{
    [Binding]
    public class TestSteps
    {
        private readonly IPage _page;
        private readonly IBrowser _browser;
        private readonly IBrowserContext _context;

        public TestSteps()
        {
            var playwright = Playwright.CreateAsync().Result;
            _browser = playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false }).Result;
            _context = _browser.NewContextAsync().Result;
            _page = _context.NewPageAsync().Result;
        }

        [Given(@"I navigate to the TestCafe example page")]
        public async Task GivenINavigateToTheTestCafeExamplePage()
        {
            await _page.GotoAsync("https://devexpress.github.io/testcafe/example/");
        }

        [When(@"I type the name 'Peter' into the name input")]
        public async Task WhenITypeTheNamePeterIntoTheNameInput()
        {
            await _page.FillAsync("#developer-name", "Peter");
        }

        [When(@"I replace the name with 'Parker'")]
        public async Task WhenIReplaceTheNameWithParker()
        {
            await _page.FillAsync("#developer-name", "Parker");
        }

        [When(@"I correct the name to 'Parker'")]
        public async Task WhenICorrectTheNameToParker()
        {
            await _page.FillAsync("#developer-name", "Parker");
        }

        [Then(@"the name input should contain 'Parker'")]
        public async Task ThenTheNameInputShouldContainParker()
        {
            var value = await _page.InputValueAsync("#developer-name");
            value.Should().Be("Parker");
        }

        [When(@"I click all feature checkboxes")]
        public async Task WhenIClickAllFeatureCheckboxes()
        {
            var features = await _page.QuerySelectorAllAsync(".feature");
            foreach (var feature in features)
            {
                await feature.ClickAsync();
            }
        }

        [Then(@"all feature checkboxes should be checked")]
        public async Task ThenAllFeatureCheckboxesShouldBeChecked()
        {
            var features = await _page.QuerySelectorAllAsync(".feature");
            foreach (var feature in features)
            {
                var isChecked = await feature.IsCheckedAsync();
                isChecked.Should().BeTrue();
            }
        }

        [When(@"I type 'Peter Parker' into the name input and move the caret position")]
        public async Task WhenITypePeterParkerIntoTheNameInputAndMoveTheCaretPosition()
        {
            await _page.FillAsync("#developer-name", "Peter Parker");
            await _page.ClickAsync("#developer-name", new ClickOptions { Position = new Position { X = 5, Y = 5 } });
        }

        [When(@"I press the backspace key")]
        public async Task WhenIPressTheBackspaceKey()
        {
            await _page.PressAsync("#developer-name", "Backspace");
        }

        [Then(@"the name input should contain 'Pete Parker'")]
        public async Task ThenTheNameInputShouldContainPeteParker()
        {
            var value = await _page.InputValueAsync("#developer-name");
            value.Should().Be("Pete Parker");
        }

        [When(@"I press the home, right, dot, delete, delete, delete keys")]
        public async Task WhenIPressTheHomeRightDotDeleteDeleteDeleteKeys()
        {
            await _page.PressAsync("#developer-name", "Home");
            await _page.PressAsync("#developer-name", "ArrowRight");
            await _page.PressAsync("#developer-name", ".");
            await _page.PressAsync("#developer-name", "Delete");
            await _page.PressAsync("#developer-name", "Delete");
            await _page.PressAsync("#developer-name", "Delete");
        }

        [Then(@"the name input should contain 'P. Parker'")]
        public async Task ThenTheNameInputShouldContainPParker()
        {
            var value = await _page.InputValueAsync("#developer-name");
            value.Should().Be("P. Parker");
        }

        [When(@"I move the slider to 9")]
        public async Task WhenIMoveTheSliderTo9()
        {
            var slider = await _page.QuerySelectorAsync("#slider");
            var initialOffset = await slider.EvaluateAsync<int>("slider => slider.offsetLeft");
            await _page.DragAndDropAsync("#slider", "#tick-9");
            var newOffset = await slider.EvaluateAsync<int>("slider => slider.offsetLeft");
            newOffset.Should().BeGreaterThan(initialOffset);
        }

        [When(@"I type 'Test Cafe' into the name input and select text")]
        public async Task WhenITypeTestCafeIntoTheNameInputAndSelectText()
        {
            await _page.FillAsync("#developer-name", "Test Cafe");
            await _page.SelectTextAsync("#developer-name", 1, 7);
        }

        [When(@"I press the delete key")]
        public async Task WhenIPressTheDeleteKey()
        {
            await _page.PressAsync("#developer-name", "Delete");
        }

        [Then(@"the name input should contain 'Tfe'")]
        public async Task ThenTheNameInputShouldContainTfe()
        {
            var value = await _page.InputValueAsync("#developer-name");
            value.Should().Be("Tfe");
        }

        [When(@"I handle the native confirmation dialog")]
        public async Task WhenIHandleTheNativeConfirmationDialog()
        {
            await _page.Dialog += async (_, dialog) => await dialog.AcceptAsync();
            await _page.ClickAsync("#populate");
            var dialogHistory = await _page.EvaluateAsync<string[]>("window.dialogHistory");
            dialogHistory[0].Should().Be("Reset information before proceeding?");
        }

        [When(@"I click the submit button")]
        public async Task WhenIClickTheSubmitButton()
        {
            await _page.ClickAsync("#submit");
        }

        [Then(@"the results should contain 'Peter Parker'")]
        public async Task ThenTheResultsShouldContainPeterParker()
        {
            var results = await _page.InnerTextAsync("#results");
            results.Should().Contain("Peter Parker");
        }

        [When(@"I select 'Both' from the interface select")]
        public async Task WhenISelectBothFromTheInterfaceSelect()
        {
            await _page.SelectOptionAsync("#preferred-interface", "Both");
        }

        [Then(@"the interface select should have 'Both' selected")]
        public async Task ThenTheInterfaceSelectShouldHaveBothSelected()
        {
            var value = await _page.InputValueAsync("#preferred-interface");
            value.Should().Be("Both");
        }

        [When(@"I fill the form with 'Bruce Wayne' and submit")]
        public async Task WhenIFillTheFormWithBruceWayneAndSubmit()
        {
            await _page.FillAsync("#developer-name", "Bruce Wayne");
            await _page.ClickAsync("#macos");
            await _page.ClickAsync("#tried-test-cafe");
            await _page.FillAsync("#comments", "It's...");
            await Task.Delay(500);
            await _page.FillAsync("#comments", "It's...
good");
            await Task.Delay(500);
            await _page.SelectTextAsync("#comments", 0, 1);
            await _page.PressAsync("#comments", "Delete");
            await _page.FillAsync("#comments", "awesome!!!");
            await Task.Delay(500);
            await _page.ClickAsync("#submit");
            var results = await _page.InnerTextAsync("#results");
            results.Should().Contain("Bruce Wayne");
        }
    }
}
using FluentAssertions;
using TechTalk.SpecFlow;
using Microsoft.Playwright;
using System.Threading.Tasks;

namespace TestProject
{
    [Binding]
    public class TestSteps
    {
        private IPage _page;
        private IBrowser _browser;
        private IBrowserContext _context;

        [BeforeScenario]
        public async Task Setup()
        {
            var playwright = await Playwright.CreateAsync();
            _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            _context = await _browser.NewContextAsync();
            _page = await _context.NewPageAsync();
            await _page.GotoAsync("https://devexpress.github.io/testcafe/example/");
        }

        [AfterScenario]
        public async Task Teardown()
        {
            await _context.CloseAsync();
            await _browser.CloseAsync();
        }

        [Given(@"I type 'Peter' in the name input")]
        public async Task GivenITypePeterInTheNameInput()
        {
            await _page.FillAsync("#developer-name", "Peter");
        }

        [Given(@"I replace it with 'Parker'")]
        public async Task GivenIReplaceItWithParker()
        {
            await _page.FillAsync("#developer-name", "Parker");
        }

        [Given(@"I correct it to 'Parker'")]
        public async Task GivenICorrectItToParker()
        {
            await _page.FillAsync("#developer-name", "Parker");
        }

        [Then(@"the name input should be 'Parker'")]
        public async Task ThenTheNameInputShouldBeParker()
        {
            var value = await _page.InputValueAsync("#developer-name");
            value.Should().Be("Parker");
        }

        // Additional steps for other tests can be added here following the same pattern.
    }
}
