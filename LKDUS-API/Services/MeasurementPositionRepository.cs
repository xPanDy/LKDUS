using LKDUS_API.Contracts;
using LKDUS_API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LKDUS_API.Services
{
    public class MeasurementPositionRepository : IMeasurementPositionRepository
    {
        private readonly ApplicationDbContext _db;

        public MeasurementPositionRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(MeasurementPosition entity)
        {
            await _db.MeasurementPositions.AddAsync(entity);
            return await Save();
        }

        public Task<bool> Create(IList<MeasurementPosition> entityList)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(MeasurementPosition entity)
        {
            _db.MeasurementPositions.Remove(entity);
            return await Save();

        }

        public async Task<IList<MeasurementPosition>> FindAll()
        {
            return await _db.MeasurementPositions.ToListAsync();
        }

        public async Task<MeasurementPosition> FindById(int id)
        {
            return await _db.MeasurementPositions.FindAsync(id);
        }

        public async Task<MeasurementPosition> FindById(string id)
        {
            return await _db.MeasurementPositions.FindAsync(id);
        }

        public async Task<bool> isExists(int id)
        {
            var isExist = await _db.MeasurementPositions.AnyAsync(m => m.Id == id);
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

        public async  Task<bool> Update(MeasurementPosition entity)
        {
             _db.MeasurementPositions.Update(entity);
            return await Save();
        }
    }
}
