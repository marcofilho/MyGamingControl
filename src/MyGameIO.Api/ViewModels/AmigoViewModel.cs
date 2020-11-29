using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyGameIO.App.ViewModels
{
    public class AmigoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(11, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 11)]
        public string Documento { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? DataEmprestimo { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? DataDevolucao { get; set; }

        public string Imagem { get; set; }

        [DisplayName("Foto")]
        public IFormFile ImagemUpload { get; set; }

        /*Relacionamento com endereço */
        public EnderecoViewModel Endereco { get; set; }

        /* Relacionamento com games */
        public IList<JogoViewModel> Jogos { get; set; }
    }
}
