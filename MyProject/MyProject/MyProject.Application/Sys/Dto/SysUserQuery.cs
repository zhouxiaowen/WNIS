﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Sys.Dto
{
    #region Insert
    [AutoMapTo(typeof(Sys_User))]
    public class SysUserInput
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int? UId { get; set; }
        /// <summary> 
        /// 工号
        /// </summary> 
        public string Code { set; get; }

        /// <summary> 
        /// 姓名
        /// </summary> 
        public string Name { set; get; }

        /// <summary> 
        /// 密码
        /// </summary> 
        public string Password { set; get; }

        /// <summary> 
        /// 状态
        /// </summary> 
        public int Status { set; get; }

    }


    public class SysUserOutput
    {
        public int UId { get; set; }
        public string Message { get; set; }

        public SysUserOutput() { }

        public SysUserOutput(string message, int UId)
        {
            this.Message = message;
            this.UId = UId;
        }
    }
    #endregion

    #region Detail
    public class SysUserDetailInput
    {
        public int UId { get; set; }
    }

    [AutoMapFrom(typeof(Sys_User))]
    public class SysUserDetailOutput : SysUserInput
    {

    }
    #endregion

    #region Query
    public class SysUserQueryInput : PagedAndSortedResultRequestDto, IShouldNormalize
    {
        /// <summary> 
        /// 关键字
        /// </summary> 
        public string Key { set; get; }
        

        public void Normalize()
        {
            base.Sorting = "Id Desc";
        }
    }


    public class SysUserQueryItem
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int UId { get; set; }

        /// <summary> 
        /// 工号
        /// </summary> 
        public string Code { set; get; }

        /// <summary> 
        /// Id 
        /// </summary> 
        public int Id { set; get; }

        /// <summary> 
        /// 最后登陆IP
        /// </summary> 
        /// 

        public string LastIP { set; get; }

        /// <summary> 
        /// 最后登陆时间
        /// </summary> 
        public DateTime? LastTime { set; get; }

        /// <summary> 
        /// 姓名
        /// </summary> 
        public string Name { set; get; }

        /// <summary> 
        /// 状态
        /// </summary> 
        public int? Status { set; get; }

    }
    public class SysUserQueryOutput : PagedResultDto<SysUserQueryItem>
    { }
    #endregion

    #region del

    public class SysUserDelInput
    {
        public int UId { get; set; }
    }

    public class SysUserDelOutput
    {
        public int UId { get; set; }
        public SysUserDelOutput()
        { }
        public SysUserDelOutput(string message,int uid)
        {
            this.Message = message;
            this.UId = uid;
        }
        public string Message { get; set; }
    }

    #endregion

    #region IsExist
    public class SysUserIsExistInput
    {
        /// <summary> 
        /// Id
        /// </summary> 
        public int UId { set; get; }

    }

    public class SysUserIsExistOutput
    {
        public bool IsExist { set; get; }
    }

    #endregion
}
