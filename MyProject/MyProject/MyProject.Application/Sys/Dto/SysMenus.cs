using Abp.Application.Services.Dto;
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
    [AutoMapTo(typeof(Sys_Menus))]
    public class SysMenusInput
    {
        /// <summary> 
        /// 菜单Id
        /// </summary> 
        public int? CDId { set; get; }

        /// <summary> 
        /// 图标样式
        /// </summary> 
        public string Icon { set; get; }

        /// <summary> 
        /// 级别
        /// </summary> 
        public int Levels { set; get; }

        /// <summary> 
        /// Url地址
        /// </summary> 
        public string LinkUrl { set; get; }

        /// <summary> 
        /// 模块编码
        /// </summary> 
        public string ModuleCode { set; get; }

        /// <summary> 
        /// 菜单名称
        /// </summary> 
        public string Name { set; get; }

        /// <summary> 
        /// 上级菜单Id
        /// </summary> 
        public int PId { set; get; }

        /// <summary> 
        /// 排序
        /// </summary> 
        public int PX { set; get; }

        /// <summary> 
        /// 描述
        /// </summary> 
        public string Remark { set; get; }

        /// <summary> 
        /// 状态
        /// </summary> 
        public int Status { set; get; }

        /// <summary> 
        /// 打开目标
        /// </summary> 
        public string Target { set; get; }

    }


    public class SysMenusOutput
    {
        public int CDId { get; set; }
        public string Message { get; set; }

        public SysMenusOutput() { }

        public SysMenusOutput(string message, int CDId)
        {
            this.Message = message;
            this.CDId = CDId;
        }
    }
    #endregion

    #region detail
    public class SysMenusDetailInput
    {
        public int CDId { get; set; }
    }

    [AutoMapFrom(typeof(Sys_Menus))]
    public class SysMenusDetailOutput : SysMenusInput
    {

    }
    #endregion

    #region Query
    public class SysMenusQueryInput : PagedAndSortedResultRequestDto, IShouldNormalize
    {
        /// <summary> 
        /// 菜单名称
        /// </summary> 
        public string Name { set; get; }

        public void Normalize()
        {
            //base.Sorting = "Id Desc";
        }
    }


    public class SysMenusQueryItem
    {
        public int Id { set; get; }
        /// <summary> 
        /// 菜单Id
        /// </summary> 
        public int CDId { set; get; }

        /// <summary> 
        /// 图标样式
        /// </summary> 
        public string Icon { set; get; }


        /// <summary> 
        /// 最后操作时间
        /// </summary> 
        public DateTime? LastTime { set; get; }

        /// <summary> 
        /// 级别
        /// </summary> 
        public int? Levels { set; get; }

        /// <summary> 
        /// Url地址
        /// </summary> 
        public string LinkUrl { set; get; }

        /// <summary> 
        /// 模块编码
        /// </summary> 
        public string ModuleCode { set; get; }

        /// <summary> 
        /// 菜单名称
        /// </summary> 
        public string Name { set; get; }

        /// <summary> 
        /// 上级菜单Id
        /// </summary> 
        public int? PId { set; get; }

        /// <summary> 
        /// 排序
        /// </summary> 
        public int? PX { set; get; }

        /// <summary> 
        /// 描述
        /// </summary> 
        public string Remark { set; get; }

        /// <summary> 
        /// 状态
        /// </summary> 
        public int? Status { set; get; }

        /// <summary> 
        /// 打开目标
        /// </summary> 
        public string Target { set; get; }

    }
    public class SysMenusQueryOutput : PagedResultDto<SysMenusQueryItem>
    { }
    #endregion

    #region Del
    public class SysMenusDelInput
    {
        public int CDId { get; set; }
    }

    public class SysMenusDelOutput
    {
        public int CDId { get; set; }
        public SysMenusDelOutput()
        { }
        public SysMenusDelOutput(string message, int CDId)
        {
            this.Message = message;
            this.CDId = CDId;
        }
        public string Message { get; set; }
    }
    #endregion

    #region IsExist
    public class SysMenusIsExistInput
    {
    }

    public class SysMenusIsExistOutput
    {
        public bool IsExist { set; get; }
    }
    #endregion


}
