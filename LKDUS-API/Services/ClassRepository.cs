using LKDUS_API.Contracts;
using LKDUS_API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LKDUS_API.Services
{
    public class ClasssRepository : IClasssRepository
    {
        private readonly ApplicationDbContext _db;

        public ClasssRepository(ApplicationDbContext db)
        {
            //dependency initialized through constructor
            _db = db;
        }

        public async Task<bool> Create(Classs entity)
        {
            await _db.Classes.AddAsync(entity);
            return await Save();

        }

        public Task<bool> Create(IList<Classs> entityList)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(Classs entity)
        {
            _db.Classes.Remove(entity);
            return await Save();
        }

        public async Task<IList<Classs>> FindAll()
        {
            var classes = await _db.Classes.ToListAsync();

            return classes;
        }

        public async Task<Classs> FindById(int id)
        {
            var classes = await _db.Classes.FindAsync(id);

            return classes;
        }

        public Task<Classs> FindById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> isExists(int id)
        {
            return await _db.Classes.AnyAsync(q => q.Id == id);
        }

        public Task<bool> isExists(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Classs entity)
        {
            _db.Classes.Update(entity);
            return await Save();

        }


    }
}
