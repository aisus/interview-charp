using System.Collections.Generic;

namespace Sample.Extensions.Infrastrcture
{
    /// <summary>
    /// Paginated list result
    /// </summary>
    public class CollectionOutputModel<T>
    {
        public int ItemsCount { get; }

        public int Limit { get; }

        public int Page { get; }
        
        public List<T> Results { get; }

        public CollectionOutputModel(int limit, int page, int itemsCount, List<T> results)
        {
            (Limit, Page, ItemsCount, Results) = (limit, page, itemsCount, results);
        }

        public CollectionOutputModel(int limit, int page)
        {
            (Limit, Page, ItemsCount, Results) = (limit, page, 0, new List<T>());
        }
    }
}