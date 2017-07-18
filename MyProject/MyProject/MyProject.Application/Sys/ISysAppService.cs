using Abp.Application.Services;
using MyProject.Sys.Dto;
using System;

namespace MyProject.Users
{
    public interface ISysAppService : IApplicationService
    {

        #region 字典相关
        /// <summary>
        /// 查询字典大类信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        DicTypeQueryOutput QueryDicTypeList(DicTypeQueryInput input);
        /// <summary>
        /// 查询字典项目信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        DicItemQueryOutput QueryDicItemList(DicItemQueryInput input);
        /// <summary>
        /// 查询字典明细信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        DicDetailQueryOutput QueryDicDetailList(DicDetailQueryInput input);
        /// <summary>
        /// 获取单个字典数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        DicDetailQueryItem GetDicDetailRecord(DicDetailQueryInput input);
        /// <summary>
        /// 添加字典
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        DicDetailOutput AddDicItem(DicItemInput input);
        /// <summary>
        /// 保存字典项目明细
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        DicDetailOutput SaveDicDetail(DicDetailInput input);
        #endregion

    }
}
