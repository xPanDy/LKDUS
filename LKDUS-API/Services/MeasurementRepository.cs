using LKDUS_API.Contracts;
using LKDUS_API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LKDUS_API.Services
{
    public class MeasurementRepository : IMeasurementRepository
    {
        private readonly ApplicationDbContext _db;

        public MeasurementRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Measurement entity)
        {
            await _db.Measurements.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Measurement entity)
        {
            _db.Measurements.Remove(entity);
            return await Save();

        }

        public async Task<IList<Measurement>> FindAll()
        {
            return await _db.Measurements.ToListAsync();
        }

        public async Task<Measurement> FindById(int id)
        {
            return await _db.Measurements.FindAsync(id);
        }

        public async Task<bool> isExists(int id)
        {
            var isExist = await _db.Measurements.AnyAsync(m => m.Id == id);
            return isExist;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async  Task<bool> Update(Measurement entity)
        {
             _db.Measurements.Update(entity);
            return await Save();
        }
    }
}
