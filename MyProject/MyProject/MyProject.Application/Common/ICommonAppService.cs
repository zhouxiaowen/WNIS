using Abp.Application.Services;
using MyProject.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Common
{
    public interface ICommonAppService: IApplicationService
    {
        /// 查询流水号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        SysLSHOutput GetLSH(SysLSHInput input);
    }
}
