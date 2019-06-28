using System.Collections.Generic;
using System.Linq;

namespace Conta.Domain.Entities
{
    public abstract class EntityBase
    {
        private List<string> _mensagemValidacao { get; set; }
        public List<string> MensagemValidacao
        {
            get { return _mensagemValidacao ?? (_mensagemValidacao = new List<string>()); }
        }

        protected void LimparMensagemValidacao()
        {
            MensagemValidacao.Clear();
        }

        public void AdicionarMensagem(string mensagem)
        {
            MensagemValidacao.Add(mensagem);
        }

        public abstract void Validate();

        public bool EhValido
        {
            get { return !MensagemValidacao.Any(); }
        }
    }
}
