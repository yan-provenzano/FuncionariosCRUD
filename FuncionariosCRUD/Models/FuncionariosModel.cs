using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FuncionariosCRUD.Models
{
    public class FuncionariosModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nome Completo")]
        public string Nome { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Estado Civil")]
        public string EstadoCivil { get; set; }

        [Required]
        [StringLength(100)]
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

        [Required]
        [Display(Name = "Foto")]
        public string? FotoFuncionario { get; set; }

        //public List<SelectListItem>? ListaEstadoCivil { set; get; }
    }
}
