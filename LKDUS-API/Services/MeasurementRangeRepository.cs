using LKDUS_API.Contracts;
using LKDUS_API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LKDUS_API.Services
{
    public class MeasurementRangeRepository : IMeasurementRangeRepository
    {
        private readonly ApplicationDbContext _db;

        public MeasurementRangeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(MeasurementRange entity)
        {
            await _db.MeasurementRanges.AddAsync(entity);
            return await Save();
        }

        public Task<bool> Create(IList<MeasurementRange> entityList)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(MeasurementRange entity)
        {
            _db.MeasurementRanges.Remove(entity);
            return await Save();

        }

        public async Task<IList<MeasurementRange>> FindAll()
        {
            return await _db.MeasurementRanges.ToListAsync();
        }

        public async Task<MeasurementRange> FindById(int id)
        {
            return await _db.MeasurementRanges.FindAsync(id);
        }

        public Task<MeasurementRange> FindById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> isExists(int id)
        {
            var isExist = await _db.MeasurementRanges.AnyAsync(m => m.Id == id);
            return isExist;
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

        public async  Task<bool> Update(MeasurementRange entity)
        {
             _db.MeasurementRanges.Update(entity);
            return await Save();
        }
    }
}
