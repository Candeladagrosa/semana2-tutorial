using Microsoft.AspNetCore.Mvc;
using semana2_tutorial.Models;
using semana2_tutorial.Data;
using Microsoft.EntityFrameworkCore;

namespace semana2_tutorial.Controllers
{
    public class MovieController : Controller
    {
        // Context
        private readonly ApplicationDbContext _context;

        public MovieController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Movie
        public async Task<IActionResult> Index()
        {
            var listMovies = await _context.Movies.ToListAsync();
            if (listMovies == null || !listMovies.Any())
            {
                ViewBag.MensajeError = "No hay películas disponibles en la base de datos.";
                return View();
            }

            ViewBag.MensajeExito = TempData["MensajeExito"];
            ViewBag.MensajeError = TempData["MensajeError"];
            return View(listMovies);
        }


        // GET: Movie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movie/Nueva
        public async Task<IActionResult> Nueva()
        {
            ViewBag.MensajeError = TempData["MensajeError"];
            ViewBag.MensajeExito = TempData["MensajeExito"];
            return View();
        }

        [HttpPost]
        public ActionResult Create(Movie movieInput)
        {
            if (!ModelState.IsValid)
            {
                TempData["MensajeError"] = "Por favor, completa todos los campos correctamente.";
                return View("Nueva", movieInput);
            }

            // Acá guardamos la movie en la base de datos
            var movie = new Movie
            {
                Genre = movieInput.Genre,
                Price = movieInput.Price,
                ReleaseDate = movieInput.ReleaseDate,
                Title = movieInput.Title
            };

            // El ID no lo mapeo porque se genera automáticamente en la base de datos
            _context.Movies.Add(movie);
            _context.SaveChanges();

            TempData["MensajeExito"] = "Película creada correctamente.";
            return View("Nueva");
        }

        // GET: Movie/Edit/5 - Muestra el formulario
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movie/Edit/5 - Procesa el formulario
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(movie);
                await _context.SaveChangesAsync();
                TempData["MensajeExito"] = "Película actualizada correctamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // DELETE: Movie/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            TempData["MensajeExito"] = "Película eliminada correctamente.";
            return RedirectToAction(nameof(Index));
        }
    }
}
