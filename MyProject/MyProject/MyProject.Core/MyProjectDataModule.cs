using Abp.EntityFramework;
using Abp.Modules;
using MyProject.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyProject
{
    [DependsOn(typeof(AbpEntityFrameworkModule), typeof(MyProjectCoreModule))]
    public class MyProjectDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            Database.SetInitializer<MyProjectDbContext>(null);
        }
    }
}
