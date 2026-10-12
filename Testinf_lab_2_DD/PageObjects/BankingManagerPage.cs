using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Lab2.PageObjects
{
    public class BankingManagerPage
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        public BankingManagerPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        // Локатори
        private By bankManagerLoginBtn = By.XPath("//button[contains(text(), 'Bank Manager Login')]");
        private By customersTabBtn = By.XPath("//button[contains(text(), 'Customers')]");
        private By searchCustomerInput = By.XPath("//input[@placeholder='Search Customer']");
        private By deleteBtn = By.XPath("//button[contains(text(), 'Delete')]");
        private By tableRows = By.XPath("//table/tbody/tr");

        // Методи сторінки
        public void GoToUrl(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public void ClickBankManagerLogin()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(bankManagerLoginBtn)).Click();
        }

        public void GoToCustomersTab()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(customersTabBtn)).Click();
        }

        public void SearchCustomer(string name)
        {
            var searchInput = _wait.Until(ExpectedConditions.ElementIsVisible(searchCustomerInput));
            searchInput.Clear();
            searchInput.SendKeys(name);
        }

        public void ClickDeleteButton()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(deleteBtn)).Click();
        }

        public bool IsCustomerInList(string name)
        {
            // Невелика пауза, щоб Angular оновив DOM після пошуку чи видалення
            System.Threading.Thread.Sleep(1000);
            var rows = _driver.FindElements(tableRows);
            foreach (var row in rows)
            {
                if (row.Text.Contains(name))
                {
                    return true;
                }
            }
            return false;
        }
    }
}