using Conta.Application.Enumerators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Conta.Application.ViewModels
{
    public class MovimentacaoModel
    {
        public int IdMovimentacao { get; set; }

        [Display(Name = "Conta")]
        public int ContaId { get; set; }

        [Display(Name = "Data de Movimentação")]
        public DateTime DataMovimentacao { get; set; }

        [Display(Name = "Tipo de Operação")]
        public TipoOperacaoEnum TipoOperacao { get; set; }
        public decimal Valor { get; set; }

        [Display(Name = "Valor Atual")]
        public decimal ValorAtual { get; set; }

        public List<string> MensagemValidacao { get; set; }
    }
}
