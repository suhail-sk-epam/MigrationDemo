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
        private readonly IPage _page;

        public TestSteps(IPage page)
        {
            _page = page;
        }

        [Given(@"I navigate to the TestCafe example page")]
        public async Task GivenINavigateToTheTestCafeExamplePage()
        {
            await _page.GotoAsync("https://devexpress.github.io/testcafe/example/");
        }

        [When(@"I type text into the name input")]
        public async Task WhenITypeTextIntoTheNameInput()
        {
            await _page.FillAsync("#developer-name", "Peter");
            await _page.FillAsync("#developer-name", "Paker", new PageFillOptions { Replace = true });
            await _page.FillAsync("#developer-name", "r", new PageFillOptions { CaretPosition = 2 });
        }

        [Then(@"the name input should have the correct value")]
        public async Task ThenTheNameInputShouldHaveTheCorrectValue()
        {
            var value = await _page.InputValueAsync("#developer-name");
            value.Should().Be("Parker");
        }

        // Add other test methods following the same pattern
    }
}
