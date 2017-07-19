using Abp.Application.Services.Dto;
using System;

namespace MyProject.Sys.Dto
{
    /// <summary>
    /// 字典保存-传入
    /// </summary>
    public class DicItemInput
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
        public int? PX { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
    /// <summary>
    /// 字典项目明细保存-传入
    /// </summary>
    public class DicDetailInput
    {

        /// <summary>
        /// ID
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 所属字典代码
        /// </summary>
        public string DicCode { get; set; }
        /// <summary>
        /// 项目代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 拼音码
        /// </summary>
        public string PYM { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int? PX { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }
    }
    /// <summary>
    /// 字典项目明细保存-返回
    /// </summary>
    public class DicDetailOutput
    {
        public string Message { get; set; }

        public DicDetailOutput() { }

        public DicDetailOutput(string message)
        {
            this.Message = message;
        }
    }



}
