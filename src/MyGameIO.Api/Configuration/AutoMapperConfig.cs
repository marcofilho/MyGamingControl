using AutoMapper;
using MyGameIO.App.ViewModels;
using MyGameIO.Business.Models;

namespace MyGameIO.App.Mappers
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Amigo, AmigoViewModel>().ReverseMap();
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
            CreateMap<Jogo, JogoViewModel>().ReverseMap();
        }

    }
}
