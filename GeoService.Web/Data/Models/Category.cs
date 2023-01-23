using System.ComponentModel.DataAnnotations;

namespace GeoService.Web.Data.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Object> Objects { get; set; }
    }
}
