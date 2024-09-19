using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using AventStack.ExtentReports.Reporter;

using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal.Commands;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.iOS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppiumCsharpFramework.Utilities
{
    class Base
    {
       

        public ExtentReports extent;
        public ExtentTest test;
       // [System.Diagnostics.CodeAnalysis.SuppressMessage("Structure", "NUnit1032:An IDisposable field/property should be Disposed in a TearDown method", Justification = "<Pending>")]
        // public ThreadLocal<AndroidElement> driver = new ThreadLocal<AndroidElement>();



        [OneTimeSetUp]
        public void setup()
        {
            string WorkingDirectory = Environment.CurrentDirectory;
            string ProjectDirectory = Directory.GetParent(WorkingDirectory).Parent.Parent.FullName;
            string reportpath = ProjectDirectory + "//Report.html";
       
            var HtmlReporter = new ExtentSparkReporter(reportpath);


             extent = new ExtentReports();
            extent.AttachReporter(HtmlReporter);
            extent.AddSystemInfo("Enviroment", "QA");
            extent.AddSystemInfo("Tester Name", "Umair Gillani");
            extent.AddSystemInfo("Device Name", "Redmi");
           // ExtentTest Testo = extent.CreateTest("Umair_Test");
            

        }
        public AndroidDriver<AndroidElement> driver;
        [SetUp]
        public void start()
        {
            test = null;
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            
            AppiumOptions caps = new AppiumOptions();
            caps.AddAdditionalCapability("deviceName", "R4SSHEN77XXK8HNB");
            caps.AddAdditionalCapability("platformVersion", "13");
            caps.AddAdditionalCapability("platformName", "Andrioid");
            caps.AddAdditionalCapability("appPackage", "com.netsolace.smsdelivery.qa");

            caps.AddAdditionalCapability("appActivity", "com.netsolace.godelivery.SplashActivity");
            caps.AddAdditionalCapability("autoGrantPermissions", "true");

           // caps.AddAdditionalCapability("noReset", "true");


            Uri URI = new Uri("http://127.0.0.1:4723/wd/hub");
            driver = new AndroidDriver<AndroidElement>(URI, caps);

        }
        public static JsonReader getData()
        {
            return new JsonReader();
        }
        [TearDown]
        public void AfterSetup()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var Stacktrace = TestContext.CurrentContext.Result.StackTrace;
            DateTime time = DateTime.Now;
            string filename = "Screenshot" + time.ToString("h_mm_ss") + ".png";

            if(status == TestStatus.Failed)
            {
                test.Fail("Test Failed", CaptureScreenshot(driver, filename));
                test.Log(Status.Fail, "Test Failed with LogTrace" + Stacktrace);

            }

            if (status == TestStatus.Passed)
            {
                test.Pass("Test Passed", CaptureScreenshot(driver, filename));
                // test.Log(Status.Pass);
               
                   //test.Pass(MediaEntityBuilder.CreateScreenCaptureFromPath("extent.png").Build());
            }
            if (status == TestStatus.Skipped) {

                test.Skip("Test Skipped", CaptureScreenshot(driver, filename));
                test.Log(Status.Skip, "Test Got Skipped" + Stacktrace);
            }
            extent.Flush();
        driver.Dispose();

        }

        private Media CaptureScreenshot(AndroidDriver<AndroidElement> driver, string ScreenshotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
           
            var ScreenShot = ts.GetScreenshot().AsBase64EncodedString;


            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(ScreenShot,ScreenshotName).Build();

            
        }
    }
}
