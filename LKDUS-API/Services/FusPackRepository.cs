using LKDUS_API.Contracts;
using LKDUS_API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LKDUS_API.Services
{
    public class FusPackRepository : IFusPackRepository
    {
        private readonly ApplicationDbContext _db;

        public FusPackRepository(ApplicationDbContext db)
        {
            //dependency initialized through constructor
            _db = db;
        }

        public Task<bool> Create(FusPack entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(FusPack entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<FusPack>> FindAll()
        {
            var packs = await _db.FusPack.ToListAsync();

            return packs;
        }

        public async Task<FusPack> FindById(int id)
        {
            var pack = await _db.FusPack.FindAsync(id);

            return pack;
        }

        public async Task<FusPack> FindById(string id)
        {
            var pack = await _db.FusPack.FindAsync(id);

            return pack;
        }

        public async Task<bool> isExists(int id)
        {
            return await _db.FusPack.AnyAsync(q => q.Id == id);
        }

        public Task<bool> isExists(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Save()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(FusPack entity)
        {
            throw new NotImplementedException();
        }
    }
}
