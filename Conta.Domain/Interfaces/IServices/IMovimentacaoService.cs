using Conta.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Conta.Domain.Interfaces.IServices
{
    public interface IMovimentacaoService
    {       
        IEnumerable<Movimentacao> ObterPorIdConta(int idConta);

        void Debitar(Movimentacao obj);

        void Creditar(Movimentacao obj);
    }
}
