using Infrastructure.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZR.Model.Myself;
using ZR.Repository.Myself;
using ZR.Service.Myself.IService;

namespace ZR.Service.Myself.IMyselfService
{
    [AppService(ServiceType = typeof(ISupplierService), ServiceLifetime = LifeTime.Transient)]
    public class SupplierService : ISupplierService
    {
        private readonly SupplierRepository _SupplierRepository;

        public SupplierService(SupplierRepository supplierRepository)
        {
            _SupplierRepository = supplierRepository;
        }

        public int SupplierAdd(Supplier form)
        {

         int code=   _SupplierRepository.SupplierAdd(form);
          
            return code;
        }
    }
}
