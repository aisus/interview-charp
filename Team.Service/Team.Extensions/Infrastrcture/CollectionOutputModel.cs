using System.Collections.Generic;

namespace Team.Extensions.Infrastrcture
{
    /// <summary>
    /// Модель постраничного вывода записей
    /// </summary>
    public class CollectionOutputModel<T>
    {
        /// <summary>
        /// Всего записей
        /// </summary>
        public int ItemsCount { get; }

        /// <summary>
        /// Размер страницы
        /// </summary>
        public int Limit { get; }

        /// <summary>
        /// Текущая страница
        /// </summary>
        public int Page { get; }

        /// <summary>
        /// Записи страницы
        /// </summary>
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