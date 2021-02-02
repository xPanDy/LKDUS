using LKDUS_API.Contracts;
using LKDUS_API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LKDUS_API.Services
{
    public class PackRepository : IPackRepository
    {
        private readonly ApplicationDbContext _db;

        public PackRepository(ApplicationDbContext db)
        {
            //dependency initialized through constructor
            _db = db;
        }

        public async Task<bool> Create(Pack entity)
        {
            await _db.Packs.AddAsync(entity);
            return await Save();

        }

        public Task<bool> Create(IList<Pack> entityList)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(Pack entity)
        {
            _db.Packs.Remove(entity);
            return await Save();
        }

        public async Task<IList<Pack>> FindAll()
        {
            var packs = await _db.Packs.ToListAsync();

            return packs;
        }

        public async Task<Pack> FindById(int id)
        {
            var pack = await _db.Packs.FindAsync(id);

            return pack;
        }

        public Task<Pack> FindById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> isExists(int id)
        {
            return await _db.Packs.AnyAsync(q => q.Id == id);
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

        public async Task<bool> Update(Pack entity)
        {
            _db.Packs.Update(entity);
            return await Save();

        }


    }
}
