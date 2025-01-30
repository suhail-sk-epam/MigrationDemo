using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using TechTalk.SpecFlow;
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
        public async Task BeforeScenario()
        {
            var playwright = await Playwright.CreateAsync();
            _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            _context = await _browser.NewContextAsync();
            _page = await _context.NewPageAsync();
            await _page.GotoAsync("https://devexpress.github.io/testcafe/example/");
        }

        [AfterScenario]
        public async Task AfterScenario()
        {
            await _browser.CloseAsync();
        }

        [Given(@"I type text into the input field")]
        public async Task GivenITypeTextIntoTheInputField()
        {
            await _page.FillAsync("#developer-name", "Peter");
            await _page.FillAsync("#developer-name", "Paker");
            await _page.FillAsync("#developer-name", "r");
            var value = await _page.InputValueAsync("#developer-name");
            value.Should().Be("Parker");
        }

        [Given(@"I click an array of labels and check their states")]
        public async Task GivenIClickAnArrayOfLabelsAndCheckTheirStates()
        {
            var featureList = await _page.QuerySelectorAllAsync(".feature");
            foreach (var feature in featureList)
            {
                await feature.ClickAsync();
                var checkbox = await feature.QuerySelectorAsync("input[type='checkbox']");
                var isChecked = await checkbox.IsCheckedAsync();
                isChecked.Should().BeTrue();
            }
        }

        [Given(@"I deal with text using keyboard")]
        public async Task GivenIDealWithTextUsingKeyboard()
        {
            await _page.FillAsync("#developer-name", "Peter Parker");
            await _page.ClickAsync("#developer-name", new PageClickOptions { Position = new Position { X = 5, Y = 0 } });
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
            await _page.CheckAsync("#tried-test-cafe");
            await _page.DragToAsync("#slider", "#slider .tick:nth-child(9)");
            var newOffset = await _page.EvalOnSelectorAsync<int>("#slider", "el => el.offsetLeft");
            newOffset.Should().BeGreaterThan(initialOffset);
        }

        [Given(@"I deal with text using selection")]
        public async Task GivenIDealWithTextUsingSelection()
        {
            await _page.FillAsync("#developer-name", "Test Cafe");
            await _page.SelectTextAsync("#developer-name", 7, 1);
            await _page.PressAsync("#developer-name", "Delete");
            var value = await _page.InputValueAsync("#developer-name");
            value.Should().Be("Tfe");
        }

        [Given(@"I handle native confirmation dialog")]
        public async Task GivenIHandleNativeConfirmationDialog()
        {
            _page.Dialog += async (_, dialog) => await dialog.AcceptAsync();
            await _page.ClickAsync("#populate");
            var dialogHistory = await _page.DialogHistoryAsync();
            dialogHistory[0].Message.Should().Be("Reset information before proceeding?");
            await _page.ClickAsync("#submit-button");
            var result = await _page.InnerTextAsync("#result");
            result.Should().Contain("Peter Parker");
        }

        [Given(@"I pick option from select")]
        public async Task GivenIPickOptionFromSelect()
        {
            await _page.ClickAsync("#preferred-interface");
            await _page.ClickAsync("#preferred-interface option[value='Both']");
            var value = await _page.InputValueAsync("#preferred-interface");
            value.Should().Be("Both");
        }

        [Given(@"I fill a form")]
        public async Task GivenIFillAForm()
        {
            await _page.FillAsync("#developer-name", "Bruce Wayne");
            await _page.CheckAsync("#macos");
            await _page.CheckAsync("#tried-test-cafe");
            await _page.FillAsync("#comments", "It's...");
            await Task.Delay(500);
            await _page.FillAsync("#comments", "\ngood");
            await Task.Delay(500);
            await _page.SelectTextAsync("#comments", 1, 0);
            await _page.PressAsync("#comments", "Delete");
            await _page.FillAsync("#comments", "awesome!!!");
            await Task.Delay(500);
            await _page.ClickAsync("#submit-button");
            var result = await _page.InnerTextAsync("#result");
            result.Should().Contain("Bruce Wayne");
        }
    }
}