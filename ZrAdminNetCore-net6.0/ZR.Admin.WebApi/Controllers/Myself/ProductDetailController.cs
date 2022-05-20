using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZR.Admin.WebApi.Filters;
using ZR.Model.Dto.Business;
using ZR.Model.Myself;
using ZR.Service.Myself.IService;

namespace ZR.Admin.WebApi.Controllers.Myself
{
    [Route("api/[controller]/[action]")]
    [Verify]
    [ApiController]
    public class ProductDetailController : BaseController
    {
        private readonly IProductDetailService _productDetailService;

        public ProductDetailController(IProductDetailService productDetailService)
        {
            _productDetailService = productDetailService;
        }
        #region  规格管理
        /// <summary>
        ///规格新增或编辑接口
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SpecificationAddOrEdit(SpecificationSaveAccept form) {

            if (form.SpecificationType == "")
            {
                form.form.delFlag = 0;
                form.form.CreateBy = User.Identity.Name;
                form.form.UpdateBy = User.Identity.Name;
                form.form.CreateDate = DateTime.Now;
                form.form.UpdateDate = DateTime.Now;
            }
            else {

              
                form.form.UpdateBy = User.Identity.Name;
              
                form.form.UpdateDate = DateTime.Now;
            }
            form.form.delFlag = 0;
            var (code,errmsg)= _productDetailService.SpecificationaddOredit(form);

            return SUCCESS(new { code =code,errmsg=errmsg});
        
        }

        /// <summary>
        /// 规格列表查询
        /// </summary>
        /// <param name="SpecificationNameQuery"></param>
        /// <returns></returns>
        [HttpPost]

        public IActionResult SpecificationQuery(SpecificationQueryAccept SpecificationNameQuery) {


            var (list,totalnum) = _productDetailService.SpecificationQuery(SpecificationNameQuery);

        return SUCCESS(new { list=list,totalnum=totalnum});
        }


        /// <summary>
        /// 规格删除接口
        /// </summary>
        /// <param name="SpecificationId"></param>
        /// <returns></returns>
        [HttpDelete("{SpecificationId}")]

        public IActionResult SpecificationDel(int SpecificationId) { 
        
        
        var code=_productDetailService.SpecificationDel(SpecificationId);


            return SUCCESS(new { code=code});
        
        }

        #endregion


    }
}
