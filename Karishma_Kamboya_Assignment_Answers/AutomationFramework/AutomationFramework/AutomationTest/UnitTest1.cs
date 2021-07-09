using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using System.Threading;
using AutomationTest.Pages;
using AutomationFramework.Base;
using AutomationFramework.Helpers;
using System.Collections.Generic;

namespace AutomationTest
{

    public class UnitTest1
    {

        [OneTimeSetUp]
        public void Test_Setup()
        {
            LogHelpers.CreateLogFile();

            string driverpath = System.IO.Directory.GetCurrentDirectory() + "\\ChromeDriver";
            DriverContext.Driver = new ChromeDriver(driverpath);
            LogHelpers.Write("Launched the browser");
            DriverContext.Driver.Manage().Window.Maximize();
            DriverContext.Driver.Navigate().GoToUrl("http://jt-dev.azurewebsites.net/#/SignUp");
            LogHelpers.Write("Navigated to Application");
            DriverContext.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);  //Waiting for 1 min to load the site
        }

        [Test]
        public void Verify_DropDownList()
        {
            LoginPage loginpage = new LoginPage();
            LogHelpers.Write("Loaded SignUp Page ");
            List<string> DDL_ExpectedValues = new List<string>() { "English", "Dutch" };
            List<string> Actual_DDL = loginpage.GetDDList();
            //Assert.Multiple is used as there are multiple verification in dropdown list
            NUnit.Framework.Assert.Multiple(() =>
            {
                foreach (var item in Actual_DDL)
                {
                    Assert.That(DDL_ExpectedValues.Contains(item));
                    LogHelpers.Write("Checked Dropdown list values");
                }
            });

        }

        [Test]
        public void Verify_Email()
        {
            LoginPage loginpage = new LoginPage();
            EmailPage emailpage = new EmailPage();
        
            emailpage.CreateEmailAccount(ConfigDetails.EmailDetails.email);
            LogHelpers.Write("Email account created successfully");
       
            loginpage.SignUpForm(
                        ConfigDetails.LoginDetails.fullName,
                        ConfigDetails.LoginDetails.orgName,
                        ConfigDetails.LoginDetails.Email);
            LogHelpers.Write("Entered Name, Org name, Email and clicked on submit button");

            emailpage.LogIntoEmailAccount(ConfigDetails.LoginDetails.Email);
            LogHelpers.Write("Navigated to Email tab");

            IList<IWebElement> allEmails = emailpage.GetAllMails();
            if (allEmails.Count != 0)
            {
                Assert.Pass("Your email has been received");
                LogHelpers.Write("Received mail");
            }
            else
            {
                Assert.Fail("Your email hasn't been received");
                LogHelpers.Write("Failed email verification");
            }
        }
        [OneTimeTearDown]
        public void Test_Cleanup()
        {
            DriverContext.Driver.Quit();
            LogHelpers.Write("Driver instance closed successfully");
        }
    }
}
