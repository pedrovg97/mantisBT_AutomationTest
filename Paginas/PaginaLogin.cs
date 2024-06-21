using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace mantisBT_AutomationTest.paginas
{
    public class PaginaLogin
    {
        public IWebDriver driver;
        public WebDriverWait wait;

        [SetUp]
        public void carregarPaginaLogin()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.Navigate().GoToUrl("https://mantis-prova.base2.com.br/login_page.php");
            driver.Manage().Window.Maximize();
            realizarLogin();
        }

        public void botaoEntrar()
        {
            var button = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("input[type='submit'][value='Entrar']")));
            button.Click();
        }
        public void realizarLogin()
        {
            driver.FindElement(By.Id("username")).SendKeys("Pedro_Bento");
            botaoEntrar();
            driver.FindElement(By.Id("password")).SendKeys("pedroobnt");
            botaoEntrar();
        }
    }
}