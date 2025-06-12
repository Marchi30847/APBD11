using APBD11.Contracts.Responses;
using APBD11.Data;
using APBD11.Mappers;
using APBD11.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace APBD11.Services.Implementations;

public class PatientService : IPatientService
{
    private readonly AppDbContext _db;

    public PatientService(AppDbContext db) => _db = db;

    public async Task<GetPatientDto?> FindPatientByIdAsync(int patientId)
    {
        var patient = await _db.Patients
            .Include(p => p.Prescriptions)
            .ThenInclude(r => r.Doctor)
            .Include(p => p.Prescriptions)
            .ThenInclude(r => r.PrescriptionMedicaments)
            .ThenInclude(pm => pm.Medicament)
            .FirstOrDefaultAsync(p => p.IdPatient == patientId);

        if (patient == null)
            return null;

        return patient.ToGetPatientDto();
    }
}