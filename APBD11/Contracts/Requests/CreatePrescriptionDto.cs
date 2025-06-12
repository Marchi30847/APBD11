namespace APBD11.Contracts.Requests;

public class CreatePrescriptionDto
{
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }

    public PatientDto Patient { get; set; } = null!;
    public DoctorDto Doctor { get; set; } = null!;
    public ICollection<MedicamentDto> Medicament { get; set; } = new List<MedicamentDto>();
}