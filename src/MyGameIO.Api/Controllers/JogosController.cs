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
    [Route("api/Jogos")]
    public class JogosController : MainController
    {
        private readonly IJogoRepository _jogoRepository;
        private readonly IMapper _mapper;
        private readonly IJogoService _jogoService;
        private readonly IAmigoRepository _amigoRepository;
        private readonly IAmigoService _amigoService;

        public JogosController(IJogoRepository jogoRepository, IMapper mapper, IJogoService jogoService, IAmigoRepository amigoRepository, IAmigoService amigoService, INotificador notificador) : base(notificador)
        {
            _jogoRepository = jogoRepository;
            _mapper = mapper;
            _jogoService = jogoService;
            _amigoRepository = amigoRepository;
            _amigoService = amigoService;
        }

        [HttpGet]
        public async Task<IEnumerable<JogoViewModel>> BuscarTodosJogos()
        {
            var jogos = _mapper.Map<IEnumerable<JogoViewModel>>(await _jogoRepository.BuscarTodos());

            foreach (var jogo in jogos)
            {
                if (jogo.AmigoId != null && jogo.AmigoId != Guid.Empty)
                {
                    var amigo = await _amigoRepository.BuscarPorId(jogo.AmigoId.Value);
                    if (amigo != null) jogo.EmprestadoAoAmigo = amigo.Nome;
                }
            }

            return jogos;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<JogoViewModel>> BuscarPorId(Guid id)
        {
            var jogo = await FindJogoById(id);

            if (jogo == null) return NotFound();

            if (jogo.AmigoId != null || jogo.AmigoId != Guid.Empty)
            {
                var amigo = await _amigoRepository.BuscarPorId(jogo.AmigoId.Value);
                if (amigo != null) jogo.EmprestadoAoAmigo = amigo.Nome;
            }

            return jogo;
        }

        [HttpPost]
        public async Task<ActionResult<JogoViewModel>> AdicionarJogo([FromBody] JogoViewModel jogoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _jogoService.Adicionar(_mapper.Map<Jogo>(jogoViewModel));

            return Ok(jogoViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<JogoViewModel>> AtualizarJogo([FromRoute] Guid id, [FromBody] JogoViewModel jogoViewModel)
        {
            if (id != jogoViewModel.Id)
            {
                NotifyError("O id informado no objeto não é o mesmo enviado na query da requisição!");
                return CustomResponse(jogoViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(jogoViewModel);

            var jogo = await _jogoRepository.BuscarPorId(jogoViewModel.Id);

            if (jogo.AmigoId != null)
            {
                NotifyError("Não é possível editar um jogo emprestado.");
                return CustomResponse(jogoViewModel);
            }

            await _jogoService.Atualizar(_mapper.Map<Jogo>(jogoViewModel));

            return CustomResponse(jogoViewModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<JogoViewModel>> RemoverJogo([FromQuery] Guid id)
        {

            var jogoViewModel = await FindJogoById(id);

            if (jogoViewModel == null) return NotFound();

            await _jogoService.Remover(id);

            return CustomResponse(jogoViewModel);
        }

        [HttpPost, Route("EmprestarJogo/{jogoId:guid}/{amigoId:guid}")]
        public async Task<ActionResult> EmprestarJogo([FromRoute] Guid jogoId, [FromRoute] Guid amigoId)
        {
            if (amigoId == null || amigoId == Guid.Empty)
            {
                NotifyError("Forneça o id do jogo!");
                return CustomResponse();
            }

            if (jogoId == null || jogoId == Guid.Empty)
            {
                NotifyError("Forneça o id do amigo!");
                return CustomResponse();
            }

            var jogo = await _jogoRepository.BuscarPorId(jogoId);
            var amigo = await _amigoRepository.BuscarPorId(amigoId);

            if (jogo == null || amigo == null) return NotFound();

            var retorno = await _jogoService.EmprestarJogo(jogo, amigo);
            if (retorno) await _amigoService.EmprestarJogo(jogo, amigo);

            return CustomResponse();
        }

        [HttpPost, Route("DevolverJogo/{amigoId:guid}/{jogoId:guid}")]
        public async Task<ActionResult> DevolverJogo([FromRoute] Guid amigoId, [FromRoute] Guid jogoId)
        {
            if (amigoId == null || amigoId == Guid.Empty) return BadRequest("Forneça o id do Amigo.");
            if (jogoId == null || jogoId == Guid.Empty) return BadRequest("Forneça o id do Jogo");

            var jogo = await _jogoRepository.BuscarPorId(jogoId);
            var amigo = await _amigoRepository.BuscarPorId(amigoId);

            if (jogo == null || amigo == null) return NotFound();

            var retorno = await _jogoService.DevolverJogo(jogo, amigo);
            if (retorno) await _amigoService.DevolverJogo(jogo, amigo);

            return CustomResponse();

        }


        public async Task<JogoViewModel> FindJogoById(Guid id)
        {
            return _mapper.Map<JogoViewModel>(await _jogoRepository.BuscarPorId(id));
        }

    }
}
