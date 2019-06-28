using Conta.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Conta.Domain.Interfaces.IRepositories
{
    public interface IMovimentacaoRepository
    {
        void Adicionar(Movimentacao entity);

        IEnumerable<Movimentacao> ObterPorIdConta(int idConta);

    }
}
