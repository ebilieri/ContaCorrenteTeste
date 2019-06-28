using Conta.Application;
using Conta.Application.Interfaces;
using Conta.Application.ViewModels;
using Conta.Domain.Entities;
using Conta.Domain.Interfaces.IServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Conta.Tests
{
    [TestClass]
    public class ContaCorrenteApplicationTest : UnitTestBase
    {
        private readonly Mock<IContaCorrenteService> _contaCorrenteServiceMock;
        private readonly Mock<IContaCorrenteApplication> _contaCorrenteApplicationMock;

        public ContaCorrenteApplicationTest()
        {
            _contaCorrenteServiceMock = new Mock<IContaCorrenteService>();
        }

        [TestMethod]
        public void ContaCorrenteApplication_Adicionar_Return_Be_Sucess()
        {
            var contaCorrente = new ContaCorrenteModel
            {
                ValorAtual = 100
            };

            var contaCorrenteApplication = new ContaCorrenteApplication(_contaCorrenteServiceMock.Object);
            contaCorrenteApplication.Adicionar(contaCorrente);

            _contaCorrenteServiceMock.Verify(r => r.Adicionar(
                It.Is<ContaCorrente>(v => v.ValorAtual == contaCorrente.ValorAtual)));

        }

        
    }
}