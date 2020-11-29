using Microsoft.EntityFrameworkCore;
using MyGameIO.Business.Interfaces;
using MyGameIO.Business.Models;
using MyGameIO.Data.Context;
using System;
using System.Threading.Tasks;

namespace MyGameIO.Data.Repositories
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(ProjectDbContext context) : base(context) { }

        public async Task<Endereco> FindEnderecoByAmigo(Guid amigoId)
        {
            return await context.Enderecos.AsNoTracking()
                 .FirstOrDefaultAsync(e => e.AmigoId == amigoId);
        }
    }
}
