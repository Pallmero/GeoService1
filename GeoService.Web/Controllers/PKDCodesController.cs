using GeoService.Web.Data;
using GeoService.Web.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeoService.Web.Controllers
{
    public class PKDCodesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PKDCodesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> ViewPKDCodes()
        {
            var pkdCodes = await _db.PKDCodes.Include(a => a.Section).ToListAsync();

            var vm = new PKDCodesViewModel()
            {
                PKDCodes = pkdCodes,
                Pagination = new PaginationViewModel()
            };

            return View(vm);
        }
    }
}
