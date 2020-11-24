using LKDUS_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LKDUS_UI.Contracts
{
    public interface IAuthenticationRepository : IBaseRepository<LoginModel>
    {
        public Task<bool> Login(LoginModel user);
        public Task Logout();
        public  Task<IList<LoginModel>> Get(string url);

    }
}
