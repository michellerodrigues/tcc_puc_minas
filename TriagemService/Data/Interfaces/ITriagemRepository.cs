using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TriagemService.Data.Models;

namespace TriagemService.Data.Interfaces
{
    public interface ITriagemRepository : IRepository<Triagem>
    {
        IEnumerable<Triagem> FindItensTriagem(Func<Triagem, bool> predicate);

        IEnumerable<Triagem> FindTriagemExpirada();

        IEnumerable<Triagem> FindTriagemStatus(string status);
    }

}