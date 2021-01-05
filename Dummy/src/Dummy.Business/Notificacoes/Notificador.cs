using System.Collections.Generic;
using System.Linq;
using Dummy.Business.Intefaces;

namespace Dummy.Business.Notificacoes
{
    public class Notificador : INotificador
    {
        private List<Notificacao> _notificacoes = new List<Notificacao>();

        public void Handle(Notificacao notificacao) => _notificacoes.Add(notificacao);

        public List<Notificacao> ObterNotificacoes() => _notificacoes;

        public bool TemNotificacao() => _notificacoes.Any();
    }
}