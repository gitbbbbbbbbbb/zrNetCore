using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZR.Model.Myself
{


    [SugarTable("specification")]
    public class Specification
    {

        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public int SpecificationId { get; set; }

        public string SpecificationName { get; set; }


        public int delFlag { get; set; }

        public DateTime? CreateDate { get; set; }


        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }
    }
}
