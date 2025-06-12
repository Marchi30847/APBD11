using APBD11.Contracts;
using APBD11.Models;

namespace APBD11.Mappers;

public static class DoctorMapper
{
    public static DoctorDto ToDto(this Doctor doctor)
    {
        return new DoctorDto
        {
            IdDoctor = doctor.IdDoctor,
            FirstName = doctor.FirstName,
            LastName = doctor.LastName,
            Email = doctor.Email
        };
    }

    public static Doctor ToEntity(this DoctorDto doctorDto)
    {
        return new Doctor
        {
            IdDoctor = doctorDto.IdDoctor,
            FirstName = doctorDto.FirstName,
            LastName = doctorDto.LastName,
            Email = doctorDto.Email
        };
    }
}