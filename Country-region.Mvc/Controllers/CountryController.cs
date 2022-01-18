using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Yangi.DATA.Context;
using Yangi.DATA.Models;

namespace Country_region.Mvc.Controllers
{
    public class CountryController : Controller
    {
        private readonly AppDbContext _context;

        public CountryController(AppDbContext context)
        {
            _context = context ?? throw new System.ArgumentNullException(nameof(context));
        }

        public async Task<IActionResult> Index()
        {
            var countries = await _context.Countries.ToListAsync();
            return View(countries);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        public async Task<IActionResult> Add(Country country)
        {

            await _context.Countries.AddAsync(country);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(long Id)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(p => p.Id == Id);
            if (country == null)
                return Redirect("Error");
            return View(country);
        }
        public async Task<IActionResult> Update(Country country)
        {
            _context.Countries.Attach(country);
            _context.Entry(country).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(long Id)
        {
            var country = await _context.Countries.Include(p => p.Regions).FirstOrDefaultAsync(p => p.Id == Id);
            if (country == null)
                return Redirect("Error");
            return View(country);
        }
        public async Task<IActionResult> Delete(long Id)
        {
            var country = await _context.Countries.Include(p => p.Regions).FirstOrDefaultAsync(p => p.Id == Id);
            if (country == null)
                return Redirect("Error");
            return View(country);
        }
        public async Task<IActionResult> Remove(Country country)
        {
            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
