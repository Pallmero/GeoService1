namespace GeoService.Web.Data.Models
{
    public class PKDCode
    {
        public Guid Id { get; set; }
        public Guid SectionId { get; set; }
        public Section Section { get; set; }
        public string Department { get; set; }
        public string Group { get; set; }
        public string Class { get; set; }
        public string PKDSymbol { get; set; }
        public string Description { get; set; }
    }
}
