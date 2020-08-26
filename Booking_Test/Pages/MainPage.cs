using Booking_Test.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace Booking_Test.Pages
{
    class MainPage
    {
        private IWebDriver _driver;
        private IJavaScriptExecutor js;

        public MainPage(IWebDriver driver)
        {
            _driver = driver;
            js = (IJavaScriptExecutor)_driver;
        }

        internal void verifyIfImOnMainPage()
        {
            IWebElement SEARCH_TITLE = _driver.FindElement(By.CssSelector("span.sb-searchbox__title-text"));
        }

        internal void fillSearchFields(Table _searchInfo)
        {
            var searchInfo = TableUtils.convertTableToDictionary(_searchInfo);
            foreach(var index in searchInfo)
            {
                switch (index.Key)
                {
                    case "Location":
                        IWebElement LOCATION = _driver.FindElement(By.Id("ss"));
                        LOCATION.SendKeys(index.Value);
                        break;

                    case "Dates":
                        DateTime now = DateTime.Now;

                        //Click on calendar area
                        js.ExecuteScript("window.document.querySelector('#frm > div.xp__fieldset.accommodation > div.xp__dates.xp__group > div.xp__dates-inner > div:nth-child(2) > div > div > div').click()");
                        
                        //Assume that every time that a clean browser open this site, the left month always will be the current one, we go 3 months from today's date
                        for (int i=0; i<3; i++) 
                        {
                            js.ExecuteScript("window.document.querySelector('#frm > div.xp__fieldset.accommodation > div.xp__dates.xp__group > div.xp-calendar > div > div > div.bui-calendar__control.bui-calendar__control--next').click()");
                        }

                        //Finding correspondent checkin and checkout days using the same number of current date
                        IWebElement LEFT_CALENDAR = _driver.FindElement(By.CssSelector(@"#frm > div.xp__fieldset.accommodation > div.xp__dates.xp__group > div.xp-calendar > div > div > div.bui-calendar__content > div:nth-child(1) > table > tbody"));
                        IReadOnlyCollection<IWebElement> columns = LEFT_CALENDAR.FindElements(By.TagName("td"));

                        foreach (IWebElement cell in columns)
                        {
                            //checkin
                            if (cell.Text == now.Day.ToString())
                            {
                                cell.Click();
                            }
                            //checkout: next day
                            if (cell.Text == (now.Day + 1).ToString())
                            {
                                cell.Click();
                                break;
                            }
                           
                        }
                        break;

                    case "Number of adults":
                        //open guest and room settings
                        _driver.FindElement(By.CssSelector("#xp__guests__toggle > span.xp__guests__count")).Click();
                        
                        IWebElement NUMBER_ADULTS = _driver.FindElement(By.CssSelector("#xp__guests__inputs-container > div > div > div.sb-group__field.sb-group__field-adults > div > div.bui-stepper__wrapper.sb-group__stepper-a11y > span.bui-stepper__display"));
                        if (NUMBER_ADULTS.Text == index.Value)
                        {
                            break;
                        }
                        else
                        {
                            if (Int32.Parse(NUMBER_ADULTS.Text) < Int32.Parse(index.Value))
                            {
                                int difference = Int32.Parse(index.Value) - Int32.Parse(NUMBER_ADULTS.Text);
                                IWebElement INCREASE_NUMBER_ADULTS = _driver.FindElement(By.CssSelector("#xp__guests__inputs-container > div > div > div.sb-group__field.sb-group__field-adults > div > div.bui-stepper__wrapper.sb-group__stepper-a11y > button.bui-button.bui-button--secondary.bui-stepper__add-button"));
                                for(int i=0; i<difference; i++)
                                {
                                    INCREASE_NUMBER_ADULTS.Click();
                                }
                            }
                            else
                            {
                                IWebElement DECREASE_NUMBER_ADULTS = _driver.FindElement(By.CssSelector("#xp__guests__inputs-container > div > div > div.sb-group__field.sb-group__field-adults > div > div.bui-stepper__wrapper.sb-group__stepper-a11y > button.bui-button.bui-button--secondary.bui-stepper__subtract-button"));
                                int difference = Int32.Parse(NUMBER_ADULTS.Text) - Int32.Parse(index.Value);
                                for (int i = 0; i < difference; i++)
                                {
                                    DECREASE_NUMBER_ADULTS.Click();
                                }
                            }
                        }   
                        break;

                    case "Number of Rooms":
                        IWebElement NUMBER_ROOMS = _driver.FindElement(By.CssSelector("#xp__guests__inputs-container > div > div > div.sb-group__field.sb-group__field-rooms > div > div.bui-stepper__wrapper.sb-group__stepper-a11y > span.bui-stepper__display"));
                        if (NUMBER_ROOMS.Text == index.Value)
                        {
                            break;
                        }
                        else
                        {
                            if (Int32.Parse(NUMBER_ROOMS.Text) < Int32.Parse(index.Value))
                            {
                                int difference = Int32.Parse(index.Value) - Int32.Parse(NUMBER_ROOMS.Text);
                                IWebElement INCREASE_NUMBER_ROOMS = _driver.FindElement(By.CssSelector("#xp__guests__inputs-container > div > div > div.sb-group__field.sb-group__field-rooms > div > div.bui-stepper__wrapper.sb-group__stepper-a11y > button.bui-button.bui-button--secondary.bui-stepper__add-button"));
                                for (int i = 0; i < difference; i++)
                                {
                                    INCREASE_NUMBER_ROOMS.Click();
                                }
                            }
                            else
                            {
                                IWebElement DECREASE_NUMBER_ROOMS = _driver.FindElement(By.CssSelector("#xp__guests__inputs-container > div > div > div.sb-group__field.sb-group__field-rooms > div > div.bui-stepper__wrapper.sb-group__stepper-a11y > button.bui-button.bui-button--secondary.bui-stepper__subtract-button"));
                                int difference = Int32.Parse(NUMBER_ROOMS.Text) - Int32.Parse(index.Value);
                                for (int i = 0; i < difference; i++)
                                {
                                    DECREASE_NUMBER_ROOMS.Click();
                                }
                            }
                        }
                        break;
                }
            }
        }

        internal void clickSearchButton()
        {
            IWebElement SEARCH_BUTTON = _driver.FindElement(By.CssSelector("#frm > div.xp__fieldset.accommodation > div.xp__button > div.sb-searchbox-submit-col.-submit-button > button"));
            SEARCH_BUTTON.Click();
        }

        
    }
}
