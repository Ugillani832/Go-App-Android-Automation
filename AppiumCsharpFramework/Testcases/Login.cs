using AppiumCsharpFramework.Page_Objects;
using AppiumCsharpFramework.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

[assembly: LevelOfParallelism(2)]

namespace AppiumCsharpFramework.Testcases
{
     [Parallelizable(ParallelScope.All)]
    class Login : Base
    {
        //[Test, Order(1)]
        [Test, Order(1)]

        //public void validcredentials(String Username, String Password)
        public void validcredentials()
        {
            try
            {
                var Username = getData().ExtractData("UserName");
                var Password = getData().ExtractData("Password");


                PObjects obj = new PObjects(driver);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("et_username")));

                obj.UserName().SendKeys(Username);
                obj.Passwrd().SendKeys(Password);
                obj.SignIn().Click();

               wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("et_pincode")));


            }
            catch (Exception ex)
            {
                Assert.Fail("TestCase Fail");
                TestContext.Progress.WriteLine(ex.StackTrace);
            }

        }

        [Test, Order(2), TestCaseSource("GetInvalidTestCasesData")]

        public void Invalidcredentials(String InvalidUser, String InvalidPassword)
        {
            try
            {
                PObjects obj = new PObjects(driver);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("et_username")));

                obj.UserName().SendKeys(InvalidUser);
                obj.Passwrd().SendKeys(InvalidPassword);
                obj.SignIn().Click();

                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//android.widget.FrameLayout[@resource-id=\"android:id/content\"]/android.view.ViewGroup/android.view.ViewGroup")));
            }
            catch (Exception ex)
            {
                Assert.Fail("TestCase Fail");
                TestContext.Progress.WriteLine(ex.StackTrace);
            }

        }

        [Test, Order(3)]
        public void HomePage()
        {
            try
            {
                validcredentials();

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

              /*  var client = new RestClient("https://mobile-sms-us-qa.netsolace.com/");

                var request = new RestRequest("/api/store/getstoredailypin", Method.Get);

                // Json to post.
                try
                {
                    var response= client.ExecuteAsync(request,CancellationToken.None);
                    
                }
                catch (Exception error)
                {
                    // Log
                }*/

                driver.FindElement(By.Id("et_pincode")).SendKeys("5594");

                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//android.widget.LinearLayout[@resource-id=\"com.netsolace.smsdelivery.qa:id/parent_layout\"]/android.widget.LinearLayout[1]/android.widget.LinearLayout")));

                driver.FindElement(By.Id("et_pincode")).SendKeys("1111");

                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                var HomePath = driver.FindElement(By.Id("android:id/button1"));
                if(HomePath != null)
                {
                    Console.WriteLine("this is alert"+ HomePath);
                    HomePath.Click();
                }
                else
                {
                    driver.FindElement(By.XPath("//android.widget.LinearLayout[@resource-id=\"com.netsolace.smsdelivery.qa:id/action_bar_root\"]")).Click();
                }

            }
            catch(Exception ex)
            {
                Assert.Fail("TestCase Fail");
                TestContext.Progress.WriteLine(ex.StackTrace);

            }


        }

        [Test, Order(4)]
        public void failScenrio()
        {
            try
            {
                driver.FindElement(By.XPath("//android.widget.LinearLayout[@resource-id=\"com.netsolace.smsdelivery.qa:id/action_bar_root\"]")).Click();

            }
            catch(Exception ex) { 
                
                Assert.Fail(ex.StackTrace);
                TestContext.Progress.WriteLine(ex.StackTrace);

            }

            }
            public static IEnumerable<TestCaseData> GetTestCasesData()
        {
            yield return new TestCaseData(getData().ExtractData("UserName"), getData().ExtractData("Password"));
        }
        public static IEnumerable<TestCaseData> GetInvalidTestCasesData()
        {
           
            yield return new TestCaseData(getData().ExtractData("InvalidUser"), getData().ExtractData("Invalidpassword"));
        }
    }
}
