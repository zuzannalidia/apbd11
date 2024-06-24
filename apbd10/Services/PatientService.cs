using apbd10.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using apbd10.DTO;

namespace apbd10.Services
{
    public class PatientService : IPatientService
    {
        private readonly DatabaseContext _context;

        public PatientService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<PatientDetailsDTO> GetPatientDetailsAsync(int id)
        {
            var patient = await _context.Patients
                .Include(p => p.Prescriptions)
                .ThenInclude(pr => pr.PrescriptionMedicaments)
                .ThenInclude(pm => pm.Medicament)
                .Include(p => p.Prescriptions)
                .ThenInclude(pr => pr.Doctor)
                .FirstOrDefaultAsync(p => p.IdPatient == id);

            if (patient == null)
            {
                return null;
            }

            var patientDetails = new PatientDetailsDTO
            {
                IdPatient = patient.IdPatient,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Birthdate = patient.Birthdate,
                Prescriptions = patient.Prescriptions
                    .OrderBy(pr => pr.DueDate)
                    .Select(pr => new PrescriptionDTO
                    {
                        IdPrescription = pr.IdPrescription,
                        Date = pr.Date,
                        DueDate = pr.DueDate,
                        Medicaments = pr.PrescriptionMedicaments.Select(pm => new PrescriptionMedicamentDTO
                        {
                            IdMedicament = pm.IdMedicament,
                            Name = pm.Medicament.Name,
                            Dose = pm.Dose,
                            Description = pm.Medicament.Description
                        }).ToList(),
                        Doctor = new DoctorDTO
                        {
                            IdDoctor = pr.Doctor.IdDoctor,
                            FirstName = pr.Doctor.FirstName,
                            LastName = pr.Doctor.LastName
                        }
                    }).ToList()
            };

            return patientDetails;
        }
    }
}
