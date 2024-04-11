using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactApp1.Server.Data.Models;

namespace ReactApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Patient>>> GetAllValues()
        {
            var Patients = new List<Patient> {
                new  Patient{

                    Id=1,
                    Name="Max",
                    Surname="Musterman",
                    isRegistered = true,
                    age=22
                }
            };
            return Ok(Patients);
        }
    }
}
