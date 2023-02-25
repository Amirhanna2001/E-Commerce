using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task< IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if(!ModelState.IsValid)
                return View(category);

            if(category == null)
                return View("Error");

            if(await _context.Categories.AnyAsync(c => c.Name == category.Name))
            {
                ModelState.AddModelError("Name", "This Category Is Exists");
                return View(category);
            }
            
            _context.Categories.Add(category);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            Category cat = await _context.Categories.FindAsync(id);
            if (cat == null)
                return NotFound();

            _context.Categories.Remove(cat);
            _context.SaveChanges();//To Delete It From Database
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edite(int id)
        {
            Category category =await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();


            return View(category);//To Go To Details.cshtml View
        }
        [HttpPost]
        public async Task<IActionResult> Edite(Category category)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Index));
            if (category == null)
                return BadRequest();

            if(await _context.Categories.AnyAsync(c=>c.Name == category.Name))
            {
                ModelState.AddModelError("Name", "العرض موجود بالفعل");
                return View(category);
            }

             _context.Categories.Update(category);
             _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}
