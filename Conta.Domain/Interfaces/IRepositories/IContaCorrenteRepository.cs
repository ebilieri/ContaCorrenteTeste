using Conta.Domain.Entities;
using System.Collections.Generic;

namespace Conta.Domain.Interfaces.IRepositories
{
    public interface IContaCorrenteRepository
    {
        void Adicionar(ContaCorrente entity);

        ContaCorrente ObterPorId(int id);

        IEnumerable<ContaCorrente> ObterTodos();

        void CriarSaldoInicial(Movimentacao entity);

        void Atualizar(ContaCorrente contaCorrente);
    }
}
