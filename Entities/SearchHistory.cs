using MassTransit;

namespace MITC_Smart_Solution.Entities
{
    public class SearchHistory(string searchText, string category, string jsonResult)
    {
        public Guid Id { get; set; } = NewId.Next().ToGuid();
        public required string SearchText { get; init; } = searchText;
        public required string Category { get; init; } = category;
        public required DateTime SearchDate { get; set; } = DateTime.UtcNow;
        public required string JsonResult { get; init; }  = jsonResult;
    }
}
