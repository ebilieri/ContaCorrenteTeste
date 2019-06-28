using Conta.Domain.Entities;
using Conta.Domain.Interfaces.IRepositories;
using Conta.Domain.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace Conta.Domain.Services
{
    public class ContaCorrenteService : IContaCorrenteService
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;

        public ContaCorrenteService(IContaCorrenteRepository contaCorrenteRepository)
        {
            _contaCorrenteRepository = contaCorrenteRepository;
        }

        public void Adicionar(ContaCorrente entity)
        {
            entity.Validate();

            if (entity.EhValido)
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    _contaCorrenteRepository.Adicionar(entity);

                    if (entity.IdConta > 0)
                    {
                        var movimentacao = new Movimentacao
                        {
                            ContaId = entity.IdConta,
                            DataMovimentacao = DateTime.Now,
                            TipoOperacao = Enumerators.TipoOperacaoEnum.CriacaoConta,
                            Valor = entity.ValorAtual,
                            ValorAtual = entity.ValorAtual
                        };

                        _contaCorrenteRepository.CriarSaldoInicial(movimentacao);
                    }

                    scope.Complete();
                }
            }
        }
        
        public ContaCorrente ObterPorId(int id)
        {
            return _contaCorrenteRepository.ObterPorId(id);
        }
        
        public IEnumerable<ContaCorrente> ObterTodos()
        {
            return _contaCorrenteRepository.ObterTodos();
        }        
    }
}
