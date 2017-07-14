using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyProject.Common.Dto;

namespace MyProject.Common
{
    public class CommonAppService: ICommonAppService
    {
        private readonly IRepository<Sys_LSH> _repositorySys_LSH;

        public CommonAppService(IRepository<Sys_LSH> repositorySys_LSH)
        {
            this._repositorySys_LSH = repositorySys_LSH;
        }

        /// <summary>
        /// 获取流水号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public SysLSHOutput GetLSH(SysLSHInput input)
        {
            var lsh = _repositorySys_LSH.FirstOrDefault(w => w.Category == input.Category && w.Name == input.Name);
            if (lsh == null)
            {
                lsh = new Sys_LSH()
                {
                    Category = input.Category,
                    Name = input.Name,
                    Code = 1,
                    CreateTime = DateTime.Now
                };
                _repositorySys_LSH.Insert(lsh);
            }
            else {
                lsh.Code = lsh.Code + input.Num;
                lsh.UpdateTime = DateTime.Now;
                _repositorySys_LSH.Update(lsh);
            }

            SysLSHOutput output = new SysLSHOutput()
            {
                Category = lsh.Category,
                Name = lsh.Name,
                Code = lsh.Code,
                Num = input.Num,
                ID = lsh.Id
            };

            return output;
        }
    }
}
