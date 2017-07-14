using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject
{

    public class TestAppService : ITestAppService
    {
        private readonly IRepository<Sys_User> _repositoryUser;
        public TestAppService(IRepository<Sys_User> repositoryUser)
        {
            this._repositoryUser = repositoryUser;
        }

        public string GetStringTest()
        {
            return "1111";
        }

        public List<Sys_User> GetUserList()
        {
            var userList = _repositoryUser.GetAll();
            return userList.ToList();
        }
    }
}
