using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Conta.Application.ViewModels
{
  public  class ContaCorrenteModel
    {
        [Display(Name = "Conta")]
        public int IdConta { get; set; }

        [Display(Name = "Valor Atual")]
        public decimal ValorAtual { get; set; }

        [Display(Name = "Cotação do Dolar")]
        public decimal CotacaoDolar { get; set; }

        [Display(Name = "Valor atual em Dolar")]
        public decimal ValorAtualDolar { get; set; }

        public List<string> MensagemValidacao { get; set; }
    }
}
