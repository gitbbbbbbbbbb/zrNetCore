using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ZR.Model.Dto;
using ZR.Model.Models;

namespace ZR.Model.Dto
{
    /// <summary>
    /// 供应商管理2输入对象
    /// </summary>
    public class SupplierDto
    {
        [Required(ErrorMessage = "不能为空")]
        public int Id { get; set; }
        [Required(ErrorMessage = "供应商名称不能为空")]
        public string SupplierName { get; set; }
        public string Address { get; set; }
        public string Context { get; set; }
        public string Remark { get; set; }
    }

    /// <summary>
    /// 供应商管理2查询对象
    /// </summary>
    public class SupplierQueryDto : PagerInfo 
    {
        public string SupplierName { get; set; }
        public string Address { get; set; }
        public string Context { get; set; }
        public string Remark { get; set; }
    }
}
