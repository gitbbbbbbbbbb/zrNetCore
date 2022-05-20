using Infrastructure.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.PagedList;
using ZR.Model.Dto.Business;
using ZR.Model.Myself;
using ZR.Repository.Myself;
using ZR.Service.Myself.IService;

namespace ZR.Service.Myself.MyselfService
{

    [AppService(ServiceType = typeof(IProductDetailService), ServiceLifetime = LifeTime.Transient)]
    public  class ProductDetailService : IProductDetailService
    {

         private readonly ProductDetailRepository _productDetailRepository;

        public ProductDetailService(ProductDetailRepository productDetailRepository)
        {
            _productDetailRepository = productDetailRepository;
        }

        public (int,string) SpecificationaddOredit(SpecificationSaveAccept form) {

           

                int code = 0;
                string errmsg = "";//错误提示信息

            if (form.SpecificationType == "AddSpecification")
            {
                var Checkdata = _productDetailRepository.SpecificationyByName(form.form.SpecificationName);
                if (Checkdata != null)
                {
                    errmsg = form.form.SpecificationName + "该规格名称已经存在不能重复保存";
                    return (code, errmsg);
                }

                code = _productDetailRepository.Specificationadd(form.form);
            }

            else if (form.SpecificationType == "EditSpecification")
            {
               var checkdata=_productDetailRepository.SpecificationyCheckByName (form.form);

                if (checkdata != null) {
                    errmsg = form.form.SpecificationName + "该规格名称已经存在不能重复保存";
                    return (code, errmsg);
                }

                var data = _productDetailRepository.SpecificationQueryByid(form.form.SpecificationId);
                
                data.SpecificationName = form.form.SpecificationName;

                code = _productDetailRepository.Specificationedit(data);

            }
         

            return (code,errmsg);
        }


        public (List<Specification>, int) SpecificationQuery(SpecificationQueryAccept SpecificationQuery) {

            var querydata = _productDetailRepository.SpecificationQueryByName(SpecificationQuery.SpecificationNameQuery);

            var total=  querydata.Count;//获取总行数


            var fenyedata = QueryOrederBLL.SetQueryableOrder(querydata.AsQueryable(), SpecificationQuery.pagerinfo.Sort,SpecificationQuery.pagerinfo.SortType).ToPagedList(SpecificationQuery.pagerinfo.CurrentPage,SpecificationQuery.pagerinfo.PageSize);

            var totalnum = fenyedata.TotalItemCount;//获取总行数

            return (fenyedata.ToList(), totalnum);

        }


        public int SpecificationDel(int SpecificationId) { 
        
        
            return _productDetailRepository.SpecificationDel(SpecificationId);
        
        }
    }
}
