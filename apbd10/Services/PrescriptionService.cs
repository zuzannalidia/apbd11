using apbd10.Data;
using apbd10.DTO;
using apbd10.Models;
using Microsoft.EntityFrameworkCore;

namespace apbd10.Services;

public class PrescriptionService : IPrescriptionService
    {
        private readonly DatabaseContext _context;

        public PrescriptionService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> CreatePrescriptionAsync(PrescriptionCreateDTO dto)
        {
            if (dto.DueDate < dto.Date)
            {
                return false;
            }

            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.FirstName == dto.Patient.FirstName &&
                                          p.LastName == dto.Patient.LastName &&
                                          p.Birthdate == dto.Patient.Birthdate);

            if (patient == null)
            {
                patient = new Patient
                {
                    FirstName = dto.Patient.FirstName,
                    LastName = dto.Patient.LastName,
                    Birthdate = dto.Patient.Birthdate
                };

                await _context.Patients.AddAsync(patient);
                await _context.SaveChangesAsync();
            }

            var medicamentIds = dto.Medicaments.Select(m => m.IdMedicament).ToList();
            var existingMedicaments = await _context.Medicaments
                .Where(m => medicamentIds.Contains(m.IdMedicament))
                .ToListAsync();

            if (existingMedicaments.Count != medicamentIds.Count)
            {
                return false;
            }

            var prescription = new Prescription
            {
                Date = dto.Date,
                DueDate = dto.DueDate,
                IdDoctor = dto.IdDoctor,
                IdPatient = patient.IdPatient,
                PrescriptionMedicaments = dto.Medicaments.Select(m => new PrescriptionMedicament
                {
                    IdMedicament = m.IdMedicament,
                    Dose = m.Dose,
                    Details = m.Details
                }).ToList()
            };

            await _context.Prescriptions.AddAsync(prescription);
            await _context.SaveChangesAsync();

            return true;
        }
    }
