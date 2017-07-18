

using System;
using Abp.Domain.Entities;

public class Sys_User:Entity
{

    /// <summary>
    /// 用户id
    /// </summary>
	public int UId{ get; set; }


    /// <summary>
    /// 工号
    /// </summary>
	public string Code{ get; set; }


    /// <summary>
    /// 姓名
    /// </summary>
	public string Name{ get; set; }


    /// <summary>
    /// 密码
    /// </summary>
	public string Password{ get; set; }


    /// <summary>
    /// 最后登陆IP
    /// </summary>
	public string LastIP{ get; set; }


    /// <summary>
    /// 最后登陆时间
    /// </summary>
	public DateTime? LastTime{ get; set; }


    /// <summary>
    /// 状态
    /// </summary>
	public int? Status{ get; set; }


}
