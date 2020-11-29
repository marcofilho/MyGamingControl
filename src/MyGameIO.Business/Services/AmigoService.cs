using MyGameIO.Business.Models.Validations;
using MyGameIO.Business.Interfaces;
using MyGameIO.Business.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MyGameIO.Business.Services
{
    public class AmigoService : BaseService, IAmigoService
    {
        private readonly IAmigoRepository _amigoRepository;
        private readonly IEnderecoRepository _enderecoRepository;

        public AmigoService(IAmigoRepository amigoRepository,
                                 IEnderecoRepository enderecoRepository,
                                 INotificador notificador) : base(notificador)
        {
            _amigoRepository = amigoRepository;
            _enderecoRepository = enderecoRepository;
        }

        public async Task<bool> Adicionar(Amigo amigo)
        {
            if (!ExecutarValidacao(new AmigoValidation(), amigo)
                || !ExecutarValidacao(new EnderecoValidation(), amigo.Endereco)) return false;

            if (_amigoRepository.FindAsync(f => f.Documento == amigo.Documento).Result.Any())
            {
                Notificar("Já existe um amigo com este documento infor mado.");
                return false;
            }

            await _amigoRepository.Adicionar(amigo);
            return true;
        }

        public async Task<bool> Atualizar(Amigo amigo)
        {
            if (!ExecutarValidacao(new AmigoValidation(), amigo)) return false;

            if (_amigoRepository.FindAsync(f => f.Documento == amigo.Documento && f.Id != amigo.Id).Result.Any())
            {
                Notificar("Já existe um amigo com este documento informado.");
                return false;
            }

            await _amigoRepository.Atualizar(amigo);
            return true;
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            if (!ExecutarValidacao(new EnderecoValidation(), endereco)) return;

            await _enderecoRepository.Atualizar(endereco);
        }

        public async Task<bool> Remover(Guid id)
        {
            if (_amigoRepository.FindAmigoGamesEndereco(id).Result.Jogos.Any())
            {
                Notificar("O amigo possui jogos emprestados!");
                return false;
            }

            var endereco = await _enderecoRepository.FindEnderecoByAmigo(id);

            if (endereco != null)
            {
                await _enderecoRepository.Remover(endereco.Id);
            }

            await _amigoRepository.Remover(id);
            return true;
        }

        public void Dispose()
        {
            _amigoRepository?.Dispose();
            _enderecoRepository?.Dispose();
        }

        public async Task<bool> EmprestarJogo(Jogo jogo, Amigo amigo)
        {
            amigo.DataEmprestimo = DateTime.Now;
            amigo.Jogos = new List<Jogo>();
            amigo.Jogos.Add(jogo);

            await _amigoRepository.Atualizar(amigo);

            return true;
        }

        public async Task<bool> DevolverJogo(Jogo jogo, Amigo amigo)
        {

            amigo.DataEmprestimo = null;
            amigo.DataDevolucao = DateTime.Now;
            amigo.Jogos.Remove(jogo);

            await _amigoRepository.Atualizar(amigo);

            return true;
        }
    }
}