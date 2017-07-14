using System.Reflection;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace MyProject
{
    [DependsOn(typeof(AbpWebApiModule), typeof(MyProjectApplicationModule))]
    public class MyProjectWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            ///返回日期格式化
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";

            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(MyProjectApplicationModule).Assembly, "app")
                .Build();
        }

        public override void PostInitialize()
        {
            base.PostInitialize();
            //Abp默认为输出小写字母开头的json，更改为Newtonsoft.Json默认输出
            Configuration.Modules.AbpWebApi()
                    .HttpConfiguration.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new DefaultContractResolver();
        }


    }
}
