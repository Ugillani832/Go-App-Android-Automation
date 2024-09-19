using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AppiumCsharpFramework.Page_Objects
{
   class PObjects
    {
       
        private IWebDriver driver;
      

        public PObjects(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "et_username")]
        private IWebElement UserNameField;

        [FindsBy(How = How.Id, Using = "et_password")]
        private IWebElement PassWordField;

        [FindsBy(How = How.Id, Using = "btn_sign_in")]
        private IWebElement SignBtn;

        public IWebElement UserName()
        {
            return UserNameField;
        }

        public IWebElement Passwrd()
        {
            return PassWordField;
        }

        public IWebElement SignIn()
        {
            return SignBtn;
        }


    }
}
