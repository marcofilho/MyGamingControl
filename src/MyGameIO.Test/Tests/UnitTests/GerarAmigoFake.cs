using MyGameIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyGameIO.Tests.UnitTests
{
    public class GerarAmigoFake
    {
        private readonly List<Amigo> _amigos;

        public GerarAmigoFake()
        {
            _amigos = new List<Amigo>()
            {
                new Amigo() {
                    Id = new Guid(),
                    Nome = "Amigo 1",
                    Documento = "11111111111"
                },
                new Amigo() {
                    Id = new Guid(),
                    Nome = "Amigo 2",
                    Documento = "22222222222"
                },
                new Amigo() {
                    Id = new Guid(),
                    Nome = "Amigo 3",
                    Documento = "33333333333"
                },
                new Amigo() {
                    Id = new Guid(),
                    Nome = "Amigo 4",
                    Documento = "44444444444"
                },
                new Amigo() {
                    Id = new Guid(),
                    Nome = "Amigo 5",
                    Documento = "55555555555"
                }
            };

            foreach (var amigo in _amigos)
            {
                amigo.Endereco = GerarEnderecoFake(amigo);
            }
        }

        public Endereco GerarEnderecoFake(Amigo amigo)
        {
            return new Endereco
            {
                Id = new Guid(),
                Amigo = amigo,
                AmigoId = amigo.Id,
                Bairro = "Bairro",
                Cep = "12345678",
                Cidade = "Cidade",
                Estado = "Estado",
                Complemento = "Complemento",
                Logradouro = "Logradouro",
                Numero = "123"
            };
        }


        public IEnumerable<Amigo> GetAllItems()
        {
            return _amigos;
        }

        public Amigo Add()
        {
            var amigo = new Amigo()
            {
                Id = new Guid(),
                Nome = "Novo amigo",
                Documento = "12345678910"
            };
            amigo.Endereco = GerarEnderecoFake(amigo);
            _amigos.Add(amigo);
            return amigo;
        }

        public Amigo AddSemEnderecoParaFalhar()
        {
            var amigo = new Amigo()
            {
                Id = new Guid(),
                Nome = "Novo amigo",
                Documento = "11111111"
            };
            _amigos.Add(amigo);
            return amigo;
        }

        public Amigo GetById(Guid id)
        {
            return _amigos.Where(a => a.Id == id)
                .FirstOrDefault();
        }

        public void Remove(Guid id)
        {
            var item = _amigos.First(a => a.Id == id);
            _amigos.Remove(item);
        }
    }
}
