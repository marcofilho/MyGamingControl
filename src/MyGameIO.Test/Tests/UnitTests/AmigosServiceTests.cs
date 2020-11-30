using Moq;
using MyGameIO.Business.Interfaces;
using MyGameIO.Business.Services;
using Xunit;

namespace MyGameIO.Tests.UnitTests
{
    public class AmigosServiceTests
    {
        private GerarAmigoFake _gerarAmigoFake;

        public AmigosServiceTests()
        {
            _gerarAmigoFake = new GerarAmigoFake();
        }

        [Fact(DisplayName = "Adicionar Amigo com Sucesso")]
        [Trait("Categoria", "Amigo Service Mock Tests")]
        public async void AmigosService_Adicionar_DeveExecutarComSucesso()
        {
            var amigo = _gerarAmigoFake.Add();
            var amigoRepo = new Mock<IAmigoRepository>();
            var enderecoRepo = new Mock<IEnderecoRepository>();
            var notificadorRepo = new Mock<INotificador>();

            var amigoService = new AmigoService(amigoRepo.Object, enderecoRepo.Object, notificadorRepo.Object);

            // Act
            var retorno = await amigoService.Adicionar(amigo);

            // Assert
            Assert.True(retorno);
            amigoRepo.Verify(a => a.Adicionar(amigo), Times.Once);
        }

        [Fact(DisplayName = "Adicionar Amigo com Falha")]
        [Trait("Categoria", "Amigo Service Mock Tests")]

        public async void AmigosService_Adicionar_DeveFalhar()
        {
            var amigo = _gerarAmigoFake.AddSemEnderecoParaFalhar();
            var amigoRepo = new Mock<IAmigoRepository>();
            var enderecoRepo = new Mock<IEnderecoRepository>();
            var notificadorRepo = new Mock<INotificador>();

            var amigoService = new AmigoService(amigoRepo.Object, enderecoRepo.Object, notificadorRepo.Object);

            // Act
            var retorno = await amigoService.Adicionar(amigo);

            // Assert
            Assert.False(retorno);
            amigoRepo.Verify(a => a.Adicionar(amigo), Times.Never);
        }

    }
}
