using Conta.Application.Enumerators;
using Conta.Application.ViewModels;
using Conta.Domain.Entities;
using System;

namespace Conta.Application.Mappings
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<ContaCorrente, ContaCorrenteModel>();

            CreateMap<Movimentacao, MovimentacaoModel>();
        }
    }
}
