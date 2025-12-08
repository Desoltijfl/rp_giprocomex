// name=rp_giprocomex.Core/Models/Employee.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rp_giprocomex.Core.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        public int Numero { get; set; } // N°

        [Required, MaxLength(200)]
        public string Puesto { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Oficina { get; set; } = string.Empty;

        [MaxLength(150)]
        public string Empresa { get; set; } = string.Empty;

        [MaxLength(50)]
        public string RegistroPatronal { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Siroc { get; set; } = string.Empty;

        [Required, MaxLength(300)]
        public string NombreCompleto { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime FechaIngreso { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FechaTermino { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Renovacion { get; set; }

        [DataType(DataType.Date)]
        public DateTime? RenovacionTermino { get; set; }

        public bool Indeterminado { get; set; }

        // No mapeado a BD: calculado en tiempo de ejecución
        [NotMapped]
        public int Aniversario
        {
            get
            {
                var now = DateTime.UtcNow.Date;
                var years = now.Year - FechaIngreso.Year;
                if (FechaIngreso > now.AddYears(-years)) years--;
                return Math.Max(0, years);
            }
        }
    }
}