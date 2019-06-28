using Conta.Domain.Enumerators;
using Conta.Domain.Entities;
using System;

namespace Conta.Domain.Entities
{
    public class Movimentacao : EntityBase
    {
        public int IdMovimentacao { get; set; }
        public int ContaId { get; set; }
        public DateTime DataMovimentacao { get; set; }
        public TipoOperacaoEnum TipoOperacao { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorAtual { get; set; }



        public override void Validate()
        {
            LimparMensagemValidacao();

            if (TipoOperacao == TipoOperacaoEnum.Debito && Valor > ValorAtual)
                AdicionarMensagem("Não é possível cadastrar uma conta com valor negativo.");


        }
    }
}
