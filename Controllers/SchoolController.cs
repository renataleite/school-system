using Edumin.Context;
using Edumin.Models;
using Microsoft.AspNetCore.Mvc;

namespace Edumin.Controllers
{
	public class SchoolController : Controller
	{
		private readonly SchoolContext _context;
		private readonly IWebHostEnvironment _hostingEnvironment;

		public SchoolController(SchoolContext context, IWebHostEnvironment hostingEnvironment)
		{
			_context = context;
			_hostingEnvironment = hostingEnvironment;
		}

		// GET: School/Add
		public ActionResult AddSchools()
		{
			return View();
		}

		// POST: School/Add
		[HttpPost]
		public IActionResult AddSchools(School school, IFormFile logoFile)
		{
				if (logoFile != null && logoFile.Length > 0)
				{
					var fileName = Path.GetFileName(logoFile.FileName);
					var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads", "Logos", fileName);

					using (var fileStream = new FileStream(filePath, FileMode.Create))
					{
						logoFile.CopyTo(fileStream);
					}

					school.LogoPath = "~/Uploads/Logos/" + fileName; // Save the relative path
				}

				_context.Schools.Add(school);
				_context.SaveChanges();
				return RedirectToAction("AllSchools", "School");

			//return View(school);
		}

		// GET: School
		public ActionResult AllSchools()
		{
			var schools = _context.Schools.ToList();
            return View(schools);
        }

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_context.Dispose();
			}
			base.Dispose(disposing);
		}

        // GET: School/EditSchool/5
        public ActionResult EditSchools(int id)
        {
            var school = _context.Schools.FirstOrDefault(s => s.SchoolId == id);
            if (school == null)
            {
                return NotFound(); // Retorna um erro 404 se a escola não for encontrada
            }
            return View(school); // Passa o objeto school para a View
        }

        // POST: School/EditSchool/5
        [HttpPost]
        public IActionResult EditSchools(int id, School school, IFormFile logoFile)
        {
            var schoolToUpdate = _context.Schools.FirstOrDefault(s => s.SchoolId == id);
            if (schoolToUpdate == null)
            {
                return NotFound(); // Verifica se a escola existe
            }

            if (logoFile != null && logoFile.Length > 0)
            {
                var fileName = Path.GetFileName(logoFile.FileName);
                var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads", "Logos", fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    logoFile.CopyTo(fileStream);
                }
                schoolToUpdate.LogoPath = "~/Uploads/Logos/" + fileName; // Atualiza o caminho do logo
            }

            schoolToUpdate.Name = school.Name; // Atualiza os outros campos da escola
                                               // Atualize aqui outros campos conforme necessário

            _context.SaveChanges(); // Salva as alterações no banco de dados
            return RedirectToAction("AllSchools"); // Redireciona para a lista de escolas
        }

        // POST: School/DeleteConfirmed/5
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var school = _context.Schools.Find(id);
            if (school != null)
            {
                _context.Schools.Remove(school);
                _context.SaveChanges();
                return Json(new { success = true, message = "Escola excluída com sucesso." });
            }
            else
            {
                return Json(new { success = false, message = "Escola não encontrada." });
            }
        }



    }
}
