using GeoService.Web.Data;
using GeoService.Web.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Object = GeoService.Web.Data.Models.Object;

namespace GeoService.Web.Controllers
{
    [Route("objects")]
    public class ObjectsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ObjectsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> ViewObjects()
        {
            var objects = await _db.Objects.Include(a => a.Category).Include(a => a.PKDCode.Section).ToListAsync();

            var vm = new ObjectsViewModel()
            {
                Objects = objects
            };

            return View(vm);
        }

        [HttpGet]
        [Route("create")]
        public async Task<IActionResult> CreateObject()
        {
            var vm = new CreateObjectViewModel();

            vm.Categories = await GetCategories();
            vm.PKDCodes = await GetPKDCodes();

            return View(vm);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateObject(CreateObjectViewModel vm)
        {
            var model = new Object()
            {
                Name = vm.Name,
                Latitude = (double)vm.Latitude,
                Longitude = (double)vm.Longitude,
                CategoryId = vm.CategoryId,
                PKDCodeId = vm.PKDCodeId
            };

            await _db.Objects.AddAsync(model);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(ViewObjects));
        }

        [HttpGet]
        [Route("update")]
        public async Task<IActionResult> UpdateObject(Guid id)
        {
            var model = await _db.Objects.Where(a => a.Id == id).FirstOrDefaultAsync();

            var vm = new UpdateObjectViewModel()
            {
                Id = model.Id,
                Name = model.Name,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                CategoryId = model.CategoryId,
                PKDCodeId = model.PKDCodeId
            };

            vm.Categories = await GetCategories();
            vm.PKDCodes = await GetPKDCodes();

            return View(nameof(UpdateObject), vm);
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateObject(UpdateObjectViewModel vm)
        {
            var model = await _db.Objects.Where(a => a.Id == vm.Id).FirstOrDefaultAsync();

            model.Name = vm.Name;
            model.Latitude = vm.Latitude;
            model.Longitude = vm.Longitude;
            model.CategoryId = vm.CategoryId;
            model.PKDCodeId = vm.PKDCodeId;

            _db.Objects.Update(model);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(ViewObjects));
        }

        [Route("delete")]
        public async Task<IActionResult> DeleteObject(Guid id)
        {
            var model = await _db.Objects.Where(a => a.Id == id).FirstOrDefaultAsync();

            _db.Objects.Remove(model);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(ViewObjects));
        }

        private async Task<IEnumerable<SelectListItem>> GetCategories()
        {
            var roles = await _db.Categories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToListAsync();

            return new SelectList(roles, "Value", "Text");
        }

        private async Task<IEnumerable<SelectListItem>> GetPKDCodes()
        {
            var roles = await _db.PKDCodes.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Description
            }).ToListAsync();

            return new SelectList(roles, "Value", "Text");
        }
    }
}
