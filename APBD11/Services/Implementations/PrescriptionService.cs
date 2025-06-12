using Microsoft.EntityFrameworkCore;
using APBD11.Contracts.Requests;
using APBD11.Data;
using APBD11.Services.Abstractions;
using APBD11.Mappers;

namespace APBD11.Services.Implementations;

public class PrescriptionService : IPrescriptionService
{
    private readonly AppDbContext _db;

    public PrescriptionService(AppDbContext db) => _db = db;

    public async Task<int> CreatePrescriptionAsync(CreatePrescriptionDto dto)
    {
        if (dto.Medicament == null || dto.Medicament.Count == 0)
            throw new ArgumentException("At least one medicament must be provided.");

        if (dto.Medicament.Count > 10)
            throw new ArgumentException("A prescription can include a maximum of 10 medicaments.");

        if (dto.DueDate < dto.Date)
            throw new ArgumentException("DueDate must be greater than or equal to Date.");

        var medIds = dto.Medicament.Select(m => m.IdMedicament).ToList();
        var existingMedIds = await _db.Medicaments
            .Where(m => medIds.Contains(m.IdMedicament))
            .Select(m => m.IdMedicament)
            .ToListAsync();
        var missing = medIds.Except(existingMedIds).ToList();
        if (missing.Any())
            throw new ArgumentException($"Medicament(s) not found: {string.Join(", ", missing)}");

        var patient = await _db.Patients
                          .FirstOrDefaultAsync(p => p.IdPatient == dto.Patient.IdPatient)
                      ?? dto.Patient.ToPatient();

        if (patient.IdPatient == 0)
        {
            _db.Patients.Add(patient);
            await _db.SaveChangesAsync();
        }

        var doctor = await _db.Doctors
            .FirstOrDefaultAsync(d => d.IdDoctor == dto.Doctor.IdDoctor);
        if (doctor == null)
            throw new ArgumentException($"Doctor with Id = {dto.Doctor.IdDoctor} not found.");

        var prescription = dto.ToEntity();
        prescription.IdPatient = patient.IdPatient;
        prescription.IdDoctor = doctor.IdDoctor;

        _db.Prescriptions.Add(prescription);
        await _db.SaveChangesAsync();

        return prescription.IdPrescription;
    }

    public async Task<PrescriptionDto?> GetPrescriptionAsync(int idPrescription)
    {
        var prescription = await _db.Prescriptions
            .Include(p => p.Patient)
            .Include(p => p.Doctor)
            .Include(p => p.PrescriptionMedicaments)
            .ThenInclude(pm => pm.Medicament)
            .FirstOrDefaultAsync(p => p.IdPrescription == idPrescription);

        if (prescription == null)
            return null;

        return prescription.ToDto();
    }
}