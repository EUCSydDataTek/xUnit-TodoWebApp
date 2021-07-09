using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using Xunit;

namespace TodoApp.UITests
{
    public class UsingForms
    {
        const string homeUrl = "https://localhost:44346/";
        const string privacyUrl = "https://localhost:44346/Privacy";
        const string homeTitle = "Todo App - TodoWebApp";
        const string createUrl = "https://localhost:44346/Create";
        const string createTitle = "Create - TodoWebApp";

        // Locate Guid element by Id and compare Guid between navigation forward and backward
        [Fact]
        [Trait("Category", "Application")]
        public void BeSubmittedWhenValid()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(createUrl);

                //IWebElement todoDescriptionField = driver.FindElement(By.Id("TodoItem_TaskDescription"));
                //todoDescriptionField.SendKeys("Run 10K");

                // Shorter form:
                driver.FindElement(By.Id("TodoItem_TaskDescription")).SendKeys("Run 10K");

                IWebElement todoIsCompleted = driver.FindElement(By.Id("TodoItem_IsCompleted"));
                Assert.False(todoIsCompleted.Selected);

                DemoHelper.Pause();

                IWebElement prioritySelect = driver.FindElement(By.Id("TodoItem_Priority"));
                SelectElement priorityChoices = new SelectElement(prioritySelect);
                
                DemoHelper.Pause();
                priorityChoices.SelectByValue("0");
                DemoHelper.Pause();
                priorityChoices.SelectByText("Normal");
                DemoHelper.Pause();
                priorityChoices.SelectByIndex(3);
                DemoHelper.Pause();

                Assert.NotEqual("Choose a priority", priorityChoices.SelectedOption.Text); // Check that a priority is selected

                driver.FindElement(By.Id("SubmitTodo")).Click();

                driver.Navigate().GoToUrl(homeUrl);
                DemoHelper.Pause();
            }
        }

    }
}
