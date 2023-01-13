using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FuncionariosCRUD.Data;
using FuncionariosCRUD.Models;
using FuncionariosCRUD.ViewModels;
using DocumentFormat.OpenXml.Drawing.Charts;
using ClosedXML.Excel;
using System.Data;

namespace FuncionariosCRUD.Controllers
{
    public class FuncionariosController : Controller
    {
        private readonly FuncionariosContexto _context;
        private readonly IWebHostEnvironment _environment;


        public FuncionariosController(FuncionariosContexto context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Funcionarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Funcionarios.ToListAsync());
        }

        // GET: Funcionarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Funcionarios == null)
            {
                return NotFound();
            }

            var funcionariosModel = await _context.Funcionarios
                .FirstOrDefaultAsync(m => m.Id == id);

            var funcionariosViewModel = new FuncionariosViewModel()
            {
                Id = funcionariosModel.Id,
                Nome = funcionariosModel.Nome,
                Endereco = funcionariosModel.Endereco,
                EstadoCivil = funcionariosModel.EstadoCivil,
                CTPS = funcionariosModel.CTPS,
                DataNascimento = funcionariosModel.DataNascimento,
                DataContratacao = funcionariosModel.DataContratacao,
                Cargo = funcionariosModel.Cargo,
                ExisteFoto = funcionariosModel.FotoFuncionario
            };


            if (funcionariosModel == null)
            {
                return NotFound();
            }

            return View(funcionariosModel);
        }

        // GET: Funcionarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Funcionarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Endereco,EstadoCivil,CTPS,DataNascimento,DataContratacao,Cargo,ImagemFuncionario")] FuncionariosViewModel model)
        {
            if (ModelState.IsValid)
            {
                string nomeUnicoArquivo = ProcessoUploadedArquivo(model);
                FuncionariosModel funcionarios = new()
                {
                    Nome = model.Nome,
                    Endereco = model.Endereco,
                    EstadoCivil = model.EstadoCivil,
                    CTPS = model.CTPS,
                    DataNascimento = model.DataNascimento,
                    DataContratacao = model.DataContratacao,
                    Cargo = model.Cargo,
                    FotoFuncionario = nomeUnicoArquivo
                };

                _context.Add(funcionarios);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Funcionarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Funcionarios == null)
            {
                return NotFound();
            }

            var funcionariosModel = await _context.Funcionarios.FindAsync(id);
            var funcionariosViewModel = new FuncionariosViewModel()
            {
                Id = funcionariosModel.Id,
                Nome = funcionariosModel.Nome,
                Endereco = funcionariosModel.Endereco,
                EstadoCivil = funcionariosModel.EstadoCivil,
                CTPS = funcionariosModel.CTPS,
                DataNascimento = funcionariosModel.DataNascimento,
                DataContratacao = funcionariosModel.DataContratacao,
                Cargo = funcionariosModel.Cargo,
                ExisteFoto = funcionariosModel.FotoFuncionario
            };

            //if (funcionariosViewModel.ImagemFuncionario == null)
            //    funcionariosViewModel.ImagemFuncionario = ProcessoUploadedArquivo(funcionariosModel);

            if (funcionariosModel == null)
            {
                return NotFound();
            }
            return View(funcionariosViewModel);
        }

        // POST: Funcionarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FuncionariosViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var funcionariosViewModel = await _context.Funcionarios.FindAsync(model.Id);
                funcionariosViewModel.Id = model.Id;
                funcionariosViewModel.Nome = model.Nome;
                funcionariosViewModel.Endereco = model.Endereco;
                funcionariosViewModel.EstadoCivil = model.EstadoCivil;
                funcionariosViewModel.CTPS = model.CTPS;
                funcionariosViewModel.DataNascimento = model.DataNascimento;
                funcionariosViewModel.DataContratacao = model.DataContratacao;
                funcionariosViewModel.Cargo = model.Cargo;


                if (model.ImagemFuncionario != null)
                {
                    if (model.ExisteFoto != null)
                    {
                        string filePath = Path.Combine(_environment.WebRootPath, "Uploads", model.ExisteFoto);
                        System.IO.File.Delete(filePath);
                    }

                    funcionariosViewModel.FotoFuncionario = ProcessoUploadedArquivo(model);
                }

                try
                {
                    _context.Update(funcionariosViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionariosModelExists(funcionariosViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Funcionarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Funcionarios == null)
            {
                return NotFound();
            }

            var funcionariosModel = await _context.Funcionarios
                .FirstOrDefaultAsync(m => m.Id == id);

            var funcionariosViewModel = new FuncionariosViewModel()
            {
                Id = funcionariosModel.Id,
                Nome = funcionariosModel.Nome,
                Endereco = funcionariosModel.Endereco,
                EstadoCivil = funcionariosModel.EstadoCivil,
                CTPS = funcionariosModel.CTPS,
                DataNascimento = funcionariosModel.DataNascimento,
                DataContratacao = funcionariosModel.DataContratacao,
                Cargo = funcionariosModel.Cargo,
                ExisteFoto = funcionariosModel.FotoFuncionario
            };

            if (funcionariosModel == null)
            {
                return NotFound();
            }

            return View(funcionariosViewModel);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Funcionarios == null)
            {
                return Problem("Entity set 'FuncionariosContexto.Funcionarios'  is null.");
            }
            var funcionariosModel = await _context.Funcionarios.FindAsync(id);

            string deleteFileFromFolder = Path.Combine(_environment.WebRootPath, "Uploads");
            var CurrentImage = Path.Combine(Directory.GetCurrentDirectory(), deleteFileFromFolder, funcionariosModel.FotoFuncionario);
            if (funcionariosModel != null)
            {
                _context.Funcionarios.Remove(funcionariosModel);
            }

            if (System.IO.File.Exists(CurrentImage))
            {
                System.IO.File.Delete(CurrentImage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("ExportToExcel")]
        public async Task<IActionResult> ExportToExcelAsync()
        {

            //using System.Data;  
            System.Data.DataTable dt = new System.Data.DataTable("Funcionarios");
            dt.Columns.AddRange(new DataColumn[8] { new DataColumn("Id"),
                                new DataColumn("Nome"), new DataColumn("Endereco"), new DataColumn("EstadoCivil"), new DataColumn("CTPS"),
                                new DataColumn("DataNascimento"), new DataColumn("DataContratacao"), new DataColumn("Cargo")});

            var FuncionariosLista = await _context.Funcionarios.ToListAsync();

            foreach (var func in FuncionariosLista)
            {
                dt.Rows.Add(func.Id, func.Nome, func.Endereco, func.EstadoCivil, func.CTPS, func.DataNascimento, func.DataContratacao, func.Cargo);
            }
            //using ClosedXML.Excel;  
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Relatório.xlsx");
                }
            }
        }

        private bool FuncionariosModelExists(int id)
        {
            return _context.Funcionarios.Any(e => e.Id == id);
        }

        private string ProcessoUploadedArquivo(FuncionariosViewModel model)
        {
            string nomeUnicoArquivo = null;
            string path = Path.Combine(_environment.WebRootPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (model.ImagemFuncionario != null)
            {
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "Uploads");
                nomeUnicoArquivo = Guid.NewGuid().ToString() + "_" + model.ImagemFuncionario.FileName;
                string filePath = Path.Combine(uploadsFolder, nomeUnicoArquivo);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImagemFuncionario.CopyTo(fileStream);
                }
            }

            return nomeUnicoArquivo;
        }

        // GET: FuncionariosRelatorio
        public async Task<IActionResult> Relatorio()
        {
            return View(await _context.Funcionarios.ToListAsync());
        }
    }
}
