using Conta.Application.ViewModels;
using System.Collections.Generic;

namespace Conta.Application.Interfaces
{
    public interface IContaCorrenteApplication
    {
        void Adicionar(ContaCorrenteModel entity);

        ContaCorrenteModel ObterPorId(int id);

        IEnumerable<ContaCorrenteModel> ObterTodos();

        void Atualizar(ContaCorrenteModel entity);
    }
}
