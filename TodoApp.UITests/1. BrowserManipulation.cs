// Browser manipulation: https://www.selenium.dev/documentation/en/webdriver/browser_manipulation/

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using Xunit;

namespace TodoApp.UITests;

public class BrowserManipulation
{
    const string homeUrl = "https://localhost:5002/";
    const string privacyUrl = "https://localhost:5002/Privacy";
    const string homeTitle = "Todo App - TodoWebApp";

    [Fact]
    [Trait("Category", "Smoke")]
    public void LoadApplicationPage()
    {
        using (IWebDriver driver = new ChromeDriver())
        {
            driver.Navigate().GoToUrl(homeUrl);

            DemoHelper.Pause();

            Assert.Equal(homeTitle, driver.Title);
            Assert.Equal(homeUrl, driver.Url);
        }
    }

    [Fact]
    [Trait("Category", "Smoke")]
    public void ReloadHomePage()
    {
        using (IWebDriver driver = new ChromeDriver())
        {
            driver.Navigate().GoToUrl(homeUrl);

            DemoHelper.Pause();

            driver.Navigate().Refresh();

            DemoHelper.Pause();

            Assert.Equal(homeTitle, driver.Title);
            Assert.Equal(homeUrl, driver.Url);
        }
    }

    [Fact]
    [Trait("Category", "Smoke")]
    public void ReloadHomePageOnForward()
    {
        using (IWebDriver driver = new ChromeDriver())
        {
            driver.Navigate().GoToUrl(privacyUrl);
            DemoHelper.Pause();

            driver.Navigate().GoToUrl(homeUrl);
            DemoHelper.Pause();

            driver.Navigate().Refresh();

            DemoHelper.Pause();

            Assert.Equal(homeTitle, driver.Title);
            Assert.Equal(homeUrl, driver.Url);
        }
    }

    [Theory]
    [InlineData(WebBrowser.Chrome)]
    [InlineData(WebBrowser.Firefox)]
    [InlineData(WebBrowser.edge)]
    [Trait("Category", "Smoke")]
    public void LoadApplicationPage_MultiBrowser(WebBrowser browser)
    {
        using (IWebDriver driver = DemoHelper.GetWebDriver(browser))
        {
            driver.Navigate().GoToUrl(homeUrl);

            DemoHelper.Pause();

            Assert.Equal(homeTitle, driver.Title);
            Assert.Equal(homeUrl, driver.Url);
        }
    }
}
