using System;
using System.Collections.Generic;
using Conta.Domain.Entities;
using Conta.Domain.Interfaces.IRepositories;
using Conta.Domain.Interfaces.IServices;

namespace Conta.Domain.Services
{
    public class MovimentacaoService : IMovimentacaoService
    {
        private readonly IMovimentacaoRepository _movimentacaoRepository;
        private readonly IContaCorrenteRepository _contaCorrenteRepository;

        public MovimentacaoService(IMovimentacaoRepository movimentacaoRepository, IContaCorrenteRepository contaCorrenteRepository)
        {
            _movimentacaoRepository = movimentacaoRepository;
            _contaCorrenteRepository = contaCorrenteRepository;
        }

        public void Creditar(Movimentacao entity)
        {
            if (entity.Valor <= 0)
            {
                entity.MensagemValidacao.Add("Informe um Valor Positivo para Creditar.");
                return;
            }

            entity.TipoOperacao = Enumerators.TipoOperacaoEnum.Debito;
            var contaCorrente = _contaCorrenteRepository.ObterPorId(entity.ContaId);

            entity.DataMovimentacao = DateTime.Now;
            entity.TipoOperacao = Enumerators.TipoOperacaoEnum.Credito;
            // Creditar
            entity.ValorAtual = contaCorrente.ValorAtual + entity.Valor;

            

            // adicionar movimentacao
            _movimentacaoRepository.Adicionar(entity);

            // Atualizar saldo conta corrente
            if (entity.IdMovimentacao > 0)
            {
                contaCorrente.ValorAtual = entity.ValorAtual;
                _contaCorrenteRepository.Atualizar(contaCorrente);
            }
        }

        public void Debitar(Movimentacao entity)
        {            
            var contaCorrente = _contaCorrenteRepository.ObterPorId(entity.ContaId);

            entity.DataMovimentacao = DateTime.Now;
            entity.TipoOperacao = Enumerators.TipoOperacaoEnum.Debito;
            //Debitar
            entity.ValorAtual = contaCorrente.ValorAtual - entity.Valor;

            if (entity.Valor <= 0)
            {
                entity.MensagemValidacao.Add("Informe um Valor Positivo para Debitar.");
                return;
            }

            if (contaCorrente.ValorAtual < entity.Valor)
            {
                entity.MensagemValidacao.Add($"Não há saldo suficiente para Debitar. Saldo Atual: {contaCorrente.ValorAtual} ");
                return;
            }

            // adicionar movimentacao
            _movimentacaoRepository.Adicionar(entity);

            // Atualizar saldo conta corrente
            if (entity.IdMovimentacao > 0)
            {
                contaCorrente.ValorAtual = entity.ValorAtual;
                _contaCorrenteRepository.Atualizar(contaCorrente);
            }
        }
             
        public IEnumerable<Movimentacao> ObterPorIdConta(int idConta)
        {
            return _movimentacaoRepository.ObterPorIdConta(idConta);
        }

       
    }
}
