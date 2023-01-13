using FuncionariosCRUD.Models;
using Microsoft.EntityFrameworkCore;


namespace FuncionariosCRUD.Data
{
    public class FuncionariosContexto : DbContext
    {
        public FuncionariosContexto(DbContextOptions<FuncionariosContexto> options) :
           base(options)
        {

        }
        public DbSet<FuncionariosModel> Funcionarios { get; set; }
    }
}
