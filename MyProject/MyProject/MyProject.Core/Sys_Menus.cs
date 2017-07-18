using System;
using Abp.Domain.Entities;

public class Sys_Menus:Entity
{
    /// <summary>
    /// 菜单Id
    /// </summary>
	public int CDId{ get; set; }

    /// <summary>
    /// 上级菜单Id
    /// </summary>
	public int? PId{ get; set; }

    /// <summary>
    /// 模块编码
    /// </summary>
	public string ModuleCode{ get; set; }

    /// <summary>
    /// 菜单名称
    /// </summary>
	public string Name{ get; set; }

    /// <summary>
    /// 描述
    /// </summary>
	public string Remark{ get; set; }

    /// <summary>
    /// 级别
    /// </summary>
	public int? Levels{ get; set; }

    /// <summary>
    /// 图标样式
    /// </summary>
	public string Icon{ get; set; }

    /// <summary>
    /// Url地址
    /// </summary>
	public string LinkUrl{ get; set; }

    /// <summary>
    /// 打开目标
    /// </summary>
	public string Target{ get; set; }

    /// <summary>
    /// 排序
    /// </summary>
	public int? PX{ get; set; }

    /// <summary>
    /// 状态
    /// </summary>
	public int? Status{ get; set; }

    /// <summary>
    /// 最后操作时间
    /// </summary>
	public DateTime? LastTime{ get; set; }

}
