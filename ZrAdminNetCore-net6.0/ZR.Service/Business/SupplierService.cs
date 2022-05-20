using System;
using SqlSugar;
using System.Collections.Generic;
using Infrastructure;
using Infrastructure.Attribute;
using ZR.Model;
using ZR.Model.Dto;
using ZR.Model.Models;
using ZR.Repository;
using ZR.Service.Business.IBusinessService;

namespace ZR.Service.Business
{
    /// <summary>
    /// 供应商管理2Service业务层处理
    ///
    /// @author zr
    /// @date 2022-05-16
    /// </summary>
    [AppService(ServiceType = typeof(ISupplierService), ServiceLifetime = LifeTime.Transient)]
    public class SupplierService : BaseService<Supplier>, ISupplierService
    {
        private readonly SupplierRepository _SupplierRepository;
        public SupplierService(SupplierRepository repository)
        {
            _SupplierRepository = repository;
        }

        #region 业务逻辑代码

        /// <summary>
        /// 查询供应商管理2列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<Supplier> GetList(SupplierQueryDto parm)
        {
            //开始拼装查询条件
            var predicate = Expressionable.Create<Supplier>();

            //搜索条件查询语法参考Sqlsugar
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.SupplierName), it => it.SupplierName.Contains(parm.SupplierName));
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.Address), it => it.Address == parm.Address);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.Context), it => it.Context == parm.Context);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.Remark), it => it.Remark == parm.Remark);
            var response = _SupplierRepository
                .Queryable()
                .OrderBy("Id desc")
                .Where(predicate.ToExpression())
                .ToPage(parm);

            return response;
        }

        /// <summary>
        /// 添加供应商管理2
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public int AddSupplier(Supplier parm)
        {
            var response = _SupplierRepository.Insert(parm, it => new
            {
                it.SupplierName,
                it.Address,
                it.Context,
                it.Remark,
            });
            return response;
        }
        #endregion
    }
}