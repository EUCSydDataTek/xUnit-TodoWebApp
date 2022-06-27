using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using Xunit;

namespace TodoApp.UITests;

public class SelectingHTMLElements
{
    const string homeUrl = "https://localhost:44346/";
    const string privacyUrl = "https://localhost:44346/Privacy";
    const string homeTitle = "Todo App - TodoWebApp";
    const string createUrl = "https://localhost:44346/Create";
    const string createTitle = "Create - TodoWebApp";

    // Locate Guid element by Id and compare Guid between navigation forward and backward
    [Fact]
    [Trait("Category", "Application")]
    public void BeInitiatedFromHomePage_Create_Guid()
    {
        using (IWebDriver driver = new ChromeDriver())
        {
            driver.Navigate().GoToUrl(homeUrl);

            IWebElement generationTokenElement = driver.FindElement(By.Id("GenerationToken"));
            string initialToken = generationTokenElement.Text;

            DemoHelper.Pause();

            driver.Navigate().GoToUrl(privacyUrl);
            DemoHelper.Pause();

            driver.Navigate().Back();
            DemoHelper.Pause();

            string reloadedToken = driver.FindElement(By.Id("GenerationToken")).Text;

            Assert.NotEqual(initialToken, reloadedToken);
        }
    }

    // Test clicking a link found by Name, LinkText and ClassName
    [Fact]
    [Trait("Category", "Application")]
    public void BeInitiatedFromHomePage_Create()
    {
        using (IWebDriver driver = new ChromeDriver())
        {
            driver.Navigate().GoToUrl(homeUrl);
            DemoHelper.Pause();

            IWebElement createLink = driver.FindElement(By.Name("CreateNewTodo"));        // By Name
            //IWebElement createLink = driver.FindElement(By.LinkText("Create New Todo"));    // By LinkText
            //IWebElement createLink = driver.FindElement(By.PartialLinkText("Create"));    // By PartialLinkText
            //IWebElement createLink = driver.FindElement(By.ClassName("btn"));    // By className
            createLink.Click();

            DemoHelper.Pause();

            Assert.Equal(createTitle, driver.Title);
            Assert.Equal(createUrl, driver.Url);
        }
    }

    // Searching for HTTP TagName
    [Fact]
    [Trait("Category", "Application")]
    public void BeInitiatedFromHomePage_Table()
    {
        using (IWebDriver driver = new ChromeDriver())
        {
            driver.Navigate().GoToUrl(homeUrl);
            DemoHelper.Pause();

            IWebElement firstTableHeaderCell = driver.FindElement(By.TagName("th"));      // By HTML TagName

            Assert.Equal("Udført", firstTableHeaderCell.Text);
        }
    }

    // Searching for Multiple elements
    [Fact]
    [Trait("Category", "Application")]
    public void BeInitiatedFromHomePage_Multiple_Elements()
    {
        using (IWebDriver driver = new ChromeDriver())
        {
            driver.Navigate().GoToUrl(homeUrl);
            DemoHelper.Pause();

            ReadOnlyCollection<IWebElement> tableCells = driver.FindElements(By.TagName("th"));      // By HTML TagName

            Assert.Equal("Id", tableCells[4].Text);
        }
    }

    // Searching for an td by XPath
    [Fact]
    [Trait("Category", "Application")]
    public void BeInitiatedFromHomePage_TableXPath()
    {
        using (IWebDriver driver = new ChromeDriver())
        {
            driver.Navigate().GoToUrl(homeUrl);
            DemoHelper.Pause();

            IWebElement firstPriorityCell = driver.FindElement(By.XPath("/html/body/div/main/form/table/tbody/tr[1]/td[3]"));     // By XPath (use Dev Tools: Copy | Copy XPath
                                                                                                                                  // Se also: http://xpather.com/
            Assert.Equal("Low", firstPriorityCell.Text);
        }
    }

    // Wait max 10 seconds for link to EUC Syd located in the second carousel page
    [Fact]
    [Trait("Category", "Application")]
    public void BeInitiatedFromHomePage_Wait()
    {
        using (IWebDriver driver = new ChromeDriver())
        {
            driver.Navigate().GoToUrl(homeUrl);
            DemoHelper.Pause();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement applyLink = wait.Until(d => d.FindElement(By.LinkText("EUC Syd")));
            applyLink.Click();

            DemoHelper.Pause();

            Assert.Equal("https://www.eucsyd.dk/", driver.Url);
        }
    }
}
