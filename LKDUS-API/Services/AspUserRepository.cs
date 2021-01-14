using LKDUS_API.Contracts;
using LKDUS_API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LKDUS_API.Services
{
  /*  public class AspUserRepository : IAspUserRepository
    {
        private readonly ApplicationDbContext _db;

        public AspUserRepository(ApplicationDbContext db)
        {
            //dependency initialized through constructor
            _db = db;
        }

        public async Task<bool> Create(AspUser entity)
        {
            await _db.AspUsers.AddAsync(entity);
            return await Save();

        }

        public async Task<bool> Delete(AspUser entity)
        {
            _db.AspUsers.Remove(entity);
            return await Save();
        }

        public async Task<IList<AspUser>> FindAll()
        {
            var users = await _db.AspUsers.ToListAsync();

            return users;
        }

        public async Task<AspUser> FindById(int id)
        {
            var user = await _db.AspUsers.FindAsync(id);

            return user;
        }

        public Task<AspUser> FindById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> isExists(int id)
        {
            return await _db.AspUsers.AnyAsync(q => q.Id == id.ToString());
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

        public async Task<bool> Update(AspUser entity)
        {
            _db.AspUsers.Update(entity);
            return await Save();

        }


    }*/
}
