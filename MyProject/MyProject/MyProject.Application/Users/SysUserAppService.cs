using Abp.Domain.Repositories;
using AutoMapper;
using MyProject.Users.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Users
{
    public class SysUserAppService : MyProjectAppServiceBase, ISysUserAppService
    {
        private readonly IRepository<Sys_User> _repositorySys_User;

        public SysUserAppService(IRepository<Sys_User> repositorySys_User)
        {
            this._repositorySys_User = repositorySys_User;
        }

        /// <summary>
        /// 查询系统用户列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public SysUserQueryOutput QuerySysUser(SysUserQueryInput input)
        {
            if (input == null) return null;

            IQueryable<Sys_User> tmpQuery = null;
            tmpQuery = _repositorySys_User.GetAll();
            //此处可以放查询过程
            tmpQuery = tmpQuery.Where(w => w.Name.Contains(input.Key) || w.Code.Contains(input.Key));

            var query = tmpQuery.Select(obj => new SysUserQueryItem()
            {
                ID = obj.Id,
                UId = obj.UId,
                Code = obj.Code,
                LastIP = obj.LastIP,
                LastTime = obj.LastTime,
                Name = obj.Name,
                Status = obj.Status,
            });
            int total = query.Count();
            var items = query.SortAndPaging(input).ToList();

            var output = new SysUserQueryOutput()
            {
                TotalCount = total,
                Items = new ReadOnlyCollection<SysUserQueryItem>(items)
            };
            return output;
        }



        /// <summary>
        /// 查询单个详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public SysUserDetailOutput QuerySysUserDetail(SysUserDetailInput input)
        {
            if (input == null || input.UId == null )
            {
                return null;
            }
            var query = _repositorySys_User.FirstOrDefault(obj => obj.UId == input.UId);
            SysUserDetailOutput result = Mapper.Map<Sys_User, SysUserDetailOutput>(query);
            return result;
        }

        /// <summary>
        /// 添加或编辑
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public SysUserOutput AddOrUpdateSysUser(SysUserInput input)
        {
            if (input == null) throw new Exception("没有需要保存的数据");


            bool res = false;
            Sys_User tmp = null;
            
                //AutoMapper.Mapper.Map<SysUserInput, Sys_User>(input);
            //此处可以放一些参数的处理，比如排序为null则赋值为0，删除标志位null则为0

            if (string.IsNullOrEmpty(input.UId))
            {
                Sys_User sysuser = new Sys_User()
                {
                    UId = GetLSH("系统管理","系统用户ID").ToString(),
                    Code = input.Code,
                    Name = input.Name,
                    Password = input.Password,
                    Status = input.Status,
                    LastTime = DateTime.Now
                };
                tmp = _repositorySys_User.Insert(sysuser);
                res = tmp == null ? false : true;
            }
            else
            {
                tmp = _repositorySys_User.FirstOrDefault(w => w.UId == input.UId);
                if (tmp != null)
                {
                    //此处放编辑时修改的字段
                    tmp.Code = input.Code;
                    tmp.Name = input.Name;
                    tmp.Password = input.Password;
                    tmp.Status = input.Status;
                }
                _repositorySys_User.Update(tmp);

                res = tmp == null ? false : true;
            }
            ////如果是C开头的表，要删除对应的缓存
            ////this.ClearTenantCache<Sys_User>();

            return res ? new SysUserOutput("", tmp == null ? 1 : tmp.Id) : new SysUserOutput("保持系统用户信息过程中发生错误",0);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public SysUserDelOutput DeleteSysUser(SysUserDelInput input)
        {
            if (input == null )
            {
                return null;
            }
            _repositorySys_User.Delete(obj => obj.UId == input.UId);
            return new SysUserDelOutput("", input.UId);
            //return null;
        }



        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public SysUserIsExistOutput GetSysUserIsExist(SysUserIsExistInput input)
        {
            //此处规则自己修正
            //var drug = _repositorySys_User.FirstOrDefault(obj => obj.YSMC.ToUpper() == input.YSMC.ToUpper() && obj.Id != input.YSID);
            var drug = _repositorySys_User.FirstOrDefault(obj => obj.Id == input.Id);
            SysUserIsExistOutput result = new SysUserIsExistOutput();
            result.IsExist = drug == null ? false : true;
            return result;
        }
    }
}
