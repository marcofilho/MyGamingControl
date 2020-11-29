using Microsoft.EntityFrameworkCore;
using MyGameIO.Business.Interfaces;
using MyGameIO.Business.Models;
using MyGameIO.Data.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyGameIO.Data.Repositories
{
    public class AmigoRepository : Repository<Amigo>, IAmigoRepository
    {
        public AmigoRepository(ProjectDbContext context) : base(context) { }

        public async Task<Amigo> FindAmigoEndereco(Guid id)
        {
            return await context.Amigos.AsNoTracking()
                .Include(a => a.Endereco)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Amigo> FindAmigoGamesEndereco(Guid id)
        {
            return await context.Amigos.AsNoTracking()
                  .Include(g => g.Jogos)
                  .Include(e => e.Endereco)
                  .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IList<Amigo>> FindAmigosComEnderecoComJogos()
        {
            return await context.Amigos.AsNoTracking()
              .Include(g => g.Jogos)
              .Include(e => e.Endereco).ToListAsync();
        }

        public async Task<bool> FindJogoEmprestado(Jogo jogo)
        {
            var emprestado = await context.Amigos.AsNoTracking().FirstOrDefaultAsync(j => j.Jogos.Contains(jogo));
            return emprestado != null ? true : false;
        }

    }
}
