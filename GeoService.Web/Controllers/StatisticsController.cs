using GeoService.Web.Data;
using GeoService.Web.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeoService.Web.Controllers
{
    [Route("statistics")]
    public class StatisticsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public StatisticsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> ViewStatistics()
        {
            var vm = new StatisticsViewModel()
            {
                Labels = new List<string>(),
                Series = new List<int>(),
                Colours = new List<string>(),
                Sections = new List<string>(),
                Data = new List<int>()
            };

            var objects = await _db.Objects.Include(a => a.Category).Include(a => a.PKDCode).ThenInclude(a => a.Section).ToListAsync();
            var categories = await _db.Categories.ToListAsync();
            var sections = await _db.Sections.ToListAsync();

            foreach (var category in categories)
            {
                vm.Labels.Add(category.Name);
                var count = objects.Where(a => a.Category.Name.Equals(category.Name)).Count();
                vm.Series.Add(count);
                count = 0;
                vm.Colours.Add(HexColourGenerator());
            }

            foreach (var section in sections)
            {
                vm.Sections.Add(section.Symbol);
                var count = objects.Where(a => a.PKDCode.Section.Symbol.Equals(section.Symbol)).Count();
                vm.Data.Add(count);
                count = 0;
            }

            return View(vm);
        }

        public class StatisticsViewModel
        {
            public List<string> Labels { get; set; }
            public List<int> Series { get; set; }
            public List<string> Colours { get; set; }
            public List<int> Data { get; set; }
            public List<string> Sections { get; set; }
        }

        private string HexColourGenerator()
        {
            var random = new Random();
            var color = String.Format("#{0:X6}", random.Next(0x1000000));

            return color;
        }
    }
}
