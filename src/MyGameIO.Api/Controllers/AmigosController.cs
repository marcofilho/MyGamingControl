using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyGameIO.App.ViewModels;
using MyGameIO.Business.Interfaces;
using MyGameIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyGameIO.Api.Controllers
{
    [Authorize]
    [Route("api/Amigos")]
    public class AmigosController : MainController
    {
        private readonly IAmigoRepository _amigoRepository;
        private readonly IMapper _mapper;
        private readonly IAmigoService _amigoService;

        public AmigosController(IAmigoRepository amigoRepository, IMapper mapper, IAmigoService amigoService, INotificador notificador) : base(notificador)
        {
            _amigoRepository = amigoRepository;
            _mapper = mapper;
            _amigoService = amigoService;
        }

        [HttpGet]
        public async Task<IEnumerable<AmigoViewModel>> BuscarTodosAmigos()
        {
            var amigos = _mapper.Map<IEnumerable<AmigoViewModel>>(await _amigoRepository.FindAmigosComEnderecoComJogos());

            foreach (var amigo in amigos)
            {
                if (amigo.Jogos != null)
                {
                    foreach (var jogo in amigo.Jogos)
                    {
                        jogo.EmprestadoAoAmigo = amigo.Nome;
                    }
                }
            }

            return amigos;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<AmigoViewModel>> BuscarPorId(Guid id)
        {
            var amigo = await FindAmigoGamesEndereco(id);

            if (amigo == null) return NotFound();

            return amigo;
        }

        [HttpPost]
        public async Task<ActionResult<AmigoViewModel>> AdicionarAmigo([FromBody] AmigoViewModel amigoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _amigoService.Adicionar(_mapper.Map<Amigo>(amigoViewModel));

            return Ok(amigoViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<AmigoViewModel>> AtualizarAmigo([FromRoute] Guid id, [FromBody] AmigoViewModel amigoViewModel)
        {
            if (id != amigoViewModel.Id)
            {
                NotifyError("O id informado no objeto não é o mesmo enviado na query da requisição!");
                return CustomResponse(amigoViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _amigoService.Atualizar(_mapper.Map<Amigo>(amigoViewModel));

            return CustomResponse(amigoViewModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<AmigoViewModel>> RemoverAmigo([FromRoute] Guid id)
        {
            var amigoViewModel = await FindAmigoById(id);

            if (amigoViewModel == null) return NotFound();

            await _amigoService.Remover(id);

            return CustomResponse(amigoViewModel);
        }

        public async Task<AmigoViewModel> FindAmigoGamesEndereco(Guid id)
        {
            return _mapper.Map<AmigoViewModel>(await _amigoRepository.FindAmigoGamesEndereco(id));
        }

        public async Task<AmigoViewModel> FindAmigoById(Guid id)
        {
            return _mapper.Map<AmigoViewModel>(await _amigoRepository.FindAmigoGamesEndereco(id));
        }

    }
}
