using MyGameIO.Business.Models;
using System;
using System.Threading.Tasks;

namespace MyGameIO.Business.Interfaces
{
    public interface IAmigoService : IDisposable
    {
        Task<bool> Adicionar(Amigo amigo);
        Task<bool> Atualizar(Amigo amigo);
        Task<bool> Remover(Guid id);
        Task<bool> EmprestarJogo(Jogo jogo, Amigo amigo);
        Task<bool> DevolverJogo(Jogo jogo, Amigo amigo);
    }
}