using GeoService.Web.Data;
using GeoService.Web.Data.Models;
using GeoService.Web.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeoService.Web.Controllers
{
    [Route("categories")]
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoriesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> ViewCategories()
        {
            var categories = await _db.Categories.ToListAsync();

            var vm = new CategoriesViewModel()
            {
                Categories = categories
            };

            return View(vm);
        }

        [HttpGet]
        [Route("create")]
        public IActionResult CreateCategory()
        {
            var vm = new CreateCategoryViewModel();

            return View(vm);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateCategory(CreateCategoryViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(nameof(CreateCategory), vm);

            var model = new Category()
            {
                Name = vm.Name
            };

            await _db.Categories.AddAsync(model);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(ViewCategories));
        }

        [HttpGet]
        [Route("update")]
        public async Task<IActionResult> UpdateCategory(Guid id)
        {
            var model = await _db.Categories.Where(a => a.Id == id).FirstOrDefaultAsync();

            var vm = new UpdateCategoryViewModel()
            {
                Id = model.Id,
                Name = model.Name
            };

            return View(nameof(UpdateCategory), vm);
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(nameof(UpdateCategory), vm);

            var model = await _db.Categories.Where(a => a.Id == vm.Id).FirstOrDefaultAsync();

            model.Name = vm.Name;

            _db.Categories.Update(model);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(ViewCategories));
        }

        [Route("delete")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var model = await _db.Categories.Where(a => a.Id == id).Include(a => a.Objects).FirstOrDefaultAsync();

            if (model?.Objects is not null && model?.Objects.Count > 0)
            {
                ModelState.AddModelError("Name", "Nie można usunąć kategorii, ponieważ zostały do niej przypisane obiekty");

                var vm = new UpdateCategoryViewModel()
                {
                    Id = model.Id,
                    Name = model.Name
                };

                return View(nameof(UpdateCategory), vm);
            }

            _db.Categories.Remove(model);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(ViewCategories));
        }
    }
}
