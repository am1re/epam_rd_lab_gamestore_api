namespace Application.Common.ViewModels
{
    public class PaginationInfo
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
        public int Count { get; set; }
        public int Total { get; set; }
    }
}