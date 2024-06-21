using mantisBT_AutomationTest.paginas;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

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

        public void selecionaOpcaoNoSelect(string seletor, string opcao)
        {
            IWebElement selectElement = driver.FindElement(By.Id(seletor));

            SelectElement dropdown = new SelectElement(selectElement);

            dropdown.SelectByText(opcao);

            Assert.That(dropdown.SelectedOption.Text, Is.EqualTo(opcao));
        }

        [Test]
        public void cadastrarTarefaPreenchendoDadosValidos1()
        {
            acessarPaginaCriarTarefa();

            //Campo categoria
            selecionaOpcaoNoSelect("category_id", "[Todos os Projetos] categoria teste");

            //Campo frequencia
            selecionaOpcaoNoSelect("reproducibility", "sempre");

            //Campo gravidade
            selecionaOpcaoNoSelect("severity", "texto");
            
            //Campo prioridade
            selecionaOpcaoNoSelect("priority", "normal");

            //Campo selecionar perfil
            selecionaOpcaoNoSelect("profile_id", "Notebook ideapad3 windows 11");



        }


    }
}
