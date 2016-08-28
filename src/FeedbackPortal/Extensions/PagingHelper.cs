using System.Collections.Generic;
using System.Linq;

namespace FeedbackPortal.Extensions
{
    public static class PagingHelper
    {
        public static int GetTotalPagesForSet<T>(IEnumerable<T> set, int pageSize)
        {
            var itemCount = set.Count();

            var pageCount = CalculateTotalPages(itemCount, pageSize);

            return pageCount;
        }

        public static int GetTotalPagesForSet<T>(IQueryable<T> set, int pageSize)
        {
            var itemCount = set.Count();

            var pageCount = CalculateTotalPages(itemCount, pageSize);

            return pageCount;
        }

        public static int CalculateSkip(int pageSize, int currentPage)
        {
            return pageSize * (currentPage - 1);
        }

        public static int CalculateTotalPages(int itemCount, int pageSize)
        {
            var pageCount = itemCount % pageSize == 0
                ? itemCount / pageSize
                : itemCount / pageSize + 1;

            return pageCount;
        }
    }
}
