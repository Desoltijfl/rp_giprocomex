using System;
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

        // Calculo centralizado de fechas (server-side)
        private void CalculateDates(Employee emp)
        {
            if (emp == null) return;

            if (emp.Indeterminado)
            {
                emp.FechaTermino = null;
                emp.Renovacion = null;
                emp.RenovacionTermino = null;
                return;
            }

            // Asegurar FechaIngreso válida; si no definida, fijar a hoy
            if (emp.FechaIngreso == default)
                emp.FechaIngreso = DateTime.Today;

            // Regla: FechaTermino = FechaIngreso + 90 días
            emp.FechaTermino = emp.FechaIngreso.AddDays(90);

            // Renovacion = FechaTermino (inicio de la renovación)
            emp.Renovacion = emp.FechaTermino;

            // RenovacionTermino = Renovacion + 90 días
            emp.RenovacionTermino = emp.Renovacion?.AddDays(90);
        }

        public async Task AddAsync(Employee emp)
        {
            // Calcula las fechas antes de guardar (defensivo)
            CalculateDates(emp);

            _db.Employees.Add(emp);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee emp)
        {
            // Recalcula las fechas antes de actualizar (defensivo)
            CalculateDates(emp);

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
                    || (e.NombreCompleto != null && EF.Functions.Like(e.NombreCompleto, $"%{q}%"))
                    || (e.Puesto != null && EF.Functions.Like(e.Puesto, $"%{q}%"))
                    || (e.Oficina != null && EF.Functions.Like(e.Oficina, $"%{q}%")))
                .AsNoTracking()
                .OrderBy(e => e.Numero)
                .ToListAsync();
    }
}