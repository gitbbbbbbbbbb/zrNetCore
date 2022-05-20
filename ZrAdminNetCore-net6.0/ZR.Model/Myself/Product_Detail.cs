using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZR.Model.Myself
{
    [SugarTable("product_detail")]

      public class Product_Detail
    {

      [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public int Id { get; set; }
        public string ProductName { get; set; }

        public int  SpecificationId { get; set; }

        public string ProductCode { get; set; }

        public int  DefaultUnit { get; set; }


        public string ImgUrl { get; set; }

        public int  FirstLevelClassId { get; set; }


        public int  TwoLevelClassId { get; set; }


        public int ThreeLevelClassId { get; set; }

        public int delFlag { get; set; }

        public DateTime? CreateDate { get; set; }


        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }

    }
}
