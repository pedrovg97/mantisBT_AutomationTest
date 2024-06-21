using mantisBT_AutomationTest.paginas;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace mantisBT_AutomationTest.Testes
{
    public class TesteCriarTarefa : PaginaLogin
    {
        [Test]
        public void acessarPaginaCriarTarefa()
        {
            var button1 = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(".btn.btn-primary.btn-sm")));
            button1.Click();
            Assert.IsTrue(driver.Url.Contains("https://mantis-prova.base2.com.br/bug_report_page.php"));
        }

        public void cadastrartarefaPreenchendo()
        {


        }


    }
}
