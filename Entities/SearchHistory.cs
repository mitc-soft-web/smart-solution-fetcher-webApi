using MassTransit;

namespace MITC_Smart_Solution.Entities
{
    public class SearchHistory(string searchText, string category, string jsonResult)
    {
        public Guid Id { get; set; } = NewId.Next().ToGuid();
        public string SearchText { get; init; } = searchText;
        public string Category { get; init; } = category;
        public DateTime SearchDate { get; set; } = DateTime.UtcNow;
        public  string JsonResult { get; init; }  = jsonResult;
    }
}
