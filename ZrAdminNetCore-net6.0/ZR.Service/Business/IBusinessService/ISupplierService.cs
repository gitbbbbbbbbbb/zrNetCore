using System;
using ZR.Model;
using ZR.Model.Dto;
using ZR.Model.Models;
using System.Collections.Generic;

namespace ZR.Service.Business.IBusinessService
{
    /// <summary>
    /// 供应商管理2service接口
    ///
    /// @author zr
    /// @date 2022-05-16
    /// </summary>
    public interface ISupplierService : IBaseService<Supplier>
    {
        PagedInfo<Supplier> GetList(SupplierQueryDto parm);

        int AddSupplier(Supplier parm);
    }
}
