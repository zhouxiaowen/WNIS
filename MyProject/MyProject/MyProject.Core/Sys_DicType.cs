

using System;
using Abp.Domain.Entities;

public class Sys_DicType:Entity
{

    /// <summary>
    /// 所属字典分类代码, 0 :一级分类
    /// </summary>
	public string DicTypeCode{ get; set; }


    /// <summary>
    /// 字典代码
    /// </summary>
	public string DicCode{ get; set; }


    /// <summary>
    /// 字典名称
    /// </summary>
	public string DicName{ get; set; }


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
    /// 创建时间
    /// </summary>
	public DateTime? CreateTime{ get; set; }


    /// <summary>
    /// 最近修改时间
    /// </summary>
	public DateTime? UpdateTime{ get; set; }


}
