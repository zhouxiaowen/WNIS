using System.Reflection;
using Abp.Modules;

namespace MyProject
{
    [DependsOn(typeof(MyProjectCoreModule))]
    public class MyProjectApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
