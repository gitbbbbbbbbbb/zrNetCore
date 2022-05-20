using System;
using System.Collections.Generic;
using SqlSugar;
using OfficeOpenXml.Attributes;

namespace ZR.Model.Models
{
    /// <summary>
    /// 供应商管理2，数据实体对象
    ///
    /// @author zr
    /// @date 2022-05-16
    /// </summary>
    [SugarTable("supplier")]
    public class Supplier
    {
        /// <summary>
        /// 描述 : 
        /// 空值 : false  
        /// </summary>
        [EpplusTableColumn(Header = "Id")]
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// 描述 : 供应商名称
        /// 空值 : false  
        /// </summary>
        [EpplusTableColumn(Header = "供应商名称")]
        public string SupplierName { get; set; }

        /// <summary>
        /// 描述 : 地址
        /// 空值 : true  
        /// </summary>
        [EpplusTableColumn(Header = "地址")]
        public string Address { get; set; }

        /// <summary>
        /// 描述 : 联系方式
        /// 空值 : true  
        /// </summary>
        [EpplusTableColumn(Header = "联系方式")]
        public string Context { get; set; }

        /// <summary>
        /// 描述 : 备注
        /// 空值 : true  
        /// </summary>
        [EpplusTableColumn(Header = "备注")]
        public string Remark { get; set; }



    }
}