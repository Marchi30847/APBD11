using APBD11.Contracts.Responses;
using APBD11.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace APBD11.Controllers;

[ApiController]
[Route("api/patients")]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService) => _patientService = patientService;

    [HttpGet("{idPatient}")]
    [ProducesResponseType(typeof(GetPatientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPatientByIdAsync(int idPatient)
    {
        var dto = await _patientService.FindPatientByIdAsync(idPatient);
        if (dto is null)
            return NotFound(new { message = $"Patient with Id = {idPatient} not found." });
        
        return Ok(dto);
    }
}