using System.Collections.Generic;

namespace MyBucks.Mvc.Tools.Model
{
    public class PaginatedResponse
    {
        public List<object> Items { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
    }
}