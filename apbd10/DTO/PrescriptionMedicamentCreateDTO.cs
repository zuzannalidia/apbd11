using System.ComponentModel.DataAnnotations;

namespace apbd10.DTO;

public class PrescriptionMedicamentCreateDTO
{
    [Required]
    public int IdMedicament { get; set; }

    [Required]
    public int Dose { get; set; }

    [MaxLength(100)]
    public string Details { get; set; }
}