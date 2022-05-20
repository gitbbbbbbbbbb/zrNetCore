using System.Collections;
using System.Collections.Generic;

namespace Utils.PagedList
{
    public interface IPagedList : IEnumerable
    {
        //当前页
        int CurrentPageIndex { get; set; }

        //每页数
        int PageSize { get; set; }

        //总条数
        int TotalItemCount { get; set; }
    }

    public interface IPagedList<T> : IEnumerable<T>, IPagedList { }
}
