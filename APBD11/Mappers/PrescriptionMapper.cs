using APBD11.Contracts.Requests;
using APBD11.Models;

namespace APBD11.Mappers;

public static class PrescriptionMapper
{
    public static Prescription ToEntity(this CreatePrescriptionDto dto)
    {
        var prescription = new Prescription
        {
            Date = dto.Date,
            DueDate = dto.DueDate,
            IdPatient = dto.Patient.IdPatient,
            IdDoctor = dto.Doctor.IdDoctor,
            PrescriptionMedicaments = dto.Medicament
                .Select(m => new PrescriptionMedicament
                {
                    IdMedicament = m.IdMedicament,
                    Dose = m.Dose,
                    Details = m.Description
                })
                .ToList()
        };

        return prescription;
    }

    public static PrescriptionDto ToDto(this Prescription prescription)
    {
        return new PrescriptionDto
        {
            IdPrescription = prescription.IdPrescription,
            Date = prescription.Date,
            DueDate = prescription.DueDate,
            Patient = prescription.Patient.ToDto(),
            Doctor = prescription.Doctor.ToDto(),
            Medicaments = prescription.PrescriptionMedicaments
                .Select(pm => pm.ToDto())
                .ToList()
        };
    }
}