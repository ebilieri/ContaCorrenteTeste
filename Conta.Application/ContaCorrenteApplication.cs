using AutoMapper;
using Conta.Application.Interfaces;
using Conta.Application.ViewModels;
using Conta.Domain.Entities;
using Conta.Domain.Interfaces.IServices;
using Conta.Application.Extensions;
using System.Collections.Generic;
using System;

namespace Conta.Application
{
    public class ContaCorrenteApplication : IContaCorrenteApplication
    {
        private readonly IContaCorrenteService _contaCorrenteService;

        public ContaCorrenteApplication(IContaCorrenteService veiculoService)
        {
            _contaCorrenteService = veiculoService;
        }

        public void Adicionar(ContaCorrenteModel entity)
        {            
            var obj = entity.MapTo<ContaCorrente>();
            
            _contaCorrenteService.Adicionar(obj);
           
            entity.IdConta = obj.IdConta;
            entity.MensagemValidacao = obj.MensagemValidacao;            
        }

        

        public void Atualizar(ContaCorrenteModel entity)
        {
            throw new System.NotImplementedException();
        }

        public ContaCorrenteModel ObterPorId(int id)
        {
            var obj = _contaCorrenteService.ObterPorId(id);

            return obj.MapTo<ContaCorrenteModel>();
        }

        public IEnumerable<ContaCorrenteModel> ObterTodos()
        {
            var obj = Mapper.Map<IEnumerable<ContaCorrente>, IEnumerable<ContaCorrenteModel>>(_contaCorrenteService.ObterTodos());

            return obj;
        }        
    }
}
