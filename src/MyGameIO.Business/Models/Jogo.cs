using MyGameIO.Business.Models;
using System;

namespace MyGameIO.Business.Models
{
    public class Jogo : Entity
    {
        public string Nome { get; set; }
        public DateTime? DataEmprestimo { get; set; }
        public bool Disponivel { get; set; }
        public string Imagem { get; set; }
        public TipoConsole TipoConsole { get; set; }
        public Nullable<Guid> AmigoId { get; set; }
        public Amigo Amigo { get; set; }
    }
}
