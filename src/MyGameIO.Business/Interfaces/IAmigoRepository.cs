using MyGameIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyGameIO.Business.Interfaces
{
    public interface IAmigoRepository : IRepository<Amigo>
    {
        Task<Amigo> FindAmigoEndereco(Guid id);
        Task<Amigo> FindAmigoGamesEndereco(Guid id);
        Task<bool> FindJogoEmprestado(Jogo jogo);
        Task<IList<Amigo>> FindAmigosComEnderecoComJogos();

    }
}
