using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Google.Protobuf.WellKnownTypes;
using CareerService.Grpc;

namespace ApiGateway.Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CareerServiceController : ControllerBase
    {
        private readonly CareerService.Grpc.CareerServiceClient _careerServiceClient;  

        public CareerServiceController(CareerService.Grpc.CareerServiceClient careerServiceClient)
        {
            _careerServiceClient = careerServiceClient;
        }

        // Obtener todas las carreras
        [HttpGet("careers")]
        public async Task<IActionResult> GetAllCareers()
        {
            var request = new Empty(); // Petición vacía (sin parámetros)
            var response = await _careerServiceClient.GetAllCareersAsync(request);
            return Ok(response.Careers); // Devuelve la lista de carreras
        }

        // Obtener todos los ramos
        [HttpGet("subjects")]
        public async Task<IActionResult> GetAllSubjects()
        {
            var request = new Empty(); // Petición vacía (sin parámetros)
            var response = await _careerServiceClient.GetAllSubjectsAsync(request);
            return Ok(response.Subjects); // Devuelve la lista de asignaturas
        }

        // Obtener todas las relaciones entre ramos
        [HttpGet("subject-relationships")]
        public async Task<IActionResult> GetAllSubjectRelationships()
        {
            var request = new Empty(); // Petición vacía (sin parámetros)
            var response = await _careerServiceClient.GetAllSubjectRelationshipsAsync(request);
            return Ok(response.SubjectRelationships); // Devuelve la lista de relaciones entre asignaturas
        }
    }
}
