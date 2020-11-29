using Microsoft.EntityFrameworkCore;
using MyGameIO.Business.Interfaces;
using MyGameIO.Business.Models;
using MyGameIO.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGameIO.Data.Repositories
{
    public class JogoRepository : Repository<Jogo>, IJogoRepository
    {
        public JogoRepository(ProjectDbContext context) : base(context) { }

        public async Task<IEnumerable<Jogo>> FindAllJogosByAmigos()
        {
            return await context.Jogos.AsNoTracking()
                .Include(g => g.Amigo)
                .OrderBy(g => g.Nome)
                .ToListAsync();
        }

        public async Task<Jogo> FindJogoByAmigo(Guid id)
        {
            return await context.Jogos.AsNoTracking().
                Include(g => g.Amigo).
                FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Jogo>> FindJogosByAmigo(Guid amigoId)
        {
            return await FindAsync(g => g.Amigo.Id == amigoId);
        }
    }
}
