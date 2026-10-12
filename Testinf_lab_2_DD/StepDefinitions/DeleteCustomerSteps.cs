using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Reqnroll;
using Lab2.PageObjects;

namespace Lab2.StepDefinitions
{
    [Binding]
    public class DeleteCustomerSteps
    {
        private IWebDriver _driver;
        private BankingManagerPage _bankingManagerPage;

        [BeforeScenario]
        public void Setup()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _bankingManagerPage = new BankingManagerPage(_driver);
        }

        [Given(@"Я перебуваю на головній сторінці банку")]
        public void GivenIAmOnTheBankingProjectLoginPage()
        {
            _bankingManagerPage.GoToUrl("https://www.globalsqa.com/angularJs-protractor/BankingProject/#/login");
        }

        [When(@"Я натискаю кнопку Bank Manager Login")]
        public void WhenIClickOnBankManagerLogin()
        {
            _bankingManagerPage.ClickBankManagerLogin();
        }

        [When(@"Я переходжу на вкладку Customers")]
        public void WhenINavigateToTheCustomersTab()
        {
            _bankingManagerPage.GoToCustomersTab();
        }

        [When(@"Я шукаю клієнта з іменем ""(.*)""")]
        public void WhenISearchForCustomer(string customerName)
        {
            _bankingManagerPage.SearchCustomer(customerName);
        }

        [When(@"Я натискаю кнопку Delete біля знайденого клієнта")]
        public void WhenIClickTheDeleteButton()
        {
            _bankingManagerPage.ClickDeleteButton();
        }

        [Then(@"Клієнт ""(.*)"" повинен зникнути зі списку")]
        public void ThenTheCustomerShouldNoLongerBeInTheList(string customerName)
        {
            _bankingManagerPage.SearchCustomer(customerName);
            bool isPresent = _bankingManagerPage.IsCustomerInList(customerName);
            Assert.That(isPresent, Is.False, $"Клієнт {customerName} не був видалений зі списку.");
        }

        [AfterScenario]
        public void TearDown()
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver.Dispose();
            }
        }
    }
}