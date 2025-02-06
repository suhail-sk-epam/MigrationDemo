using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using TechTalk.SpecFlow;
using Microsoft.Playwright;

namespace TestProject.StepDefinitions
{
    [Binding]
    public class TestSteps
    {
        private IPage _page;

        public TestSteps()
        {
            var playwright = Playwright.CreateAsync().Result;
            var browser = playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false }).Result;
            _page = browser.NewPageAsync().Result;
            _page.GotoAsync("https://devexpress.github.io/testcafe/example/").Wait();
        }

        [Given(@"I type the name '([^']*)' in the input field")]
        public void GivenITypeTheNameInTheInputField(string name)
        {
            _page.FillAsync("#developer-name", name).Wait();
        }

        [Then(@"the input field should contain '([^']*)'")]
        public void ThenTheInputFieldShouldContain(string expectedName)
        {
            var actualName = _page.InputValueAsync("#developer-name").Result;
            actualName.Should().Be(expectedName);
        }

        [When(@"I click the populate button")]
        public void WhenIClickThePopulateButton()
        {
            _page.ClickAsync("#populate").Wait();
        }

        [Then(@"a confirmation dialog should appear with text '([^']*)'")]
        public void ThenAConfirmationDialogShouldAppearWithText(string expectedText)
        {
            var dialog = _page.WaitForEventAsync(PageEvent.Dialog).Result;
            dialog.Message.Should().Be(expectedText);
            dialog.AcceptAsync().Wait();
        }

        [When(@"I submit the form")]
        public void WhenISubmitTheForm()
        {
            _page.ClickAsync("#submit-button").Wait();
        }

        [Then(@"the result should contain '([^']*)'")]
        public void ThenTheResultShouldContain(string expectedResult)
        {
            var result = _page.InnerTextAsync("#article-header").Result;
            result.Should().Contain(expectedResult);
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
        private readonly ScenarioContext _scenarioContext;

        public TestSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I navigate to the example page")]
        public async Task GivenINavigateToTheExamplePage()
        {
            var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            var context = await browser.NewContextAsync();
            _page = await context.NewPageAsync();
            await _page.GotoAsync("https://devexpress.github.io/testcafe/example/");
        }

        [When(@"I type the name '(.*)' in the name input")]
        public async Task WhenITypeTheNameInTheNameInput(string name)
        {
            await _page.FillAsync("#developer-name", name);
        }

        [Then(@"the name input should have value '(.*)'")]
        public async Task ThenTheNameInputShouldHaveValue(string expectedValue)
        {
            var value = await _page.InputValueAsync("#developer-name");
            value.Should().Be(expectedValue);
        }

        [When(@"I click the '(.*)' feature")]
        public async Task WhenIClickTheFeature(string feature)
        {
            await _page.ClickAsync($"label[for='{feature}']");
        }

        [Then(@"the '(.*)' feature should be checked")]
        public async Task ThenTheFeatureShouldBeChecked(string feature)
        {
            var isChecked = await _page.IsCheckedAsync($"#remote-testing");
            isChecked.Should().BeTrue();
        }

        [When(@"I press the '(.*)' key")]
        public async Task WhenIPressTheKey(string key)
        {
            await _page.PressAsync("#developer-name", key);
        }

        [When(@"I move the slider to '(.*)'")]
        public async Task WhenIMoveTheSliderTo(string value)
        {
            await _page.DragAndDropAsync("#slider", $"#slider > span:nth-child({value})");
        }

        [Then(@"the slider value should be greater than '(.*)'")]
        public async Task ThenTheSliderValueShouldBeGreaterThan(string value)
        {
            var sliderValue = await _page.EvalOnSelectorAsync<int>("#slider", "el => el.value");
            sliderValue.Should().BeGreaterThan(int.Parse(value));
        }

        [When(@"I select text from '(.*)' to '(.*)'")]
        public async Task WhenISelectTextFromTo(int start, int end)
        {
            await _page.EvalOnSelectorAsync("#developer-name", "(el, start, end) => el.setSelectionRange(start, end)", new object[] { start, end });
        }

        [When(@"I handle the native confirmation dialog")]
        public async Task WhenIHandleTheNativeConfirmationDialog()
        {
            _page.Dialog += async (_, dialog) => await dialog.AcceptAsync();
            await _page.ClickAsync("#populate");
        }

        [Then(@"the dialog text should be '(.*)'")]
        public async Task ThenTheDialogTextShouldBe(string expectedText)
        {
            var dialogHistory = await _page.EvalOnSelectorAsync<string[]>("#populate", "el => window.confirmHistory");
            dialogHistory[0].Should().Be(expectedText);
        }

        [When(@"I pick the '(.*)' option from the select")]
        public async Task WhenIPickTheOptionFromTheSelect(string option)
        {
            await _page.SelectOptionAsync("#preferred-interface", option);
        }

        [Then(@"the select value should be '(.*)'")]
        public async Task ThenTheSelectValueShouldBe(string expectedValue)
        {
            var value = await _page.InputValueAsync("#preferred-interface");
            value.Should().Be(expectedValue);
        }

        [When(@"I fill the form with name '(.*)', OS '(.*)', and comments '(.*)'")]
        public async Task WhenIFillTheFormWithNameOSAndComments(string name, string os, string comments)
        {
            await _page.FillAsync("#developer-name", name);
            await _page.CheckAsync($"#macos");
            await _page.CheckAsync("#tried-test-cafe");
            await _page.FillAsync("#comments", comments);
        }

        [When(@"I submit the form")]
        public async Task WhenISubmitTheForm()
        {
            await _page.ClickAsync("#submit-button");
        }

        [Then(@"the results should contain '(.*)'")]
        public async Task ThenTheResultsShouldContain(string expectedText)
        {
            var resultsText = await _page.InnerTextAsync("#article-header");
            resultsText.Should().Contain(expectedText);
        }
    }
}
