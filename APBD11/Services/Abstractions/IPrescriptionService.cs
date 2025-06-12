using APBD11.Contracts.Requests;

namespace APBD11.Services.Abstractions;

public interface IPrescriptionService
{
    Task<int> CreatePrescriptionAsync(CreatePrescriptionDto dto);
    Task<PrescriptionDto?> GetPrescriptionAsync(int idPrescription);
}