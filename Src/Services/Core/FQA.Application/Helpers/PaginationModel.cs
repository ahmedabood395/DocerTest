namespace FAQ.Application.Helpers
{
    public class PaginationModel
    {
        public PaginationModel(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
