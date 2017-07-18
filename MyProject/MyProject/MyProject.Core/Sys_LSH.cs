
























using System;
using Abp.Domain.Entities;

public class Sys_LSH:Entity
{

    /// <summary>
    /// 分类
    /// </summary>
	public string Category{ get; set; }


    /// <summary>
    /// 名称
    /// </summary>
	public string Name{ get; set; }


    /// <summary>
    /// 流水号
    /// </summary>
	public int Code{ get; set; }


    /// <summary>
    /// 创建时间
    /// </summary>
	public DateTime? CreateTime{ get; set; }


    /// <summary>
    /// 更新时间
    /// </summary>
	public DateTime? UpdateTime{ get; set; }


}
