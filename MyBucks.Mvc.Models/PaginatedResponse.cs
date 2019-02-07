using System.Collections.Generic;

namespace MyBucks.Mvc.Models
{
    public class PaginatedResponse
    {
        public object Items { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
    }

    public class PaginatedResponse<TListType> : PaginatedResponse
    {
        public new List<TListType> Items { get; set; }
    }
}