using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking_Test.Utils
{
    class DriverUtils
    {
        private string DriverDirectory;
        private EdgeOptions edgeOptions;
        private ChromeOptions options;
        private IWebDriver _driver;

        public IWebDriver CreateDriver(string browser)
        {
            DriverDirectory = "C:\\Windows";
            
            if(browser == "Chrome")
            {
                options = new ChromeOptions();
                _driver = new ChromeDriver(DriverDirectory, options, TimeSpan.FromMinutes(2));
            }
            if(browser == "Edge")
            {
                edgeOptions = new EdgeOptions();
                _driver = new EdgeDriver(DriverDirectory, edgeOptions, TimeSpan.FromMinutes(2));
            }

            _driver.Manage().Window.Maximize();
            return _driver;
        }
        
        public void DriverNavigate(IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public void DestroyDriver(IWebDriver driver)
        {
            driver.Dispose();
        }
    }
}
