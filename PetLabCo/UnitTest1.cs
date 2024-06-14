using Microsoft.Playwright;
using NUnit.Framework;

namespace PetLabCo;

public class Tests
{
    private IPlaywright _playwright;
    private IBrowser _browser;
    
    [SetUp]
    public async Task Setup()
    {
        //Playwright
        _playwright = await Playwright.CreateAsync();
        //Browser
        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = false
                });
    }
    [Test]
    public async Task Test1()
    {
        //page
        var page = await _browser.NewPageAsync();
        await page.GotoAsync(url: "https://thepetlabco.com/");
        await page.ClickAsync(selector: "text=Account");
        await page.ClickAsync(selector: "text=My Account");
        
        await page.ClickAsync(selector: "#email");
        await page.FillAsync(selector: "#email", value: "photoshare.ui@gmail.com");
        await page.ClickAsync(selector: "#password");
        await page.Keyboard.DownAsync("Shift");
        await page.Keyboard.TypeAsync("T");
        await page.Keyboard.UpAsync("Shift");
        await page.Keyboard.TypeAsync("3st1ng@");
        
        // the above can be written using Locators as:
        /*await page.GetByLabel("email").ClickAsync();
        await page.GetByLabel("email").FillAsync("photoshare.ui@gmail.com");
        await page.GetByLabel("password").ClickAsync();
        await page.GetByLabel("password").PressAsync("CapsLock");
        await page.GetByLabel("password").FillAsync("T");
        await page.GetByLabel("password").PressAsync("CapsLock");
        await page.GetByLabel("password").FillAsync("T3st1ng@");*/
        
        await page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();
        
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        //var isExit = await page.Locator(selector: "text=Dashboard").IsVisibleAsync();
        page.GetByRole(AriaRole.Navigation).Locator("a").Filter(new() { HasText = "Dashboard" });
        
        //Thread.Sleep(2500);
        //Assert.Pass();
        //Assert.IsTrue(isExit);

        await page.GotoAsync("https://account.thepetlabco.com/pets");
        Thread.Sleep(2500);
        await page.GotoAsync("https://account.thepetlabco.com/dashboard");
        Thread.Sleep(2500);
        await page.GotoAsync("https://account.thepetlabco.com/login");
        Thread.Sleep(2500);
    }
}