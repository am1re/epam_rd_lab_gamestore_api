using System.Collections.Generic;

namespace Application.Common.ViewModels
{
    public class EntitiesListVm<T>
    {
        public PaginationInfo Pagination { get; set; }
        public ICollection<T> Data { get; set; }
    }
}