using System.ComponentModel.DataAnnotations;

namespace GeoService.Web.Data.ViewModels
{
    public class UpdateCategoryViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Nazwa jest wymagana")]
        public string Name { get; set; }
    }
}
