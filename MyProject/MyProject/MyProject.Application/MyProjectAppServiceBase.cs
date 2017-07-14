using Abp.Application.Services;
using Abp.Domain.Repositories;
using MyProject.Common.Dto;
using System;

namespace MyProject
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class MyProjectAppServiceBase : ApplicationService
    {

        private readonly IRepository<Sys_LSH> _repositorySys_LSH;

        protected MyProjectAppServiceBase()
        {
            LocalizationSourceName = MyProjectConsts.LocalizationSourceName;
            this._repositorySys_LSH = Abp.Dependency.IocManager.Instance.Resolve<IRepository<Sys_LSH>>(); //repositorySys_LSH;  //IRepository<Sys_LSH> repositorySys_LSH
        }

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
            else
            {
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

        public Int64 GetLSH(string category, string name,int step)
        {
            var output = GetLSH(new SysLSHInput()
            {
                Category = category,
                Name = name,
                Num = step
            });
            return output.Code;
        }

        public Int64 GetLSH(string category, string name)
        {
            return GetLSH(category, name,1);
        }
    }
}