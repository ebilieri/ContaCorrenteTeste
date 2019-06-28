using Conta.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Conta.Domain.Interfaces.IServices
{
    public interface IContaCorrenteService
    {        
        void Adicionar(ContaCorrente entity);              

        ContaCorrente ObterPorId(int id);

        IEnumerable<ContaCorrente> ObterTodos();        
    }
}
