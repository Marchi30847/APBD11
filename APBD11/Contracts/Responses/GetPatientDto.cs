using APBD11.Contracts.Requests;

namespace APBD11.Contracts.Responses;

public class GetPatientDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    
    public ICollection<PrescriptionDto> Prescriptions { get; set; } = new List<PrescriptionDto>();
}