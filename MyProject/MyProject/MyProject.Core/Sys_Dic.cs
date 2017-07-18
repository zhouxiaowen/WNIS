

using System;
using Abp.Domain.Entities;

public class Sys_Dic:Entity
{

    /// <summary>
    /// 字典编码
    /// </summary>
	public string DicCode{ get; set; }


    /// <summary>
    /// 项目代码
    /// </summary>
	public string Code{ get; set; }


    /// <summary>
    /// 项目名称
    /// </summary>
	public string Name{ get; set; }


    /// <summary>
    /// 拼音码
    /// </summary>
	public string PYM{ get; set; }


    /// <summary>
    /// 排序
    /// </summary>
	public int? PX{ get; set; }


    /// <summary>
    /// 备注
    /// </summary>
	public string Remark{ get; set; }


    /// <summary>
    /// 状态
    /// </summary>
	public int? Status{ get; set; }


    /// <summary>
    /// 创建时间
    /// </summary>
	public DateTime? CreateTime{ get; set; }


    /// <summary>
    /// 最近修改时间
    /// </summary>
	public DateTime? UpdateTime{ get; set; }


}
