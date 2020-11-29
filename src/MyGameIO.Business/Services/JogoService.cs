using MyGameIO.Business.Interfaces;
using MyGameIO.Business.Models;
using MyGameIO.Business.Models.Validations;
using System;
using System.Threading.Tasks;

namespace MyGameIO.Business.Services
{
    public class JogoService : BaseService, IJogoService
    {
        private readonly IJogoRepository _jogoRepository;
        private readonly IAmigoRepository _amigoRepository;

        public JogoService(IJogoRepository produtoRepository,
                           IAmigoRepository amigoRepository,
                           INotificador notificador) : base(notificador)
        {
            _jogoRepository = produtoRepository;
            _amigoRepository = amigoRepository;
        }

        public async Task<bool> Adicionar(Jogo jogo)
        {
            if (!ExecutarValidacao(new JogoValidation(), jogo)) return false;

            await _jogoRepository.Adicionar(jogo);

            return true;
        }

        public async Task<bool> Atualizar(Jogo jogo)
        {
            if (!ExecutarValidacao(new JogoValidation(), jogo)) return false;

            await _jogoRepository.Atualizar(jogo);

            return true;
        }

        public async Task<bool> Remover(Guid id)
        {
            var jogo = await _jogoRepository.BuscarPorId(id);

            if (await _amigoRepository.FindJogoEmprestado(jogo))
            {
                Notificar("Esse jogo encontra-se emprestado!");
                return false;
            }

            await _jogoRepository.Remover(id);

            return true;
        }

        public async Task<bool> EmprestarJogo(Jogo jogo, Amigo amigo)
        {
            if (await _amigoRepository.FindJogoEmprestado(jogo))
            {
                Notificar("Esse jogo encontra-se emprestado!");
                return false;
            }
            if (amigo.Jogos != null && amigo.Jogos.Count > 0)
            {
                if (amigo.Jogos.Contains(jogo))
                {
                    Notificar("Esse jogo já encontra-se emprestado para " + amigo.Nome + "!");
                    return false;
                }
            }

            jogo.DataEmprestimo = DateTime.Now;
            jogo.Disponivel = false;
            jogo.AmigoId = amigo.Id;

            await _jogoRepository.Atualizar(jogo);

            return true;
        }

        public async Task<bool> DevolverJogo(Jogo jogo, Amigo amigo)
        {
            if (amigo.Jogos == null || amigo.Jogos.Count < 1)
            {
                Notificar("Esse amigo não possue jogos para serem devolvidos!");
                return false;
            }

            if (!await _amigoRepository.FindJogoEmprestado(jogo))
            {
                Notificar("Esse jogo não encontra-se emprestado!");
                return false;
            }

            if (!amigo.Jogos.Contains(jogo))
            {
                Notificar("Esse jogo não encontra-se emprestado para " + amigo.Nome + "!");
                return false;
            }

            jogo.DataEmprestimo = null;
            jogo.Disponivel = true;
            jogo.AmigoId = null;

            await _jogoRepository.Atualizar(jogo);

            return true;
        }

        public void Dispose()
        {
            _jogoRepository?.Dispose();
        }


    }
}