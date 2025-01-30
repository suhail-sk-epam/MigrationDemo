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
