using Booking_Test.Pages;
using Booking_Test.Utils;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace Booking_Test.Steps
{
    [Binding]
    public class FilterSteps
    {
        private IWebDriver driver;
        private MainPage mainPage;
        private ResultsPage resultsPage;
        private string url;
        private DriverUtils driverUtils;

        public FilterSteps()
        {
            url = @"https://www.booking.com";
            driverUtils = new DriverUtils();
        }

        [Given(@"I am using '(.*)' browser")]
        public void GivenIAmUsingBrowser(string browser)
        {
            driver = driverUtils.CreateDriver(browser);

            mainPage = new MainPage(driver);
            resultsPage = new ResultsPage(driver);
        }


        [Given(@"I am on Booking main page")]
        public void GivenIAmOnBookingMainPage()
        {
            
            driverUtils.DriverNavigate(driver, url);
            mainPage.verifyIfImOnMainPage();

        }
        
        [Given(@"I fill the search fields")]
        public void GivenIFillTheSearchFields(Table searchInfo)
        {
            
            mainPage.fillSearchFields(searchInfo);
        }
        
        [Given(@"I click on Search button")]
        public void GivenIClickOnSearchButton()
        {
            mainPage.clickSearchButton();
        }
        
        [Given(@"I am on Results Page")]
        public void GivenIAmOnResultsPage()
        {
            resultsPage.verifyIfImOnResultsPage();
        }
        
        [When(@"I click on five star filter")]
        public void WhenIClickOnStarFilter()
        {
            resultsPage.clickFiveStarsRating();
        }
        
        [When(@"I click on Sauna filter")]
        public void WhenIClickOnSaunaFilter()
        {
            resultsPage.clickSaunaFilter();
        }


        [Then(@"I will be able to validate if five star (.*) (.*)")]
        public void ThenIWillBeAbleToValidateIfFiveStar(string hotelName, string shouldAppear)
        {
            resultsPage.validateIfFiveStarHotelsWereListed(hotelName, shouldAppear);
        }

        [Then(@"I should be able to validate if (.*) (.*)")]
        public void ThenIShouldBeAbleToValidateIf(string hotelName, string shouldAppear)
        {
            resultsPage.validateIfSaunaHotelsWereListed(hotelName, shouldAppear);
        }
        
        [Then(@"I should see five stars in each ad")]
        public void ThenIShouldSeeStarsInEachAd()
        {
            resultsPage.verifyFiveStar();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            driverUtils.DestroyDriver(driver);
        }
    }
}
