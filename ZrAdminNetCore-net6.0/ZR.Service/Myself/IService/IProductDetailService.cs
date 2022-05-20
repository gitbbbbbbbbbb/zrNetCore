using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZR.Model.Dto.Business;
using ZR.Model.Myself;

namespace ZR.Service.Myself.IService
{
    public interface IProductDetailService
    {
        (int,string ) SpecificationaddOredit(SpecificationSaveAccept form);

       ( List<Specification>,int ) SpecificationQuery(SpecificationQueryAccept SpecificationNameQuery);


        int SpecificationDel(int  SpecificationId);
    }
}
