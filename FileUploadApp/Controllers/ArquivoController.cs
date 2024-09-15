using FileUploadApp.Data;
using FileUploadApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FileUploadApp.Controllers
{
    public class ArquivoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArquivoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile arquivo)
        {
            if (arquivo != null && arquivo.Length > 0)
            {
                // Salvando o arquivo na pasta 'uploads' dentro do projeto.
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", arquivo.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await arquivo.CopyToAsync(stream);
                }

                // Salvando as informações no banco de dados.
                var novoArquivo = new Arquivo
                {
                    NomeArquivo = arquivo.FileName,
                    CaminhoArquivo = "/uploads/" + arquivo.FileName,
                    DataEnvio = DateTime.Now
                };

                _context.Arquivos.Add(novoArquivo);
                await _context.SaveChangesAsync();

                return RedirectToAction("Lista");

            }
            return View();
        }

        public IActionResult Lista()
        {
            var arquivos = _context.Arquivos.ToList();
            return View(arquivos);
        }
    }
}
