using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            await _db.Employees.AsNoTracking().OrderBy(e => e.Numero).ToListAsync();

        public async Task<Employee?> GetByIdAsync(int id) =>
            await _db.Employees.FindAsync(id);

        public async Task AddAsync(Employee emp)
        {
            // Normalizar datos: si es indeterminado, quitar fechas de término/renovación
            if (emp.Indeterminado)
            {
                emp.FechaTermino = null;
                emp.Renovacion = null;
                emp.RenovacionTermino = null;
            }

            _db.Employees.Add(emp);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee emp)
        {
            if (emp.Indeterminado)
            {
                emp.FechaTermino = null;
                emp.Renovacion = null;
                emp.RenovacionTermino = null;
            }

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

        public async Task<List<Employee>> SearchAsync(string? q) =>
            await _db.Employees
                .Where(e => string.IsNullOrEmpty(q)
                    || (e.NombreCompleto != null && e.NombreCompleto.Contains(q))
                    || (e.Puesto != null && e.Puesto.Contains(q))
                    || (e.Oficina != null && e.Oficina.Contains(q)))
                .AsNoTracking()
                .OrderBy(e => e.Numero)
                .ToListAsync();
    }
}