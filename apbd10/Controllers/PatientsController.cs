using Microsoft.AspNetCore.Mvc;
using apbd10.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace apbd10.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientDetails(int id)
        {
            var patientDetails = await _patientService.GetPatientDetailsAsync(id);

            if (patientDetails == null)
            {
                return NotFound();
            }

            return Ok(patientDetails);
        }
    }
}