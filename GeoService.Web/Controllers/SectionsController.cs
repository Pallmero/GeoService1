using CsvHelper;
using GeoService.Web.Data;
using GeoService.Web.Data.DTOs;
using GeoService.Web.Data.Models;
using GeoService.Web.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace GeoService.Web.Controllers
{
    public class SectionsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SectionsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> ViewSections()
        {
            var sections = await _db.Sections.ToListAsync();

            var vm = new SectionsViewModel()
            {
                Sections = sections
            };

            return View(vm);
        }
    }
}
