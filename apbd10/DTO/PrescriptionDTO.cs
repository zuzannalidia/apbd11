namespace apbd10.DTO;

public class PrescriptionDTO
{
    public int IdPrescription { get; set; }

    public DateTime Date { get; set; }

    public DateTime DueDate { get; set; }

    public List<PrescriptionMedicamentDTO> Medicaments { get; set; }

    public DoctorDTO Doctor { get; set; }
}