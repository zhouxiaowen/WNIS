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
    public interface ISysMenuModuleAppService : IApplicationService
    {

        /// <summary>
        /// 查询菜单模块列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        SysMenuModuleQueryOutput QuerySysMenuModule(SysMenuModuleQueryInput input);

        /// <summary>
        /// 查询单个详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        SysMenuModuleDetailOutput QuerySysMenuModuleDetail(SysMenuModuleDetailInput input);

        /// <summary>
        /// 添加或编辑
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        SysMenuModuleOutput AddOrUpdateSysMenuModule(SysMenuModuleInput input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        SysMenuModuleDelOutput DeleteSysMenuModule(SysMenuModuleDelInput input);

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="input"></param>
        SysMenuModuleIsExistOutput GetSysMenuModuleIsExist(SysMenuModuleIsExistInput input);


    }

    public class SysMenuModuleAppService : MyProjectAppServiceBase, ISysMenuModuleAppService
    {
        private readonly IRepository<Sys_MenuModule> _repositorySysMenuModule;

        public SysMenuModuleAppService(IRepository<Sys_MenuModule> repositorySysMenuModule)
        {
            this._repositorySysMenuModule = repositorySysMenuModule;
        }

        /// <summary>
        /// 查询菜单模块列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public SysMenuModuleQueryOutput QuerySysMenuModule(SysMenuModuleQueryInput input)
        {
            if (input == null) return null;

            IQueryable<Sys_MenuModule> tmpQuery = null;
            tmpQuery = _repositorySysMenuModule.GetAll();
            //此处可以放查询过程
            if(!string.IsNullOrEmpty(input.Key))
                tmpQuery = tmpQuery.Where(w => w.Code.Contains(input.Key) || w.Name.Contains(input.Key));

            var query = tmpQuery.Select(obj => new SysMenuModuleQueryItem()
            {
                Id = obj.Id,
                Code = obj.Code,
                LinkUrl = obj.LinkUrl,
                Name = obj.Name,
                Status = obj.Status,
            });
            int total = query.Count();
            var items = query.SortAndPaging(input).ToList();

            var output = new SysMenuModuleQueryOutput()
            {
                TotalCount = total,
                Items = new ReadOnlyCollection<SysMenuModuleQueryItem>(items)
            };
            return output;
        }

        /// <summary>
        /// 查询单个详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public SysMenuModuleDetailOutput QuerySysMenuModuleDetail(SysMenuModuleDetailInput input)
        {
            if (input == null)
            {
                return null;
            }
            var query = _repositorySysMenuModule.FirstOrDefault(obj => obj.Code == input.Code);
            SysMenuModuleDetailOutput result = Mapper.Map<Sys_MenuModule, SysMenuModuleDetailOutput>(query);
            return result;
        }

        /// <summary>
        /// 添加或编辑
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public SysMenuModuleOutput AddOrUpdateSysMenuModule(SysMenuModuleInput input)
        {
            if (input == null) throw new Exception("没有需要保存的数据");

            SysMenuModuleIsExistOutput tmpRes = GetSysMenuModuleIsExist(new SysMenuModuleIsExistInput() { Code = input.Code});
            //if (tmpRes.IsExist)
            //{
            //return new SysMenuModuleOutput("菜单模块已存在,请修改后重新保存。", CommonData.Guid);
            //}
            bool res = false;
            Sys_MenuModule tmp = null;
            //此处可以放一些参数的处理，比如排序为null则赋值为0，删除标志位null则为0

            if (!tmpRes.IsExist)
            {
                Sys_MenuModule sysmenumodule = AutoMapper.Mapper.Map<SysMenuModuleInput, Sys_MenuModule>(input);
                //sysmenumodule.Code = GetLSH("系统管理", "系统用户ID");
                //此处可以添加默认相关参数

                tmp = _repositorySysMenuModule.Insert(sysmenumodule);
                res = tmp == null ? false : true;
            }
            else
            {
                tmp = _repositorySysMenuModule.FirstOrDefault(w => w.Code == input.Code);
                if (tmp != null)
                {
                    //此处放编辑时修改的字段
                    tmp.LinkUrl = input.LinkUrl;
                    tmp.Name = input.Name;
                    tmp.Status = input.Status;
                }
                res = tmp == null ? false : true;
            }

            return res ? new SysMenuModuleOutput("", tmp == null ? "0" : tmp.Code) : new SysMenuModuleOutput("保持菜单模块信息过程中发生错误", "0");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public SysMenuModuleDelOutput DeleteSysMenuModule(SysMenuModuleDelInput input)
        {
            if (input == null)
            {
                return null;
            }
            _repositorySysMenuModule.Delete(obj => obj.Code == input.Code);
            return new SysMenuModuleDelOutput("", input.Code);
        }



        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public SysMenuModuleIsExistOutput GetSysMenuModuleIsExist(SysMenuModuleIsExistInput input)
        {
            //此处规则自己修正
            //var drug = _repositorySysMenuModule.FirstOrDefault(obj => obj.YSMC.ToUpper() == input.YSMC.ToUpper() && obj.Id != input.YSID);
            var drug = _repositorySysMenuModule.FirstOrDefault(w =>  w.Code == input.Code);
            SysMenuModuleIsExistOutput result = new SysMenuModuleIsExistOutput();
            result.IsExist = drug == null ? false : true;
            return result;
        }
    }
}
