using APBD11.Contracts;
using APBD11.Models;

namespace APBD11.Mappers;

public static class MedicamentMapper
{
    public static MedicamentDto ToDto(this PrescriptionMedicament prescriptionMedicament)
    {
        return new MedicamentDto
        {
            IdMedicament = prescriptionMedicament.IdMedicament,
            Dose = prescriptionMedicament.Dose,
            Description = prescriptionMedicament.Medicament.Description
        };
    }
}