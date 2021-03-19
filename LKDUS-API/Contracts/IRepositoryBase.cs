using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LKDUS_API.Contracts
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<IList<T>> FindAll();
        Task<T> FindById(int id);//Task<T> FindById(string id);
        
        Task<bool> Create(T entity);
       // Task<bool> Create(IList<T> entityList);

        Task<bool> isExists(int id);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        Task<bool> Save();

        
        Task<bool> isExists(string id);

    }
}
