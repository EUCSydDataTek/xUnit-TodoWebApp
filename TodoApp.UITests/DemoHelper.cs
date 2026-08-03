using Microsoft.AspNetCore.Components.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System.Threading;

namespace TodoApp.UITests
{
    internal static class DemoHelper
    {
        public static void Pause(int secondsToPause = 3000)
        {
            Thread.Sleep(secondsToPause);
        }

        public static IWebDriver GetWebDriver(WebBrowser browser)
        {
            switch (browser)
            {
                case WebBrowser.Chrome:
                    return new ChromeDriver();
                case WebBrowser.Firefox:
                    return new FirefoxDriver();
                case WebBrowser.edge:
                    return new EdgeDriver();
                default:
                    return null!;
            }
        }
     
    }

    public enum WebBrowser
    {
        Chrome,
        Firefox,
        edge,
    }
}
