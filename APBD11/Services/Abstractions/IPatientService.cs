using APBD11.Contracts.Responses;

namespace APBD11.Services.Abstractions;

public interface IPatientService
{
    Task<GetPatientDto?> FindPatientByIdAsync(int patientId);
}