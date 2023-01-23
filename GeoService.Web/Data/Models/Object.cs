using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoService.Web.Data.Models
{
    public class Object
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid PKDCodeId { get; set; }
        public PKDCode PKDCode { get; set; }

        [NotMapped]
        public string CategoryName { get; set; }
    }
}
