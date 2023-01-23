using Microsoft.AspNetCore.Mvc.Rendering;
using Object = GeoService.Web.Data.Models.Object;

namespace GeoService.Web.Data.ViewModels
{
    public class MapViewModel
    {
        public string Name { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public Guid CategoryId { get; set; }
        public Guid PKDCodeId { get; set; }
        public Guid? CategoryFilterId { get; set; }
        public List<Object> Objects { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<SelectListItem> PKDCodes { get; set; }
    }
}
