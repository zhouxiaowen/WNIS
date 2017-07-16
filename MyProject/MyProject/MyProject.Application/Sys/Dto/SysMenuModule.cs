using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MyProject.Sys.Dto
{

    #region Insert
    [AutoMapTo(typeof(Sys_MenuModule))]
    public class SysMenuModuleInput
    {
        /// <summary> 
        /// 编码
        /// </summary> 
        public string Code { set; get; }

        /// <summary> 
        /// 链接地址
        /// </summary> 
        public string LinkUrl { set; get; }

        /// <summary> 
        /// 名称
        /// </summary> 
        public string Name { set; get; }

        /// <summary> 
        /// 状态
        /// </summary> 
        public int Status { set; get; }

    }


    public class SysMenuModuleOutput
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public SysMenuModuleOutput() { }

        public SysMenuModuleOutput(string message, string Code)
        {
            this.Message = message;
            this.Code = Code;
        }
    }
    #endregion

    #region detail
    public class SysMenuModuleDetailInput
    {
        public string Code { get; set; }
    }

    [AutoMapFrom(typeof(Sys_MenuModule))]
    public class SysMenuModuleDetailOutput : SysMenuModuleInput
    {

    }
    #endregion

    #region Query

    public class SysMenuModuleQueryInput : PagedAndSortedResultRequestDto, IShouldNormalize
    {
        /// <summary> 
        /// 名称
        /// </summary> 
        public string Key { set; get; }

        public void Normalize()
        {
            //base.Sorting = "Code";
        }
    }


    public class SysMenuModuleQueryItem
    {
        public int Id { set; get; }

        /// <summary> 
        /// 编码
        /// </summary> 
        public string Code { set; get; }

        /// <summary> 
        /// 链接地址
        /// </summary> 
        public string LinkUrl { set; get; }

        /// <summary> 
        /// 名称
        /// </summary> 
        public string Name { set; get; }

        /// <summary> 
        /// 状态
        /// </summary> 
        public int? Status { set; get; }

    }
    public class SysMenuModuleQueryOutput : PagedResultDto<SysMenuModuleQueryItem>
    { }
    #endregion

    #region Del
    public class SysMenuModuleDelInput
    {
        public string Code { get; set; }
    }

    public class SysMenuModuleDelOutput
    {
        public string Code { get; set; }
        public SysMenuModuleDelOutput()
        { }
        public SysMenuModuleDelOutput(string message, string Code)
        {
            this.Message = message;
            this.Code = Code;
        }
        public string Message { get; set; }
    }
    #endregion

    #region IsExist
    public class SysMenuModuleIsExistInput
    {
        /// <summary> 
        /// 编码
        /// </summary> 
        public string Code { set; get; }

    }

    public class SysMenuModuleIsExistOutput
    {
        public bool IsExist { set; get; }
    }
    #endregion

}