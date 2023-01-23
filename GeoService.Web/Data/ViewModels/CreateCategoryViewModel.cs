using System.ComponentModel.DataAnnotations;

namespace GeoService.Web.Data.ViewModels
{
    public class CreateCategoryViewModel
    {
        [Required(ErrorMessage = "Nazwa jest wymagana")]
        public string Name { get; set; }
    }
}
