using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FuncionariosCRUD.ViewModels
{
    public class UploadFotoViewModel
    {
        [Display(Name = "Foto")]
        public IFormFile? ImagemFuncionario{ get; set; }
    }
}
