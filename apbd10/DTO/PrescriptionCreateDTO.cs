using System.ComponentModel.DataAnnotations;

namespace apbd10.DTO;

public class PrescriptionCreateDTO
{
    [Required]
    public DateTime Date { get; set; }

    [Required]
    public DateTime DueDate { get; set; }

    [Required]
    public int IdDoctor { get; set; }

    [Required]
    public PatientCreateDTO Patient { get; set; }

    [Required]
    [MaxLength(10, ErrorMessage = "A prescription can include up to 10 medicaments.")]
    public List<PrescriptionMedicamentCreateDTO> Medicaments { get; set; }
}