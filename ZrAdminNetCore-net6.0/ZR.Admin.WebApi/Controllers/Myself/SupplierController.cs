using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZR.Model.Myself;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Threading.Tasks;
using ZR.Service.Myself.IService;

namespace ZR.Admin.WebApi.Controllers.Myself
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SupplierController : BaseController
    {

        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        /// <summary>
        /// 供应商新增
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public IActionResult SupplierAdd(Supplier form ) {


         int code=  _supplierService.SupplierAdd(form);

            //返回json给前端
            return SUCCESS(new { code=0});
            
        }

    }
}
