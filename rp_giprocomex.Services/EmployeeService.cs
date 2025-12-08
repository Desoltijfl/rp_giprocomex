using Microsoft.EntityFrameworkCore;
using rp_giprocomex.Core.Models;
using rp_giprocomex.Data;

namespace rp_giprocomex.Services
{
    public class EmployeeService
    {
        private readonly AppDbContext _db;
        public EmployeeService(AppDbContext db) => _db = db;

        public async Task<List<Employee>> GetAllAsync() =>
            await _db.Employees.AsNoTracking().OrderBy(e => e.LastName).ToListAsync();

        public async Task<Employee?> GetByIdAsync(int id) =>
            await _db.Employees.FindAsync(id);

        public async Task<Employee> CreateAsync(Employee emp)
        {
            _db.Employees.Add(emp);
            await _db.SaveChangesAsync();
            return emp;
        }

        public async Task UpdateAsync(Employee emp)
        {
            emp.UpdatedAt = DateTime.UtcNow;
            _db.Employees.Update(emp);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var e = await _db.Employees.FindAsync(id);
            if (e != null)
            {
                _db.Employees.Remove(e);
                await _db.SaveChangesAsync();
            }
        }
    }
}