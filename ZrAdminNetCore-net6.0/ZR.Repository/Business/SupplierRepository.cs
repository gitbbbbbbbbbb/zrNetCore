using System;
using Infrastructure.Attribute;
using ZR.Repository.System;
using ZR.Model.Models;

namespace ZR.Repository
{
    /// <summary>
    /// 供应商管理2仓储
    ///
    /// @author zr
    /// @date 2022-05-16
    /// </summary>
    [AppService(ServiceLifetime = LifeTime.Transient)]
    public class SupplierRepository : BaseRepository<Supplier>
    {
        #region 业务逻辑代码
        #endregion
    }
}