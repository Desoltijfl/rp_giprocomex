using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rp_giprocomex.Core.Models
{
    [Table("Employees")]
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        public int Numero { get; set; } // N°

        [MaxLength(200)]
        public string? Puesto { get; set; }

        [MaxLength(200)]
        public string? Oficina { get; set; }

        [MaxLength(150)]
        public string? Empresa { get; set; }

        [MaxLength(50)]
        public string? RegistroPatronal { get; set; }

        [MaxLength(50)]
        public string? Siroc { get; set; }

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
    }
}