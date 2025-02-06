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

        [Given("I navigate to the example page")]
        public async Task GivenINavigateToTheExamplePage()
        {
            var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            var context = await browser.NewContextAsync();
            _page = await context.NewPageAsync();
            await _page.GotoAsync("https://devexpress.github.io/testcafe/example/");
        }

        [When("I type the name 'Peter' into the input")]
        public async Task WhenITypeTheNamePeterIntoTheInput()
        {
            await _page.FillAsync("#developer-name", "Peter");
        }

        [When("I replace it with the name 'Parker'")]
        public async Task WhenIReplaceItWithTheNameParker()
        {
            await _page.FillAsync("#developer-name", "Parker");
        }

        [Then("the input value should be 'Parker'")]
        public async Task ThenTheInputValueShouldBeParker()
        {
            var value = await _page.InputValueAsync("#developer-name");
            value.Should().Be("Parker");
        }

        // Other tests can be added here following the same pattern
    }
}
