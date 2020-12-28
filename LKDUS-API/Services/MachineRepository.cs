using LKDUS_API.Contracts;
using LKDUS_API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LKDUS_API.Services
{
    public class MachineRepository : IMachineRepository
    {
        private readonly ApplicationDbContext _db;

        public MachineRepository(ApplicationDbContext db)
        {
            //dependency initialized through constructor
            _db = db;
        }

        public async Task<bool> Create(Machine entity)
        {
            await _db.Machines.AddAsync(entity);
            return await Save();

        }

        public async Task<bool> Delete(Machine entity)
        {
            _db.Machines.Remove(entity);
            return await Save();
        }

        public async Task<IList<Machine>> FindAll()
        {
            var machines = await _db.Machines.ToListAsync();

            return machines;
        }

        public async Task<Machine> FindById(int id)
        {
            var machine = await _db.Machines.FindAsync(id);

            return machine;
        }

        public Task<Machine> FindById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> isExists(int id)
        {
            return await _db.Machines.AnyAsync(q => q.Id == id);
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

        public async Task<bool> Update(Machine entity)
        {
            _db.Machines.Update(entity);
            return await Save();

        }


    }
}
