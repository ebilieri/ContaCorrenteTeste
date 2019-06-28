using Conta.Application.ViewModels;
using System.Collections.Generic;

namespace Conta.Application.Interfaces
{
    public interface IMovimentacaoApplication
    {
       
        IEnumerable<MovimentacaoModel> ObterPorIdConta(int idConta);
        void Debitar(MovimentacaoModel model);
        void Creditar(MovimentacaoModel model);
    }
}
