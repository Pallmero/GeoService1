using Microsoft.AspNetCore.Mvc.Rendering;

namespace GeoService.Web.Data.ViewModels
{
    public class CreateObjectViewModel
    {
        public string Name { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public Guid CategoryId { get; set; }
        public Guid PKDCodeId { get; set; }
        public IEnumerable<SelectListItem> PKDCodes { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
