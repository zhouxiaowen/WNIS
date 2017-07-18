using Abp.Application.Services.Dto;
using System;

namespace MyProject.Sys.Dto
{
    /// <summary>
    /// 字典项目明细查询-传入
    /// </summary>
    public class DicDetailQueryInput
    {
        /// <summary>
        /// 所属字典代码
        /// </summary>
        public string DicCode { get; set; }
        /// <summary>
        /// 字典记录id
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 状态 1:有效, 0:无效
        /// </summary>
        public int? Status { get; set; }
    }

    /// <summary>
    /// 字典项目明细查询-实体
    /// </summary>
    public class DicDetailQueryItem: DicDetailInput
    {
    }
    /// <summary>
    /// 字典项目明细查询-返回
    /// </summary>
    public class DicDetailQueryOutput : PagedResultDto<DicDetailQueryItem>
    { }



}
