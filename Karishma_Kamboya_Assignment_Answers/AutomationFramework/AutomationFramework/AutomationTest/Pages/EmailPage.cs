using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using AutomationFramework.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTest.Pages
{
   public class EmailPage 
    {
        public EmailPage()
        {
            PageFactory.InitElements(DriverContext.Driver, this);
        }

        //Objects for the Email Page
        [FindsBy(How = How.Id, Using = "addOverlay")]
        public IWebElement EmailID { get; set; }
        [FindsBy(How = How.Id, Using = "go-to-public")]
        public IWebElement GoBtn { get; set; }


        //Create an email account
        public void CreateEmailAccount(string email)
        {
            
            ((IJavaScriptExecutor)DriverContext.Driver).ExecuteScript("window.open();"); //For opening new tab in browser.
            DriverContext.Driver.SwitchTo().Window(DriverContext.Driver.WindowHandles.Last());
            DriverContext.Driver.Navigate().GoToUrl("https://www.mailinator.com/");
            System.Threading.Thread.Sleep(5000);
            EmailID.SendKeys(email);
            GoBtn.Click();
            System.Threading.Thread.Sleep(8000);
            DriverContext.Driver.SwitchTo().Window(DriverContext.Driver.WindowHandles.First());
        }

        //Log in to Email Account
        public void LogIntoEmailAccount(string email)
        {
            //EmailPage ePage = new EmailPage();
            DriverContext.Driver.SwitchTo().Window(DriverContext.Driver.WindowHandles.Last());
            DriverContext.Driver.Navigate().Refresh();
        }

        //Get All mails
        public IList<IWebElement> GetAllMails()
        {
            IList<IWebElement> allMessages = DriverContext.Driver.FindElements(By.XPath("//*[contains(text(), 'Please Complete JabaTalks Account')]"));
            return allMessages;
        }
    }
    
}
