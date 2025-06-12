namespace APBD11.Contracts.Requests;

public class PrescriptionDto
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }

    public PatientDto Patient { get; set; } = null!;
    public DoctorDto Doctor { get; set; } = null!;
    public ICollection<MedicamentDto> Medicaments { get; set; } = new List<MedicamentDto>();
}