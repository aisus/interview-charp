using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Team.Extensions.Infrastrcture
{
    public class PageModel
    {
        [Range(1, 999)]
        [Required]
        public int Limit { get; set; }

        [Range(1, 9999)]
        [Required]
        public int Page { get; set; }

        [BindNever] public int Offset => Limit * (Page - 1);

        public PageModel()
        {
            (Limit, Page) = (999999, 0);
        }

        public PageModel(int limit, int page = 0)
        {
            (Limit, Page) = (limit, page);
        }
    }
}