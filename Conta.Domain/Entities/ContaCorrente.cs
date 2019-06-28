using Conta.Domain.Entities;

namespace Conta.Domain.Entities
{
    public class ContaCorrente : EntityBase
    {
        public int IdConta { get; set; }
        public decimal ValorAtual { get; set; }

        public override void Validate()
        {
            LimparMensagemValidacao();

            if (ValorAtual < 0 )
                AdicionarMensagem("Não é possível cadastrar uma conta com valor negativo.");
        }
    }
}
