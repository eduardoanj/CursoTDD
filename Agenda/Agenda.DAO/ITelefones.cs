using Agenta.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.DAO
{
    public interface ITelefones
    {
        List<ITelefone> ObterTodosDoContato(Guid ContatoId);
    }
}
