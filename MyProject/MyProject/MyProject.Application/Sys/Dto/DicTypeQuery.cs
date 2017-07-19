using Abp.Application.Services.Dto;
using System;

namespace MyProject.Sys.Dto
{
    /// <summary>
    /// 字典大类查询-传入
    /// </summary>
    public class DicTypeQueryInput
    {
        /// <summary>
        /// 字典代码
        /// </summary>
        public string DicCode { get; set; }
    }

    /// <summary>
    /// 字典大类查询-实体
    /// </summary>
    public class DicTypeQueryItem
    {
        /// <summary>
        /// 字典分类代码
        /// </summary>
        public string DicCode { get; set; }
        /// <summary>
        /// 字典分类名称
        /// </summary>
        public string DicName { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int? PX { get; set; }
    }
    /// <summary>
    /// 字典大类查询-返回
    /// </summary>
    public class DicTypeQueryOutput : PagedResultDto<DicTypeQueryItem>
    {
    }



}
