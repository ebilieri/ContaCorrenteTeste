using AutoMapper;
using Conta.Application.Interfaces;
using Conta.Application.ViewModels;
using Conta.Domain.Entities;
using Conta.Domain.Interfaces.IServices;
using Conta.Application.Extensions;
using System.Collections.Generic;

namespace Conta.Application
{
    public class MovimentacaoApplication : IMovimentacaoApplication
    {
        private readonly IMovimentacaoService _contaCorrenteService;

        public MovimentacaoApplication(IMovimentacaoService veiculoService)
        {
            _contaCorrenteService = veiculoService;
        }              

        public void Creditar(MovimentacaoModel entity)
        {
            var obj = entity.MapTo<Movimentacao>();

            _contaCorrenteService.Creditar(obj);

            entity.IdMovimentacao = obj.IdMovimentacao;
            entity.MensagemValidacao = obj.MensagemValidacao;
        }

        public void Debitar(MovimentacaoModel entity)
        {
            var obj = entity.MapTo<Movimentacao>();

            _contaCorrenteService.Debitar(obj);

            entity.IdMovimentacao = obj.IdMovimentacao;
            entity.MensagemValidacao = obj.MensagemValidacao;
        }

       
        public IEnumerable<MovimentacaoModel> ObterPorIdConta(int idConta)
        {
            var obj = Mapper.Map<IEnumerable<Movimentacao>, IEnumerable<MovimentacaoModel>>(_contaCorrenteService.ObterPorIdConta(idConta));

            return obj;
        }

        
    }
}
