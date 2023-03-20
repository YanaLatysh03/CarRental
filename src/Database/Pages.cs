using System;

namespace Database
{
    public class Pages
    {
        public int TotalItems { get; set; }
        
        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public Pages (int totalItem, int page, int pageSize)
        {
            var totalPages = (int)Math.Ceiling((decimal)totalItem / (decimal)pageSize);

            TotalItems = totalItem;
            CurrentPage = page;
            PageSize = pageSize;
            TotalPages = totalPages;
        }
    }
}
