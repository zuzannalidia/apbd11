using System.ComponentModel.DataAnnotations;

namespace apbd10.Models
{
    public class PrescriptionMedicament
    {
        public int IdMedicament { get; set; }
        public Medicament Medicament { get; set; }

        public int IdPrescription { get; set; }
        public Prescription Prescription { get; set; }

        public int Dose { get; set; }

        [MaxLength(100)]
        public string Details { get; set; } = string.Empty;
    }
}