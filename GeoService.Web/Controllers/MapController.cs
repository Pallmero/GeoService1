using CsvHelper;
using GeoService.Web.Data;
using GeoService.Web.Data.DTOs;
using GeoService.Web.Data.Models;
using GeoService.Web.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Object = GeoService.Web.Data.Models.Object;

namespace GeoService.Web.Controllers
{
    public class MapController : Controller
    {
        private readonly ApplicationDbContext _db;

        public MapController(ApplicationDbContext db)
        {
            _db = db;
        }

        [ResponseCache(NoStore = true, Duration = 0)]
        public async Task<IActionResult> ViewMap(MapViewModel viewModel)
        {
            var query = _db.Objects.AsQueryable();

            if (viewModel.CategoryFilterId is null || viewModel.CategoryFilterId == Guid.Empty)
                query = query.Include(a => a.Category);
            else
                query = query.Include(a => a.Category).Where(a => a.CategoryId == viewModel.CategoryFilterId);

            var objects = await query.ToListAsync();

            var vm = new MapViewModel()
            {
                Objects = objects
            };

            foreach (var objectItem in vm.Objects)
            {
                objectItem.CategoryName = objectItem.Category.Name;
                objectItem.Category = null;
            }

            vm.Categories = await GetCategories();
            vm.PKDCodes = await GetPKDCodes();

            return View(vm);
        }

        [Route("create-from-map-view")]
        public async Task<IActionResult> CreateObjectFromMapView(MapViewModel vm)
        {
            if (vm?.PKDCodeId == Guid.Empty && vm?.CategoryId == Guid.Empty && string.IsNullOrWhiteSpace(vm?.Name))
            {
                var model = await _db.Objects.Where(a => a.Longitude == vm.Longitude && a.Latitude == vm.Latitude).FirstOrDefaultAsync();
                _db.Objects.Remove(model);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(ViewMap));
            }
            else
            {
                var model = new Object()
                {
                    Name = vm.Name,
                    Latitude = (double)vm.Latitude,
                    Longitude = (double)vm.Longitude,
                    CategoryId = vm.CategoryId,
                    PKDCodeId = vm.PKDCodeId
                };

                await _db.AddAsync(model);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(ViewMap));
            }
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

        public async Task<IActionResult> SeedData()
        {
            if (!await _db.Sections.AsNoTracking().AnyAsync())
            {
                using var sectionsReader = new StreamReader("wwwroot\\pkd\\sekcje-pkd.csv");
                using var sectionsCsv = new CsvReader(sectionsReader, CultureInfo.InvariantCulture);
                var sectionsRecords = sectionsCsv.GetRecords<SectionDto>().ToList();

                foreach (var record in sectionsRecords)
                {
                    var section = new Section()
                    {
                        Symbol = record.Symbol,
                        Description = record.Description
                    };

                    await _db.Sections.AddAsync(section);
                    await _db.SaveChangesAsync();
                }
            }

            if (!await _db.PKDCodes.AsNoTracking().AnyAsync())
            {
                using var codesReader = new StreamReader("wwwroot\\pkd\\kody-pkd.csv");
                using var codesCsv = new CsvReader(codesReader, CultureInfo.InvariantCulture);
                var codesRecords = codesCsv.GetRecords<PKDCodeDto>().ToList();

                foreach (var record in codesRecords)
                {
                    var pkdCode = new PKDCode()
                    {
                        Department = record.Department,
                        Group = record.Group,
                        Class = record.Class,
                        PKDSymbol = record.PKDSymbol,
                        Description = record.Description,
                        SectionId = await _db.Sections.Where(a => a.Symbol.Equals(record.Section)).Select(a => a.Id).FirstOrDefaultAsync()
                    };

                    await _db.AddAsync(pkdCode);
                    await _db.SaveChangesAsync();
                }
            }

            return RedirectToAction(nameof(ViewMap));
        }
    }
}