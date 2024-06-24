using apbd10.DTO;

namespace apbd10.Services;

public interface IPatientService
{
    Task<PatientDetailsDTO> GetPatientDetailsAsync(int id);
}
