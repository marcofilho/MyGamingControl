using MyGameIO.Business.Models;
using MyGameIO.Business.Services;
using System;
using System.Threading.Tasks;

namespace MyGameIO.Business.Interfaces
{
    public interface IJogoService : IDisposable
    {
        Task<bool> Adicionar(Jogo jogo);
        Task<bool> Atualizar(Jogo jogo);
        Task<bool> Remover(Guid id);
        Task<bool> EmprestarJogo(Jogo jogo, Amigo amigo);
        Task<bool> DevolverJogo(Jogo jogo, Amigo amigo);

    }
}