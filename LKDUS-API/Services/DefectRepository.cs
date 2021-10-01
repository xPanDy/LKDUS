using LKDUS_API.Contracts;
using LKDUS_API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LKDUS_API.Services
{
    public class DefectRepository : IDefectRepository
    {
        private readonly ApplicationDbContext _db;

        public DefectRepository(ApplicationDbContext db)
        {
            //dependency initialized through constructor
            _db = db;
        }

        public async Task<bool> Create(Defect entity)
        {
            await _db.Defects.AddAsync(entity);
            return await Save();

        }

        public Task<bool> Create(IList<Defect> entityList)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(Defect entity)
        {
            _db.Defects.Remove(entity);
            return await Save();
        }

        public async Task<IList<Defect>> FindAll()
        {
            var defects = await _db.Defects.ToListAsync();

            return defects;
        }

        public async Task<Defect> FindById(int id)
        {
            var defect = await _db.Defects.FindAsync(id);

            return defect;
        }

        public Task<Defect> FindById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> isExists(int id)
        {
            return await _db.Defects.AnyAsync(q => q.Id == id);
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

        public async Task<bool> Update(Defect entity)
        {
            _db.Defects.Update(entity);
            return await Save();

        }


    }
}
