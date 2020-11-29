using System;

namespace MyGameIO.Business.Models
{
    public class Endereco : Entity
    {
        public Guid AmigoId { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        /*Relacionamento com Amigo */
        public Amigo Amigo { get; set; }
    }
}
