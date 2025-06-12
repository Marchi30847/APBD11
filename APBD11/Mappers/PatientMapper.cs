using APBD11.Contracts;
using APBD11.Contracts.Responses;
using APBD11.Models;

namespace APBD11.Mappers;

public static class PatientMapper
{
    public static PatientDto ToDto(this Patient patient)
    {
        return new PatientDto {
            IdPatient = patient.IdPatient,
            LastName = patient.LastName,
            FirstName = patient.FirstName,
            Birthdate = patient.BirthDate,
        };
    }

    public static GetPatientDto ToGetPatientDto(this Patient patient)
    {
        return new GetPatientDto
        {
            IdPatient = patient.IdPatient,
            LastName = patient.LastName,
            FirstName = patient.FirstName,
            DateOfBirth = patient.BirthDate,
            Prescriptions = patient.Prescriptions
                .OrderBy(p => p.DueDate)
                .Select(p => p.ToDto())
                .ToList()
        };
    }
    
    public static Patient ToPatient(this PatientDto patientDto)
    {
        return new Patient
        {
            IdPatient = patientDto.IdPatient,
            LastName = patientDto.LastName,
            FirstName = patientDto.FirstName,
            BirthDate = patientDto.Birthdate
        };
    }
}