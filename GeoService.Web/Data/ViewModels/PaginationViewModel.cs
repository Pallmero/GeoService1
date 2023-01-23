namespace GeoService.Web.Data.ViewModels
{
    public class PaginationViewModel
    {
        public PaginationViewModel()
        {
            CurrentPage = 1;
            PageSize = 10;
        }

        public PaginationViewModel(int currenPage, int pageSize)
        {
            CurrentPage = currenPage;
            PageSize = pageSize;
        }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
