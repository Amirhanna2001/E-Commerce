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
    }
}
