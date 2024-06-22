using mantisBT_AutomationTest.paginas;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Reflection.Metadata;

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

        public void enviaArquivos(string seletor, string caminho, string tamanho)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            IWebElement uploadElement = driver.FindElement(By.CssSelector(seletor));
            uploadElement.SendKeys(caminho); 
            System.Threading.Thread.Sleep(5000);

            try
            {
                if (wait.Until(ExpectedConditions.AlertIsPresent()) != null && tamanho == "Grande")
                {
                    Assert.IsTrue(true, "Sistema alerta para arquivo grande de mais.");
                }
            }
            catch (WebDriverTimeoutException)
            {
                if (tamanho == "Pequeno")
                {
                    Assert.IsTrue(true, "Arquivo válido.");
                }
                else
                {
                    Assert.Fail("Sistema permitiu insersao de arquivo grande de mais.");
                }
            }
        }

        public void preencheCampoDeTexto(string seletor, string texto)
        {
            IWebElement campoTexto = driver.FindElement(By.Id(seletor));
            campoTexto.SendKeys(texto);
            Assert.AreEqual(texto, campoTexto.GetAttribute("value"), "Texto incorreto inserido no campo");
        }

        //este metodo verifica se os valores preenchidos são salvos corretamente e aparecem em ver detalhes da tarefa
        public void verificaValores(string seletor, string valorEsperado)
        {
            IWebElement valor = driver.FindElement(By.XPath(seletor));
            Assert.That(valor.Text, Is.EqualTo(valorEsperado));
        }

        [Test]
        public void testCriacaoDeTarefaComCamposObrigatoriosEArquivoValido()
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

            //Campo resumo
            preencheCampoDeTexto("summary", "Texto teste");

            //Campo descricao
            preencheCampoDeTexto("description", "Texto teste");

            //Campo passos para reproduzir
            preencheCampoDeTexto("steps_to_reproduce", "");

            //Campo informacoes adicionais
            preencheCampoDeTexto("additional_info", "Texto teste");

            //Campo aplicar marcadores
            driver.FindElement(By.Id("tag_select")).SendKeys("Texto");

            //Campo enviar arquivos 
            enviaArquivos("input[type=file]", "C:\\Users\\pedro\\OneDrive\\Documentos\\Projetos\\mantisBT_AutomationTest\\Arquivos para teste\\arquivoTesteMenorQue2.097kb.pdf", "Pequeno");

            //Campo visibilidade
            driver.FindElement(By.XPath("//span[text()='público']")).Click();

            //Clicar no botão criar tarefa
            driver.FindElement(By.CssSelector("input[type='submit'][value='Criar Nova Tarefa']")).Click();

            TimeSpan.FromSeconds(3);

            verificaValores("//td[@class='bug-priority']", "normal");
            verificaValores("//td[@class='bug-severity']", "texto");
            verificaValores("//td[@class='bug-reproducibility']", "sempre");
            verificaValores("//td[@class='bug-description']", "Texto teste");
            verificaValores("//td[@class='bug-additional-information']", "Texto teste");
            verificaValores("//td[@class='bug-view-status']", "público");
            verificaValores("//td[@class='bug-category']", "[Todos os Projetos] categoria teste");

        }
        [Test]
        public void testCadastrarTarefaComArquivoMaiorQue2MB()
        {
            acessarPaginaCriarTarefa();

            //Campo enviar arquivos 
            enviaArquivos("input[type=file]", "C:\\Users\\pedro\\OneDrive\\Documentos\\Projetos\\mantisBT_AutomationTest\\Arquivos para teste\\arquivotesteMaiorQue2.097kb.jpg", "Grande");

        }

        [Test]
        public void testCadastrarTarefaComCampoCategoriaVazio()
        {
            acessarPaginaCriarTarefa();

            //Campo frequencia
            selecionaOpcaoNoSelect("reproducibility", "aleatório");

            //Campo gravidade
            selecionaOpcaoNoSelect("severity", "pequeno");

            //Campo prioridade
            selecionaOpcaoNoSelect("priority", "nenhuma");

            //Campo selecionar perfil
            selecionaOpcaoNoSelect("profile_id", "Notebook ideapad3 windows 11");

            //Campo resumo
            preencheCampoDeTexto("summary", "Texto teste");

            //Campo descricao
            preencheCampoDeTexto("description", "Texto teste");

            //Campo passos para reproduzir
            preencheCampoDeTexto("steps_to_reproduce", "Texto teste");

            //Campo informacoes adicionais
            preencheCampoDeTexto("additional_info", "Texto teste");

            //Campo aplicar marcadores
            driver.FindElement(By.Id("tag_select")).SendKeys("Texto");

            //Campo visibilidade
            driver.FindElement(By.XPath("//span[text()='público']")).Click();

            //Clicar no botão criar tarefa
            driver.FindElement(By.CssSelector("input[type='submit'][value='Criar Nova Tarefa']")).Click();

            Assert.That(driver.Url, Is.EqualTo("https://mantis-prova.base2.com.br/bug_report.php?posted=1"), "Sistema nao permitiu cadastro");

        }
        [Test]
        public void testCadastrarTarefaComCampoResumoVazio()
        {
            acessarPaginaCriarTarefa();

            //Campo resumo
            preencheCampoDeTexto("summary", "");

            //Campo descricao
            preencheCampoDeTexto("description", "Texto teste");
         
            //Clicar no botão criar tarefa
            driver.FindElement(By.CssSelector("input[type='submit'][value='Criar Nova Tarefa']")).Click();

            Assert.That(driver.Url, Is.EqualTo("https://mantis-prova.base2.com.br/bug_report_page.php"), "Sistema nao permitiu cadastro");

        }
        [Test]
        public void testCadastrarTarefaComCampoDescricaoVazio()
        {
            acessarPaginaCriarTarefa();

            //Campo resumo
            preencheCampoDeTexto("summary", "Texto teste");

            //Campo descricao
            preencheCampoDeTexto("description", "");

            //Clicar no botão criar tarefa
            driver.FindElement(By.CssSelector("input[type='submit'][value='Criar Nova Tarefa']")).Click();

            Assert.That(driver.Url, Is.EqualTo("https://mantis-prova.base2.com.br/bug_report_page.php"), "Sistema nao permitiu cadastro");

        }

    }
}