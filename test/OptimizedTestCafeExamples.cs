// Optimized version of TestCafeExamples.cs with improvements in readability, performance, and adherence to best practices.

using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace TestCafeExamples
{
    [TestClass]
    public class OptimizedTestCafeExamples
    {
        private IBrowser _browser;
        private IBrowserContext _context;
        private IPage _page;

        [TestInitialize]
        public async Task Setup()
        {
            var playwright = await Playwright.CreateAsync();
            _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });
            _context = await _browser.NewContextAsync();
            _page = await _context.NewPageAsync();
        }

        [TestCleanup]
        public async Task Teardown()
        {
            await _page.CloseAsync();
            await _context.CloseAsync();
            await _browser.CloseAsync();
        }

        [TestMethod]
        public async Task TestExample()
        {
            await _page.GotoAsync("https://example.com");
            var title = await _page.TitleAsync();
            title.Should().Be("Example Domain");
        }

        private async Task ReusableMethodExample(string url)
        {
            await _page.GotoAsync(url);
        }
    }
}