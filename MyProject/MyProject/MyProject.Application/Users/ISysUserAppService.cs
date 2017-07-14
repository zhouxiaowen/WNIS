using Abp.Application.Services;
using MyProject.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Users
{
    public interface ISysUserAppService : IApplicationService
    {

        /// <summary>
        /// 查询系统用户列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        SysUserQueryOutput QuerySysUser(SysUserQueryInput input);


        /// <summary>
        /// 查询单个详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        SysUserDetailOutput QuerySysUserDetail(SysUserDetailInput input);

        /// <summary>
        /// 添加或编辑
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        SysUserOutput AddOrUpdateSysUser(SysUserInput input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        SysUserDelOutput DeleteSysUser(SysUserDelInput input);

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="input"></param>
        SysUserIsExistOutput GetSysUserIsExist(SysUserIsExistInput input);
    }
}
