using Infrastructure;
using Infrastructure.Attribute;
using Infrastructure.Enums;
using Infrastructure.Model;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using ZR.Model.Dto;
using ZR.Model.Models;
using ZR.Service.Business.IBusinessService;
using ZR.Admin.WebApi.Extensions;
using ZR.Admin.WebApi.Filters;
using ZR.Common;

namespace ZR.Admin.WebApi.Controllers
{
    /// <summary>
    /// 供应商管理2Controller
    /// 
    /// @tableName supplier
    /// @author zr
    /// @date 2022-05-16
    /// </summary>
    [Verify]
    [Route("business/Supplier")]
    public class SupplierController : BaseController
    {
        /// <summary>
        /// 供应商管理2接口
        /// </summary>
        private readonly ISupplierService _SupplierService;

        public SupplierController(ISupplierService SupplierService)
        {
            _SupplierService = SupplierService;
        }

        /// <summary>
        /// 查询供应商管理2列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "business:supplier:list")]
        public IActionResult QuerySupplier([FromQuery] SupplierQueryDto parm)
        {
            var response = _SupplierService.GetList(parm);
            return SUCCESS(response);
        }


        /// <summary>
        /// 查询供应商管理2详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        [ActionPermissionFilter(Permission = "business:supplier:query")]
        public IActionResult GetSupplier(int Id)
        {
            var response = _SupplierService.GetFirst(x => x.Id == Id);
            
            return SUCCESS(response);
        }

        /// <summary>
        /// 添加供应商管理2
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "business:supplier:add")]
        [Log(Title = "供应商管理2", BusinessType = BusinessType.INSERT)]
        public IActionResult AddSupplier([FromBody] SupplierDto parm)
        {
            if (parm == null)
            {
                throw new CustomException("请求参数错误");
            }
            //从 Dto 映射到 实体
            var modal = parm.Adapt<Supplier>().ToCreate(HttpContext);

            var response = _SupplierService.AddSupplier(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 更新供应商管理2
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "business:supplier:edit")]
        [Log(Title = "供应商管理2", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateSupplier([FromBody] SupplierDto parm)
        {
            if (parm == null)
            {
                throw new CustomException("请求实体不能为空");
            }
            //从 Dto 映射到 实体
            var modal = parm.Adapt<Supplier>().ToUpdate(HttpContext);

            var response = _SupplierService.Update(w => w.Id == modal.Id, it => new Supplier()
            {
                //Update 字段映射
                SupplierName = modal.SupplierName,
                Address = modal.Address,
                Context = modal.Context,
                Remark = modal.Remark,
            });

            return ToResponse(response);
        }

        /// <summary>
        /// 删除供应商管理2
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{ids}")]
        [ActionPermissionFilter(Permission = "business:supplier:delete")]
        [Log(Title = "供应商管理2", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteSupplier(string ids)
        {
            int[] idsArr = Tools.SpitIntArrary(ids);
            if (idsArr.Length <= 0) { return ToResponse(ApiResult.Error($"删除失败Id 不能为空")); }

            var response = _SupplierService.Delete(idsArr);

            return ToResponse(response);
        }

        /// <summary>
        /// 导出供应商管理2
        /// </summary>
        /// <returns></returns>
        [Log(Title = "供应商管理2", BusinessType = BusinessType.EXPORT, IsSaveResponseData = false)]
        [HttpGet("export")]
        [ActionPermissionFilter(Permission = "business:supplier:export")]
        public IActionResult Export([FromQuery] SupplierQueryDto parm)
        {
            parm.PageSize = 10000;
            var list = _SupplierService.GetList(parm).Result;

            string sFileName = ExportExcel(list, "Supplier", "供应商管理2");
            return SUCCESS(new { path = "/export/" + sFileName, fileName = sFileName });
        }

    }
}