using LKDUS_API.Contracts;
using LKDUS_API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LKDUS_API.Services
{
    public class MeasurementTypeRepository : IMeasurementTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public MeasurementTypeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(MeasurementType entity)
        {
            await _db.MeasurementsType.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(MeasurementType entity)
        {
            _db.MeasurementsType.Remove(entity);
            return await Save();

        }

        public async Task<IList<MeasurementType>> FindAll()
        {
            return await _db.MeasurementsType.ToListAsync();
        }

        public async Task<MeasurementType> FindById(int id)
        {
            return await _db.MeasurementsType.FindAsync(id);
        }

        public Task<MeasurementType> FindById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> isExists(int id)
        {
            var isExist = await _db.MeasurementsType.AnyAsync(m => m.Id == id);
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

        public async  Task<bool> Update(MeasurementType entity)
        {
             _db.MeasurementsType.Update(entity);
            return await Save();
        }
    }
}
