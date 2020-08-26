using Booking_Test.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TechTalk.SpecFlow;
using Xunit;

namespace Booking_Test.Pages
{
    class ResultsPage
    {
        private IWebDriver _driver;
        private IJavaScriptExecutor js;
        public ResultsPage(IWebDriver driver)
        {
            _driver = driver;
            js = (IJavaScriptExecutor)_driver;
        }

        internal void verifyIfImOnResultsPage()
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            IWebElement SEARCH_HEADER = _driver.FindElement(By.CssSelector("#right > div:nth-child(4) > div"));
            //Assert.NotNull(SEARCH_HEADER);
        }
        internal void clickSaunaFilter()
        {

            //verify if Sauna is present
            bool containsSauna = false;
            //Collect "Fun things to do" filters
            IReadOnlyCollection<IWebElement> FUN_THINGS_FILTERS = _driver.FindElements(By.CssSelector("[data-name=\"popular_activities\"]"));

            //Check sauna
            foreach (IWebElement FILTER in FUN_THINGS_FILTERS)
            {
                string filterName = FILTER.FindElement(By.CssSelector("label > div > span.filter_label")).Text;
                if (filterName == "Sauna")
                {
                    IWebElement FILTER_CHECKBOX = FILTER.FindElement(By.CssSelector("label > div"));
                    FILTER_CHECKBOX.Click();
                    containsSauna = true;
                    break;
                }
            }

            //To use the "Sauna" filter, we must: Enable "Spa" filter, then Check "Sauna", then uncheck "SPA" filter. Sometimes SPA doesn't come automaticaly
            if (!containsSauna)
            {
                //Collect "Facility" filtrs
                IReadOnlyCollection<IWebElement> FACILITY_FILTERS = _driver.FindElements(By.CssSelector("[data-name=\"hotelfacility\"]"));

                //Click in show more
                IWebElement MORE_FACILITIES = _driver.FindElement(By.CssSelector("#filter_facilities > div.filteroptions > button.collapsed_partly_link.collapsed_partly_more"));
                MORE_FACILITIES.Click();

                //Check "SPA" or "SPA and Wellness Centre"

                foreach (IWebElement FILTER in FACILITY_FILTERS)
                {
                    string filterName = FILTER.FindElement(By.CssSelector("label > div > span.filter_label")).Text;
                    if (filterName == "Spa" || filterName == "Spa and Wellness Centre")
                    {
                        IWebElement FILTER_CHECKBOX = FILTER.FindElement(By.CssSelector("label > div"));
                        FILTER_CHECKBOX.Click();
                        Thread.Sleep(3000);
                        break;
                    }
                }

                //Collect "Fun things to do" filters
                FUN_THINGS_FILTERS = _driver.FindElements(By.CssSelector("[data-name=\"popular_activities\"]"));

                //Check sauna
                foreach (IWebElement FILTER in FUN_THINGS_FILTERS)
                {
                    string filterName = FILTER.FindElement(By.CssSelector("label > div > span.filter_label")).Text;
                    if (filterName == "Sauna")
                    {
                        IWebElement FILTER_CHECKBOX = FILTER.FindElement(By.CssSelector("label > div"));
                        FILTER_CHECKBOX.Click();
                        Thread.Sleep(3000);
                        break;
                    }
                }

                //Uncheck spa from "Fun things to do" area
                FUN_THINGS_FILTERS = _driver.FindElements(By.CssSelector("[data-name=\"popular_activities\"]"));
                foreach (IWebElement FILTER in FUN_THINGS_FILTERS)
                {
                    string filterName = FILTER.FindElement(By.CssSelector("label > div > span.filter_label")).Text;
                    if (filterName == "Spa" || filterName == "Spa and Wellness Centre")
                    {
                        IWebElement FILTER_CHECKBOX = FILTER.FindElement(By.CssSelector("label > div"));
                        if (FILTER_CHECKBOX.Selected)
                        {
                            FILTER_CHECKBOX.Click();
                            Thread.Sleep(3000);
                            break;
                        }
                    }
                }
                //Uncheck spa from facility ares
                FACILITY_FILTERS = _driver.FindElements(By.CssSelector("[data-name=\"hotelfacility\"]"));
                foreach (IWebElement FILTER in FACILITY_FILTERS)
                {
                    string filterName = FILTER.FindElement(By.CssSelector("label > div > span.filter_label")).Text;
                    if (filterName == "Spa" || filterName == "Spa and Wellness Centre")
                    {
                        IWebElement FILTER_CHECKBOX = FILTER.FindElement(By.CssSelector("label > div"));
                        FILTER_CHECKBOX.Click();
                        break;
                        
                    }
                }
                Thread.Sleep(10000);
            }
        }
        internal void clickFiveStarsRating()
        {
            IWebElement FIVE_STAR_RATING = _driver.FindElement(By.CssSelector("#filter_class > div.filteroptions > a:nth-child(3) > label > div"));
            FIVE_STAR_RATING.Click();
            Thread.Sleep(3000);
        }
        internal void validateIfFiveStarHotelsWereListed(string hotelName, string shouldAppear)
        {
            Thread.Sleep(6000);

            if (shouldAppear == "Yes")
            {
                var HOTELS = _driver.FindElements(By.CssSelector("[data-class=\"5\"]"));
                foreach (var element in HOTELS)
                {
                    if (element.Text.Contains(hotelName))
                    {
                        break;
                    }
                    else
                    {
                        throw new Exception(@"The hotel " + hotelName + " should appear on this list, but it doesn't");
                    }
                }
            }
            else
            {
                var HOTELS = _driver.FindElements(By.CssSelector("[data-class=\"5\"]"));
                foreach (var element in HOTELS)
                {
                    if (element.Text.Contains(hotelName))
                    {
                        throw new Exception(@"The hotel " + hotelName + " was found, but it doesn't");
                    }
                }
            }

        }

        internal void validateIfSaunaHotelsWereListed(string hotelName, string shouldAppear)
        {
            Thread.Sleep(6000);

            var HOTEL = _driver.FindElements(By.XPath("//*[contains(., '" + hotelName + "')]"));

            if (shouldAppear == "Yes")
            {
                if (HOTEL.Count <= 0)
                    throw new Exception(@"The hotel " + hotelName + " wasn't found, but it should be");
            }
            else
            {
                if (HOTEL.Count > 0)
                    throw new Exception(@"The hotel " + hotelName + " was found, but doesn't should be");
            }

        }

        internal void verifyFiveStar()
        {
            Thread.Sleep(6000);
            var HOTELS = _driver.FindElements(By.CssSelector("[data-class=\"5\"]"));
            foreach (var element in HOTELS)
            {
                var STAR = element.FindElement(By.CssSelector("[title=\"5-star hotel\"]"));
            }
        }
    }
}
