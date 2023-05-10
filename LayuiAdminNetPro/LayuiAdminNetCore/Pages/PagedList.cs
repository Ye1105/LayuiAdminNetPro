using Microsoft.EntityFrameworkCore;

namespace LayuiAdminNetCore.Pages
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public int OffSet { get; set; }

        public bool HasPrevious => CurrentPage > 1;

        public bool HasNext => CurrentPage < TotalPages;

        public PagedList(List<T> items, int count, int pageNumber, int pageSize, int offset)
        {
            OffSet = offset;
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);  //向上取整
            AddRange(items);
        }

        public static PagedList<T> Create(System.Linq.IQueryable<T> source, int pageNumber, int pageSize, int offeset)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageNumber, pageSize, offeset);
        }

        public static PagedList<T> Create(List<T> items, long count, int pageNumber, int pageSize, int offeset)
        {
            return new PagedList<T>(items, (int)count, pageNumber, pageSize, offeset);
        }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize, int offeset)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize + offeset).Take(pageSize).ToListAsync();

            //Log.Information(source.Skip((pageNumber - 1) * pageSize + offeset).Take(pageSize).ToQueryString());
            return new PagedList<T>(items, count, pageNumber, pageSize, offeset);
        }
    }
}