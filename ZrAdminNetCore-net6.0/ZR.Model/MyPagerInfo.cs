using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZR.Model
{
    

        public class MyPagerInfo
    {

        public int totalResult { get; set; }
            /// <summary>
            /// 当前页码
            /// </summary>
            public int CurrentPage { get; set; }
            public int PageSize { get; set; }
           /// <summary>
            /// 排序字段
            /// </summary>
            public string Sort { get; set; } = string.Empty;
            /// <summary>
            /// 排序类型,前端传入的是"asc"，"desc"
            /// </summary>
            public string SortType { get; set; } = string.Empty;

        //public MyPagerInfo(int page = 1, int pageSize = 20,string  Sortd="desc",string  sorttype="Id")
        //{
        //    CurrentPage = page;
        //    PageSize = pageSize;
        //    Sort = "desc";
        //    SortType = "Id";
        //}

    }

}
