using System.Threading.Tasks;
using Microsoft.Playwright;
using TechTalk.SpecFlow;
using FluentAssertions;

[Binding]
public class TestCafeExampleTests
{
    private IPage page;

    [BeforeScenario]
    public async Task InitializeAsync()
    {
        var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        page = await browser.NewPageAsync();
        await page.GotoAsync("https://devexpress.github.io/testcafe/example/");
    }

    [AfterScenario]
    public async Task CleanupAsync()
    {
        await page.CloseAsync();
    }

    [Scenario]
    public async Task TextTypingBasics()
    {
        await page.FillAsync("#developer-name", "Peter");
        await page.FillAsync("#developer-name", "Paker");
        await page.PressAsync("#developer-name", "ArrowLeft");
        await page.PressAsync("#developer-name", "r");
        var value = await page.InputValueAsync("#developer-name");
        value.Should().Be("Parker");
    }

    [Scenario]
    public async Task ClickArrayLabelsCheckStates()
    {
        var features = page.Locator(".feature-label");
        int count = await features.CountAsync();
        for (int i = 0; i < count; i++)
        {
            await features.Nth(i).ClickAsync();
            bool isChecked = await features.Nth(i).IsCheckedAsync();
            isChecked.Should().BeTrue();
        }
    }

    [Scenario]
    public async Task DealingWithTextUsingKeyboard()
    {
        await page.FillAsync("#developer-name", "Peter Parker");
        await page.ClickAsync("#developer-name", new PageClickOptions { Position = new Position { X = 5 } });
        await page.PressAsync("Backspace");
        var value = await page.InputValueAsync("#developer-name");
        value.Should().Be("Pete Parker");
    }

    [Scenario]
    public async Task MovingTheSlider()
    {
        await page.CheckAsync("#tried-test-cafe");
        var sliderHandle = page.Locator(".ui-slider-handle");
        var initialOffset = await sliderHandle.BoundingBoxAsync();
        await sliderHandle.DragToAsync(page.Locator(".slider-value").Nth(9));
        var newOffset = await sliderHandle.BoundingBoxAsync();
        newOffset.X.Should().BeGreaterThan(initialOffset.X);
    }

    [Scenario]
    public async Task DealingWithTextUsingSelection()
    {
        await page.FillAsync("#developer-name", "Test Cafe");
        await page.SelectTextAsync("#developer-name");
        await page.PressAsync("Delete");
        var value = await page.InputValueAsync("#developer-name");
        value.Should().Be("Tfe");
    }

    [Scenario]
    public async Task HandleNativeConfirmationDialog()
    {
        page.Dialog += (_, dialog) => dialog.AcceptAsync();
        await page.ClickAsync("#populate");
        await page.ClickAsync("#submit-button");
        var result = await page.TextContentAsync(".result-content");
        result.Should().Contain("Peter Parker");
    }

    [Scenario]
    public async Task PickOptionFromSelect()
    {
        await page.SelectOptionAsync("#preferred-interface", new[] { "Both" });
        var value = await page.InputValueAsync("#preferred-interface");
        value.Should().Be("Both");
    }

    [Scenario]
    public async Task FillingAForm()
    {
        await page.FillAsync("#developer-name", "Bruce Wayne");
        await page.CheckAsync("#macos");
        await page.CheckAsync("#tried-test-cafe");
        await page.FillAsync("#comments", "It's...\ngood");
        await page.SelectTextAsync("#comments");
        await page.PressAsync("Delete");
        await page.FillAsync("#comments", "awesome!!!");
        await page.ClickAsync("#submit-button");
        var result = await page.TextContentAsync(".result-content");
        result.Should().Contain("Bruce Wayne");
    }
}