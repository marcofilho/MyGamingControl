using MyGameIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyGameIO.Business.Interfaces
{
    public interface IJogoRepository : IRepository<Jogo>
    {
        Task<IEnumerable<Jogo>> FindJogosByAmigo(Guid amigoId);
        Task<IEnumerable<Jogo>> FindAllJogosByAmigos();
    }
}
