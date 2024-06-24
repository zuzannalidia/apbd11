using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace apbd10.Models
{
    public class Patient
    {
        [Key]
        public int IdPatient { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        public DateTime Birthdate { get; set; }

        public ICollection<Prescription> Prescriptions { get; set; } = new HashSet<Prescription>();
    }
}