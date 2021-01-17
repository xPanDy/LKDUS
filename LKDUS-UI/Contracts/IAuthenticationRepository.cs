using LKDUS_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LKDUS_UI.Contracts
{
    public interface IAuthenticationRepository : IBaseRepository<LoginModel>, IBaseRepository<LoginModelCreate>
    {
        public Task<bool> Login(LoginModel user);
        public Task<bool> Register(LoginModelCreate user);
        public Task Logout();
        public new Task<IList<LoginModel>> Get(string url);
  //      public Task<LoginModel> Get(string url,int id);
  //      public Task<LoginModel> GetById(string   id);
         public Task<LoginModel> Get(string url,string id);
        public new Task<bool> DeleteUserById(string url, string id);
        
    }
}
