namespace Web.Api.Infrastructure.Siren.Links
{
    public class PagedProperties
    {
        public static string GetPageDetails(int pageNumber, int pageSize, int totalEntries)
        {
            var pageAbsoluteIndexStart = totalEntries == 0 ? 0 : (pageNumber * pageSize) + 1;

            var pageAbsoluteIndexEnd = (pageNumber * pageSize) + pageSize;
            if (totalEntries < pageAbsoluteIndexEnd)
                pageAbsoluteIndexEnd = totalEntries;

            return $"Showing {pageAbsoluteIndexStart} to {pageAbsoluteIndexEnd}";
        }
    }
}