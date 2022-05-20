using Infrastructure.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZR.Model.Dto.Business;
using ZR.Model.Myself;

namespace ZR.Repository.Myself
{
    [AppService(ServiceLifetime = LifeTime.Transient)]
    public class ProductDetailRepository : BaseRepository<Product_Detail>
    {

        public Specification SpecificationQueryByid(int id)
        {

            return Context.Queryable<Specification>().Single(O=>O.SpecificationId==id && O.delFlag==0);
        }

        public Specification SpecificationyByName(string name)
        {

            return Context.Queryable<Specification>().Single(O => O.SpecificationName== name && O.delFlag==0) ;
        }

        public Specification SpecificationyCheckByName(Specification data)
        {

            return Context.Queryable<Specification>().Single(O => O.SpecificationName == data.SpecificationName && O.SpecificationId!=data.SpecificationId && O.delFlag==0);
        }


        public List< Specification> SpecificationQueryByName(string SpecificationName )
        {
            if (!string.IsNullOrWhiteSpace(SpecificationName)) {

                return Context.Queryable<Specification>().Where(o => o.SpecificationName.Contains(SpecificationName) &&o.delFlag==0).ToList();
            }
            return Context.Queryable<Specification>().ToList();
        }

        public int Specificationadd(Specification form) { 
         
            return Context.Insertable(form).ExecuteCommand();
        }

        public int Specificationedit(Specification form)
        {

            return Context.Updateable(form).ExecuteCommand(); 
        }

        public int SpecificationDel(int SpecificationId) {

          var data=  Context.Queryable<Specification>().Single(o => o.SpecificationId == SpecificationId);
            data.delFlag=0;

            return Context.Updateable(data).ExecuteCommand();
        }


    }
}
