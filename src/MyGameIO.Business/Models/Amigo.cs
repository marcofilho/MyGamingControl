using System;
using System.Collections.Generic;

namespace MyGameIO.Business.Models
{
    public class Amigo : Entity
    {
        public string Nome { get; set; }
        public string Documento { get; set; }
        public DateTime? DataEmprestimo { get; set; }
        public DateTime? DataDevolucao { get; set; }
        public string Imagem { get; set; }
        public Endereco Endereco { get; set; }
        public IList<Jogo> Jogos { get; set; }
    }
}
