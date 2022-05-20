using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZR.Model.Myself
{
    [SugarTable("Supplier")]

      public class Supplier
    {

      [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public int Id { get; set; }
        public string SupplierName { get; set; }

        public string  Address{ get; set; }

        public string Context { get; set; }

        public string Remark { get; set; }

    }
}
