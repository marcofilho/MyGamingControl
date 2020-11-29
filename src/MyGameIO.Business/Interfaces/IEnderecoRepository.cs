using MyGameIO.Business.Models;
using System;
using System.Threading.Tasks;

namespace MyGameIO.Business.Interfaces
{
    public interface IEnderecoRepository : IRepository<Endereco>
    {
        Task<Endereco> FindEnderecoByAmigo(Guid amigoId);
    }
}
