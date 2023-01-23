using System.ComponentModel.DataAnnotations;

namespace GeoService.Web.Data.Models
{
    public class Section
    {
        [Key]
        public Guid Id { get; set; }
        public string Symbol { get; set; }
        public string Description { get; set; }
        public virtual ICollection<PKDCode> PKDCodes { get; set; }
    }
}
