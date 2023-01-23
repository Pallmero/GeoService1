using GeoService.Web.Data.Models;

namespace GeoService.Web.Data.ViewModels
{
    public class PKDCodesViewModel
    {
        public List<PKDCode> PKDCodes { get; set; }
        public PaginationViewModel Pagination { get; set; }
    }
}
