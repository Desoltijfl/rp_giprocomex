// name=rp_giprocomex.Services/EmployeeService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using rp_giprocomex.Data;
using rp_giprocomex.Core.Models;

namespace rp_giprocomex.Services
{
    public class EmployeeService
    {
        private readonly AppDbContext _db;
        public EmployeeService(AppDbContext db) => _db = db;

        public async Task<List<Employee>> GetAllAsync() =>
            await _db.Employees.AsNoTracking().OrderBy(e => e.Numero).ToListAsync();

        public async Task<Employee?> GetByIdAsync(int id) =>
            await _db.Employees.FindAsync(id);

        public async Task AddAsync(Employee emp)
        {
            _db.Employees.Add(emp);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee emp)
        {
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

        // Opcional: buscar por texto / filtro por oficina / proximos aniversarios
        public async Task<List<Employee>> SearchAsync(string? q) =>
            await _db.Employees
                .Where(e => string.IsNullOrEmpty(q)
                    || e.NombreCompleto.Contains(q)
                    || e.Puesto.Contains(q)
                    || e.Oficina.Contains(q))
                .AsNoTracking()
                .ToListAsync();
    }
}