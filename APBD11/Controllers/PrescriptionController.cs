using APBD11.Contracts.Requests;
using APBD11.Contracts.Responses;
using APBD11.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace APBD11.Controllers;

[ApiController]
[Route("api/prescriptions")]
public class PrescriptionsController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;

    public PrescriptionsController(IPrescriptionService prescriptionService)
        => _prescriptionService = prescriptionService;

    [HttpPost]
    [ProducesResponseType(typeof(PrescriptionCreatedDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreatePrescriptionDto dto)
    {
        try
        {
            int id = await _prescriptionService.CreatePrescriptionAsync(dto);

            var response = new PrescriptionCreatedDto
            {
                IdPrescription = id
            };

            return Created($"/api/prescriptions/{id}", response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("{idPrescription}")]
    [ProducesResponseType(typeof(PrescriptionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int idPrescription)
    {
        var dto = await _prescriptionService.GetPrescriptionAsync(idPrescription);
        if (dto == null)
            return NotFound(new { message = $"Prescription with Id = {idPrescription} not found." });

        return Ok(dto);
    }
}