using Infrastructure.Attribute;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZR.Model.Myself;

namespace ZR.Repository.Myself
{
    [AppService(ServiceLifetime = LifeTime.Transient)]
    public class SupplierRepository : BaseRepository<Supplier>
    {

        public int SupplierAdd(Supplier form) {
            int code = 0;
         var checkdata=   Context.Queryable<Supplier>().Single(o => o.SupplierName == form.SupplierName);
            if (checkdata == null)
            {
                Context.Insertable(form).ExecuteCommand();
            }
            else {

                code = 1;
            }
            return code;
        }
    }
}
