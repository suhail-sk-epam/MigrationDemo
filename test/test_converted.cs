using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using SpecFlow;
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
        public async Task TearDown()
        {
            await _context.CloseAsync();
            await _browser.CloseAsync();
        }

        [Given(@"I type name '([^']*)' into the name input field")]
        public async Task GivenITypeNameIntoTheNameInputField(string name)
        {
            await _page.FillAsync("#developer-name", name);
        }

        [Then(@"The name input field should have value '([^']*)'")]
        public async Task ThenTheNameInputFieldShouldHaveValue(string expectedValue)
        {
            var value = await _page.InputValueAsync("#developer-name");
            value.Should().Be(expectedValue);
        }

        // Add other steps for each test case
    }
}
