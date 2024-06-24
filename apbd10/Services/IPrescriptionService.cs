using apbd10.DTO;

namespace apbd10.Services;

public interface IPrescriptionService
{
    Task<bool> CreatePrescriptionAsync(PrescriptionCreateDTO dto);
}
