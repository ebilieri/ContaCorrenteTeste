using Conta.Domain.Entities;
using Conta.Domain.Interfaces.IRepositories;
using Conta.Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Conta.Tests
{
    [TestClass]
    public class ContaCorrenteServiceTest : UnitTestBase
    {
        private readonly Mock<IContaCorrenteRepository> _contaCorrenteRepositoryMock;
        public ContaCorrenteServiceTest()
        {
            _contaCorrenteRepositoryMock = new Mock<IContaCorrenteRepository>();
        }

        [TestMethod]
        public void ContaCorrenteService_Adicionar_Return_Be_Sucess()
        {
            var contaCorrente = new ContaCorrente
            {
                ValorAtual = 100
            };


            var contaCorrenteService = new ContaCorrenteService(_contaCorrenteRepositoryMock.Object);
            contaCorrenteService.Adicionar(contaCorrente);

            _contaCorrenteRepositoryMock.Verify(r => r.Adicionar(
                It.Is<ContaCorrente>(v => v.ValorAtual == contaCorrente.ValorAtual)));

        }

        [TestMethod]
        public void ContaCorrenteService_ObterPorId_Return_Be_Sucess()
        {
            _contaCorrenteRepositoryMock.Setup(x => x.ObterPorId(1)).Returns(new ContaCorrente
            {
                IdConta = 1,
                ValorAtual = 100
            });

            var contaCorrenteService = new ContaCorrenteService(_contaCorrenteRepositoryMock.Object);
            var contaCorrente = contaCorrenteService.ObterPorId(1);

            _contaCorrenteRepositoryMock.Verify(r => r.ObterPorId(
                It.Is<int>(v => v == contaCorrente.IdConta)));
        }

        
    }
}
