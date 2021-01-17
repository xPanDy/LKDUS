using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LKDUS_UI
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> Get(string url, int id);
        Task<T> Get(  int id);
        Task<IList<T>> Get(string url);
        Task<bool> Create(string url, T obj);
        Task<bool> Put(string url, T obj);
        Task<bool> Update(string url, T obj, int id);
        Task<bool> Update(string url, T obj );
        Task<bool> Update(string url, T obj, string id);
        Task<bool> Delete(string url, int id);
        Task<bool> DeleteUserById(string url, string idd);
    }
}
