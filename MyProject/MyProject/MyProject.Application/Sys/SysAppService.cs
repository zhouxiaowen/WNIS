using Abp.Domain.Repositories;
using Abp.UI;
using AutoMapper;
using MyProject.Sys.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Users
{
    public class SysAppService : MyProjectAppServiceBase, ISysAppService
    {
        private readonly IRepository<Sys_DicType> _repositorySys_DicType;
        private readonly IRepository<Sys_Dic> _repositorySys_Dic;

        public SysAppService(IRepository<Sys_DicType> repositorySys_DicType,
            IRepository<Sys_Dic> repositorySys_Dic)
        {
            this._repositorySys_DicType = repositorySys_DicType;
            this._repositorySys_Dic = repositorySys_Dic;
        }

        #region 字典相关
        /// <summary>
        /// 查询字典大类信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public DicTypeQueryOutput QueryDicTypeList(DicTypeQueryInput input)
        {
            List<DicTypeQueryItem> list = new List<DicTypeQueryItem>();
            //获取一级字典
            var query = _repositorySys_DicType.GetAll().ToList();
            int count = query.Count;
            query = query.Where(p => p.DicTypeCode == "0").ToList();
            list = (from q in query
                    select new DicTypeQueryItem()
                    {
                        DicCode = q.DicCode,
                        DicName = q.DicName,
                        PX = q.PX ?? 0
                    }).OrderBy(p => p.PX).ToList();
            var output = new DicTypeQueryOutput()
            {
                TotalCount = count,
                Items = new ReadOnlyCollection<DicTypeQueryItem>(list)
            };
            return output;
        }

        /// <summary>
        /// 查询字典项目信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public DicItemQueryOutput QueryDicItemList(DicItemQueryInput input)
        {
            if (input.DicTypeCode == null || input.DicTypeCode == "")
            {
                throw new UserFriendlyException("条件不足, 无法查询字典");
            }
            List<DicItemQueryItem> list = new List<DicItemQueryItem>();
            //获取字典表数据
            var query = _repositorySys_DicType.GetAll().ToList();
            int count = query.Count;
            query = query.Where(p => p.DicTypeCode == input.DicTypeCode).ToList();
            list = (from q in query
                    select new DicItemQueryItem()
                    {
                        DicCode = q.DicCode,
                        DicName = q.DicName,
                        PX = q.PX ?? 0,
                        DicTypeCode = input.DicTypeCode,
                        PYM = q.PYM,
                        Remark = q.Remark
                    }).OrderBy(p => p.PX).ThenBy(p => p.PYM).ToList();

            var output = new DicItemQueryOutput()
            {
                TotalCount = count,
                Items = new ReadOnlyCollection<DicItemQueryItem>(list)
            };
            return output;
        }

        /// <summary>
        /// 查询字典明细信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public DicDetailQueryOutput QueryDicDetailList(DicDetailQueryInput input)
        {
            if (input.DicCode == null || input.DicCode == "")
            {
                throw new UserFriendlyException("条件不足, 无法查询字典明细");
            }
            List<DicDetailQueryItem> list = new List<DicDetailQueryItem>();
            var query = _repositorySys_Dic.GetAll().Where(p => p.DicCode == input.DicCode);
            if (input.Status != null)
            {
                query = query.Where(p => p.Status == input.Status);
            }
            list = (from q in query
                    select new DicDetailQueryItem()
                    {
                        Id = q.Id,
                        DicCode = q.DicCode,
                        Code = q.Code,
                        Name = q.Name,
                        PX = q.PX ?? 0,
                        PYM = q.PYM,
                        Status = q.Status,
                        Remark = q.Remark
                    }).OrderBy(p => p.PX).ThenBy(p => p.PYM).ToList();
            var output = new DicDetailQueryOutput()
            {
                TotalCount = list.Count,
                Items = new ReadOnlyCollection<DicDetailQueryItem>(list)
            };
            return output;
        }

        /// <summary>
        /// 获取单个字典数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public DicDetailQueryItem GetDicDetailRecord(DicDetailQueryInput input)
        {
            if (input.Id == null || input.Id <= 0)//新增
            {
                throw new UserFriendlyException("条件不足, 无法查询字典明细信息");
            }
            var query = _repositorySys_Dic.FirstOrDefault(p => p.Id == input.Id);
            if (query == null)
            {
                return null;
            }
            DicDetailQueryItem result = new DicDetailQueryItem()
            {
                DicCode = query.DicCode,
                Code = query.Code,
                Name = query.Name,
                PX = query.PX ?? 0,
                PYM = query.PYM,
                Status = query.Status,
                Id = query.Id,
                Remark = query.Remark
            };
            return result;
        }

        /// <summary>
        /// 添加字典
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public DicDetailOutput AddDicItem(DicItemInput input)
        {
            DicDetailOutput output = new DicDetailOutput();
            Sys_DicType result = _repositorySys_DicType.FirstOrDefault(p => p.DicName == input.DicName);
            if (result != null && result.Id > 0)
            {
                output.Message = "存在相同名称的字典, 无法重复添加";
            }
            else
            {
                Sys_DicType model = new Sys_DicType()
                {
                    DicTypeCode = input.DicTypeCode,
                    DicCode = input.DicCode,
                    DicName = input.DicName,
                    PX = input.PX,
                    PYM = input.PYM,
                    CreateTime = DateTime.Now,
                    Remark = input.Remark
                };
                _repositorySys_DicType.Insert(model);

            }
            return output;
        }

        /// <summary>
        /// 保存字典项目明细
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public DicDetailOutput SaveDicDetail(DicDetailInput input)
        {
            DicDetailOutput output = new DicDetailOutput();
            if (input.Id == null || input.Id <= 0)//新增
            {
                Sys_Dic result = _repositorySys_Dic.FirstOrDefault(p => p.DicCode == input.DicCode && (p.Code == input.Code || p.Name == input.Name));
                if (result != null && result.Id > 0)
                {
                    output.Message = "存在重复名称或代码的字典明细, 无法重复添加";
                }
                else
                {

                    Sys_Dic model = new Sys_Dic()
                    {
                        Code = input.Code,
                        Status = input.Status,
                        DicCode = input.DicCode,
                        Name = input.Name,
                        PX = input.PX,
                        PYM = input.PYM,
                        CreateTime = DateTime.Now,
                        Remark = input.Remark
                    };
                    _repositorySys_Dic.Insert(model);
                }
            }
            else//修改
            {
                Sys_Dic model = _repositorySys_Dic.FirstOrDefault(p => p.Id == input.Id);
                if (model != null && model.Id > 0)
                {

                    model.Code = input.Code;
                    model.Status = input.Status;
                    model.DicCode = input.DicCode;
                    model.Name = input.Name;
                    model.PX = input.PX;
                    model.PYM = input.PYM;
                    model.Remark = input.Remark;
                    model.CreateTime = DateTime.Now;
                    model.UpdateTime = DateTime.Now;
                    _repositorySys_Dic.Update(model);
                }
                else
                {
                    output.Message = "未找到当前字典,保存失败";
                }

            }
            return output;
        }

        #endregion
    }
}
