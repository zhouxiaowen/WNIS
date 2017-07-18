using Abp.Application.Services.Dto;
using System;

namespace MyProject.Sys.Dto
{
    /// <summary>
    /// 字典项目查询-传入
    /// </summary>
    public class DicItemQueryInput
    {
        /// <summary>
        /// 字典分类代码
        /// </summary>
        public string DicTypeCode { get; set; }

    }
    /// <summary>
    /// 字典项目查询-实体
    /// </summary>
    public class DicItemQueryItem
    {
        /// <summary>
        /// 所属字典分类代码
        /// </summary>
        public string DicTypeCode { get; set; }
        /// <summary>
        /// 字典代码
        /// </summary>
        public string DicCode { get; set; }
        /// <summary>
        /// 字典名称
        /// </summary>
        public string DicName { get; set; }
        /// <summary>
        /// 拼音码
        /// </summary>
        public string PYM { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int PX { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
    /// <summary>
    /// 字典项目查询-返回
    /// </summary>
    public class DicItemQueryOutput : PagedResultDto<DicItemQueryItem>
    { }



}
