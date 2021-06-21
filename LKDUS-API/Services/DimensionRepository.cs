using LKDUS_API.Contracts;
using LKDUS_API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LKDUS_API.Services
{
    public class DimensionRepository : IDimensionRepository
    {
        private readonly ApplicationDbContext _db;

        public DimensionRepository(ApplicationDbContext db)
        {
            //dependency initialized through constructor
            _db = db;
        }

        public async Task<bool> Create(Pack entity)
        {
            await _db.Packs.AddAsync(entity);
            return await Save();

        }

        public Task<bool> Create(IList<Dimension> entityList)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Create(Dimension entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(Dimension entity)
        {
            _db.Dimensions.Remove(entity);
            return await Save();
        }

        public async Task<IList<Dimension>> FindAll()
        {
            var dimensions = await _db.Dimensions.ToListAsync();

            return dimensions;
        }

        public async Task<Dimension> FindById(int id)
        {
            var dmension = await _db.Dimensions.FindAsync(id);

            return dmension;
        }

        public Task<Pack> FindById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> isExists(int id)
        {
            return await _db.Dimensions.AnyAsync(q => q.Id == id);
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

        public async Task<bool> Update(Dimension entity)
        {
            _db.Dimensions.Update(entity);
            return await Save();

        }


    }
}
