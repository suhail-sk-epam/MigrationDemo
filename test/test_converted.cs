using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using SpecFlow;
using Microsoft.Playwright;

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

        [Given(@"I type text in the input field")]
        public async Task GivenITypeTextInTheInputField()
        {
            await _page.FillAsync("#developer-name", "Peter");
            await _page.FillAsync("#developer-name", "Parker");
            var value = await _page.InputValueAsync("#developer-name");
            value.Should().Be("Parker");
        }

        [Given(@"I click an array of labels and check their states")]
        public async Task GivenIClickAnArrayOfLabelsAndCheckTheirStates()
        {
            var labels = await _page.QuerySelectorAllAsync(".feature label");
            foreach (var label in labels)
            {
                await label.ClickAsync();
                var checkbox = await label.QuerySelectorAsync("input[type=checkbox]");
                var isChecked = await checkbox.IsCheckedAsync();
                isChecked.Should().BeTrue();
            }
        }

        [Given(@"I deal with text using keyboard")]
        public async Task GivenIDealWithTextUsingKeyboard()
        {
            await _page.FillAsync("#developer-name", "Peter Parker");
            await _page.ClickAsync("#developer-name", new ClickOptions { Position = new Position { X = 5, Y = 5 } });
            await _page.PressAsync("#developer-name", "Backspace");
            var value = await _page.InputValueAsync("#developer-name");
            value.Should().Be("Pete Parker");
            await _page.PressAsync("#developer-name", "Home Right . Delete Delete Delete");
            value = await _page.InputValueAsync("#developer-name");
            value.Should().Be("P. Parker");
        }

        [Given(@"I move the slider")]
        public async Task GivenIMoveTheSlider()
        {
            var initialOffset = await _page.EvalOnSelectorAsync<int>("#slider", "el => el.offsetLeft");
            await _page.ClickAsync("#tried-test-cafe");
            await _page.DragAndDropAsync("#slider", "#slider-tick-9");
            var newOffset = await _page.EvalOnSelectorAsync<int>("#slider", "el => el.offsetLeft");
            newOffset.Should().BeGreaterThan(initialOffset);
        }

        [Given(@"I deal with text using selection")]
        public async Task GivenIDealWithTextUsingSelection()
        {
            await _page.FillAsync("#developer-name", "Test Cafe");
            await _page.SelectTextAsync("#developer-name", 1, 7);
            await _page.PressAsync("#developer-name", "Delete");
            var value = await _page.InputValueAsync("#developer-name");
            value.Should().Be("Tfe");
        }

        [Given(@"I handle native confirmation dialog")]
        public async Task GivenIHandleNativeConfirmationDialog()
        {
            await _page.ClickAsync("#populate");
            var dialog = await _page.WaitForEventAsync(PageEvent.Dialog);
            await dialog.AcceptAsync();
            var dialogHistory = await _page.EvalOnSelectorAllAsync<string>("#dialog-history", "els => els.map(el => el.innerText)");
            dialogHistory[0].Should().Be("Reset information before proceeding?");
            await _page.ClickAsync("#submit-button");
            var result = await _page.InnerTextAsync("#result");
            result.Should().Contain("Peter Parker");
        }

        [Given(@"I pick option from select")]
        public async Task GivenIPickOptionFromSelect()
        {
            await _page.SelectOptionAsync("#preferred-interface", "Both");
            var value = await _page.InputValueAsync("#preferred-interface");
            value.Should().Be("Both");
        }

        [Given(@"I fill a form")]
        public async Task GivenIFillAForm()
        {
            await _page.FillAsync("#developer-name", "Bruce Wayne");
            await _page.ClickAsync("#macos");
            await _page.ClickAsync("#tried-test-cafe");
            await _page.FillAsync("#comments", "It's...");
            await _page.WaitForTimeoutAsync(500);
            await _page.FillAsync("#comments", "\ngood");
            await _page.WaitForTimeoutAsync(500);
            await _page.SelectTextAreaContentAsync("#comments", 1, 0);
            await _page.PressAsync("#comments", "Delete");
            await _page.FillAsync("#comments", "awesome!!!");
            await _page.WaitForTimeoutAsync(500);
            await _page.ClickAsync("#submit-button");
            var result = await _page.InnerTextAsync("#result");
            result.Should().Contain("Bruce Wayne");
        }
    }
}
