using Abp.Application.Services;
using Abp.Domain.Repositories;
using AutoMapper;
using MyProject.Sys.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Sys
{
    public interface ISysMenusAppService : IApplicationService
    {

        /// <summary>
        /// 查询系统菜单列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        SysMenusQueryOutput QuerySysMenus(SysMenusQueryInput input);

        /// <summary>
        /// 查询单个详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        SysMenusDetailOutput QuerySysMenusDetail(SysMenusDetailInput input);

        /// <summary>
        /// 添加或编辑
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        SysMenusOutput AddOrUpdateSysMenus(SysMenusInput input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        SysMenusDelOutput DeleteSysMenus(SysMenusDelInput input);

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="input"></param>
        SysMenusIsExistOutput GetSysMenusIsExist(SysMenusIsExistInput input);


    }


    public class SysMenusAppService : MyProjectAppServiceBase, ISysMenusAppService
    {
        private readonly IRepository<Sys_Menus> _repositorySysMenus;

        public SysMenusAppService(IRepository<Sys_Menus> repositorySysMenus)
        {
            this._repositorySysMenus = repositorySysMenus;
        }

        /// <summary>
        /// 查询系统菜单列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public SysMenusQueryOutput QuerySysMenus(SysMenusQueryInput input)
        {
            if (input == null) return null;

            IQueryable<Sys_Menus> tmpQuery = null;
            tmpQuery = _repositorySysMenus.GetAll();
            //此处可以放查询过程


            var query = tmpQuery.Select(obj => new SysMenusQueryItem()
            {
                Id = obj.Id,
                CDId = obj.CDId,
                Icon = obj.Icon,
                LastTime = obj.LastTime,
                Levels = obj.Levels,
                LinkUrl = obj.LinkUrl,
                ModuleCode = obj.ModuleCode,
                Name = obj.Name,
                PId = obj.PId,
                PX = obj.PX,
                Remark = obj.Remark,
                Status = obj.Status,
                Target = obj.Target,
            });
            int total = query.Count();
            var items = query.SortAndPaging(input).ToList();

            var output = new SysMenusQueryOutput()
            {
                TotalCount = total,
                Items = new ReadOnlyCollection<SysMenusQueryItem>(items)
            };
            return output;
        }

        /// <summary>
        /// 查询单个详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public SysMenusDetailOutput QuerySysMenusDetail(SysMenusDetailInput input)
        {
            if (input == null)
            {
                return null;
            }
            var query = _repositorySysMenus.FirstOrDefault(obj => obj.CDId == input.CDId);
            SysMenusDetailOutput result = Mapper.Map<Sys_Menus, SysMenusDetailOutput>(query);
            return result;
        }

        /// <summary>
        /// 添加或编辑
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public SysMenusOutput AddOrUpdateSysMenus(SysMenusInput input)
        {
            if (input == null) throw new Exception("没有需要保存的数据");

            //SysMenusIsExistOutput tmpRes = GetSysMenusIsExist(new SysMenusIsExistInput() { YSMC = input.YSMC, YSID = input.ID });
            //if (tmpRes.IsExist)
            //{
            //return new SysMenusOutput("系统菜单已存在,请修改后重新保存。", CommonData.Guid);
            //}
            bool res = false;
            Sys_Menus tmp = null;
            //此处可以放一些参数的处理，比如排序为null则赋值为0，删除标志位null则为0

            if (input.CDId == null)
            {
                Sys_Menus sysmenus = AutoMapper.Mapper.Map<SysMenusInput, Sys_Menus>(input);
                sysmenus.CDId = GetLSH("系统管理", "系统用户ID");
                //此处可以添加默认相关参数

                tmp = _repositorySysMenus.Insert(sysmenus);
                res = tmp == null ? false : true;
            }
            else
            {
                tmp = _repositorySysMenus.FirstOrDefault(w => w.CDId == input.CDId);
                if (tmp != null)
                {
                    //此处放编辑时修改的字段
                    tmp.Icon = input.Icon;
                    tmp.Levels = input.Levels;
                    tmp.LinkUrl = input.LinkUrl;
                    tmp.ModuleCode = input.ModuleCode;
                    tmp.Name = input.Name;
                    tmp.PId = input.PId;
                    tmp.PX = input.PX;
                    tmp.Remark = input.Remark;
                    tmp.Status = input.Status;
                    tmp.Target = input.Target;
                }
                res = tmp == null ? false : true;
            }

            return res ? new SysMenusOutput("", tmp == null ? 0 : tmp.CDId) : new SysMenusOutput("保持系统菜单信息过程中发生错误", 0);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public SysMenusDelOutput DeleteSysMenus(SysMenusDelInput input)
        {
            if (input == null)
            {
                return null;
            }
            _repositorySysMenus.Delete(obj => obj.CDId == input.CDId);
            return new SysMenusDelOutput("", input.CDId);
        }



        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public SysMenusIsExistOutput GetSysMenusIsExist(SysMenusIsExistInput input)
        {
            //此处规则自己修正
            //var drug = _repositorySysMenus.FirstOrDefault(obj => obj.YSMC.ToUpper() == input.YSMC.ToUpper() && obj.Id != input.YSID);
            var drug = _repositorySysMenus.FirstOrDefault(w => true);
            SysMenusIsExistOutput result = new SysMenusIsExistOutput();
            result.IsExist = drug == null ? false : true;
            return result;
        }

    }
}
