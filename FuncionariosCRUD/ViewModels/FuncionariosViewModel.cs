using System.ComponentModel.DataAnnotations;

namespace FuncionariosCRUD.ViewModels
{
    public class FuncionariosViewModel : EditFotoViewModel
    {
        [Required]
        [Display(Name = "Nome Completo")]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [Required]
        [Display(Name = "Estado Civil")]
        public string EstadoCivil { get; set; }

        [Required]
        [Display(Name = "Nº da CTPS")]
        public int CTPS { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data da Contratação")]
        public DateTime DataContratacao { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Cargo")]
        public string Cargo { get; set; }

    }
}
