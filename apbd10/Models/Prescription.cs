using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace apbd10.Models
{
    public class Prescription
    {
        [Key]
        public int IdPrescription { get; set; }

        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }

        public int IdDoctor { get; set; }
        public Doctor Doctor { get; set; }

        public int IdPatient { get; set; }
        public Patient Patient { get; set; }

        public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = new HashSet<PrescriptionMedicament>();
    }
}