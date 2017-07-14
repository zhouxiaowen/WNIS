using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyProject
{
    public interface  ITestAppService: IApplicationService
    {
        
        string GetStringTest();

        List<Sys_User> GetUserList();
    }
}
