using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Microsoft.Playwright;
using TechTalk.SpecFlow;

namespace PlaywrightSpecFlowTests
{
    [Binding]
    public class ExampleTests
    {
        private IPage _page;

        [BeforeScenario]
        public async Task SetUp()
        {
            var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            _page = await browser.NewPageAsync();
            await _page.GotoAsync("https://devexpress.github.io/testcafe/example/");
        }

        [AfterScenario]
        public async Task TearDown()
        {
            await _page.CloseAsync();
        }

        [Given("I type text into the input field")]
        public async Task GivenITypeTextIntoTheInputField()
        {
            await _page.FillAsync("#developer-name", "Peter");
            await _page.FillAsync("#developer-name", "Parker");
            var inputValue = await _page.InputValueAsync("#developer-name");
            inputValue.Should().Be("Parker");
        }

        [Given("I click an array of labels and check their states")]
        public async Task GivenIClickAnArrayOfLabelsAndCheckTheirStates()
        {
            var features = new[] { "#remote-testing", "#reusing-js-code", "#background-parallel-testing", "#continuous-integration-embedding", "#traffic-markup-analysis" };
            foreach (var feature in features)
            {
                await _page.CheckAsync(feature);
                var isChecked = await _page.IsCheckedAsync(feature);
                isChecked.Should().BeTrue();
            }
        }

        [Given("I deal with text using keyboard")]
        public async Task GivenIDealWithTextUsingKeyboard()
        {
            await _page.FillAsync("#developer-name", "Peter Parker");
            await _page.PressAsync("#developer-name", "Backspace");
            var inputValue = await _page.InputValueAsync("#developer-name");
            inputValue.Should().Be("Pete Parker");
            await _page.PressAsync("#developer-name", "Home Right . Delete Delete Delete");
            inputValue = await _page.InputValueAsync("#developer-name");
            inputValue.Should().Be("P. Parker");
        }

        [Given("I move the slider")]
        public async Task GivenIMoveTheSlider()
        {
            var initialOffset = await _page.EvaluateAsync<int>("() => document.querySelector('.ui-slider-handle').offsetLeft");
            await _page.CheckAsync("#tried-test-cafe");
            await _page.DragAndDropAsync(".ui-slider-handle", ".slider-value");
            var newOffset = await _page.EvaluateAsync<int>("() => document.querySelector('.ui-slider-handle').offsetLeft");
            newOffset.Should().BeGreaterThan(initialOffset);
        }

        [Given("I deal with text using selection")]
        public async Task GivenIDealWithTextUsingSelection()
        {
            await _page.FillAsync("#developer-name", "Test Cafe");
            await _page.FillAsync("#developer-name", "Tfe");
            var inputValue = await _page.InputValueAsync("#developer-name");
            inputValue.Should().Be("Tfe");
        }

        [Given("I handle native confirmation dialog")]
        public async Task GivenIHandleNativeConfirmationDialog()
        {
            _page.Dialog += async (_, dialog) => await dialog.AcceptAsync();
            await _page.ClickAsync("#populate");
            var dialogHistory = await _page.EvaluateAsync<string[]>("() => window.confirmationHistory");
            dialogHistory[0].Should().Be("Reset information before proceeding?");
            await _page.ClickAsync("#submit-button");
            var resultsText = await _page.InnerTextAsync("#article-header");
            resultsText.Should().Contain("Peter Parker");
        }

        [Given("I pick option from select")]
        public async Task GivenIPickOptionFromSelect()
        {
            await _page.SelectOptionAsync("#preferred-interface", "Both");
            var selectValue = await _page.EvaluateAsync<string>("() => document.querySelector('#preferred-interface').value");
            selectValue.Should().Be("Both");
        }

        [Given("I fill a form")]
        public async Task GivenIFillAForm()
        {
            await _page.FillAsync("#developer-name", "Bruce Wayne");
            await _page.CheckAsync("#macos");
            await _page.CheckAsync("#tried-test-cafe");
            await _page.FillAsync("#comments", "It's...\ngood");
            await _page.FillAsync("#comments", "awesome!!!");
            await _page.ClickAsync("#submit-button");
            var resultsText = await _page.InnerTextAsync("#article-header");
            resultsText.Should().Contain("Bruce Wayne");
        }
    }
}
