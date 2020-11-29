using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyGameIO.App.ViewModels
{
    public class JogoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 10)]
        public string Nome { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? DataEmprestimo { get; set; }

        [DisplayName("Disponível?")]
        public bool Disponivel { get; set; }

        public string Imagem { get; set; }

        [DisplayName("Foto")]
        public IFormFile ImagemUpload { get; set; }

        public int TipoConsole { get; set; }

        public Nullable<Guid> AmigoId { get; set; }

        [ScaffoldColumn(false)]
        public string EmprestadoAoAmigo { get; set; }
    }
}
