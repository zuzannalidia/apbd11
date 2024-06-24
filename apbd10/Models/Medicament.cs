using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace apbd10.Models
{
    public class Medicament
    {
        [Key]
        public int IdMedicament { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Description { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Type { get; set; } = string.Empty;

        public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = new HashSet<PrescriptionMedicament>();
    }
}