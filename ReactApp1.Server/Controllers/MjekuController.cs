﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data;
using ReactApp1.Server.Data.Models;
using ReactApp1.Server.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MjekuController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MjekuController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Mjeku
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MjekuDTO>>> GetAllMjeket()
        {
            var mjeket = await _context.Mjeket
                .Include(m => m.User)           // Include related User data
                .Select(m => new MjekuDTO
                {
                    Id = m.Id,
                    Specializimi = m.Specializimi,
                    NumriLicences = m.NumriLicences,
                })
                .ToListAsync();

            return Ok(mjeket);
        }

        // GET: api/Mjeket/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MjekuDTO>> GetMjeku(string id)
        {
            var mjeku = await _context.Mjeket
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id.ToString() == id);

            if (mjeku == null)
            {
                return NotFound();
            }

            var mjekuDto = new MjekuDTO
            {
                Id = mjeku.Id,
                Specializimi = mjeku.Specializimi,
                NumriLicences = mjeku.NumriLicences,
            };

            return Ok(mjekuDto);
        }

        // PUT: api/Mjeket/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMjeku(string id, MjekuDTO mjekuDto)
        {

            

            if (id != mjekuDto.Id)
            {
                return BadRequest();
            }

            var mjeku = await _context.Mjeket.FindAsync(id);
            if (mjeku == null)
            {
                return NotFound();
            }

            mjeku.Id = mjekuDto.Id;
            mjeku.Specializimi = mjekuDto.Specializimi;
            mjeku.NumriLicences = mjekuDto.NumriLicences;

            _context.Entry(mjeku).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MjekuExists(int.Parse(id)))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Mjeket
        [HttpPost]
        public async Task<ActionResult<MjekuDTO>> PostMjeku(MjekuDTO mjekuDto)
        {

            var mjeku = new Mjeku
            {
                Id = mjekuDto.Id,
                Specializimi = mjekuDto.Specializimi,
                NumriLicences = mjekuDto.NumriLicences,
            };

            _context.Mjeket.Add(mjeku);
            await _context.SaveChangesAsync();


            return CreatedAtAction(nameof(GetMjeku), new { id = mjeku.Id }, mjekuDto);
        }

        // DELETE: api/Mjeket/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMjeku(string id)
        {
            var mjeku = await _context.Mjeket.FindAsync(id);
            if (mjeku == null)
            {
                return NotFound();
            }

            _context.Mjeket.Remove(mjeku);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MjekuExists(int id)
        {
            return _context.Mjeket.Any(e => e.Id.Equals(id));
        }


        [HttpPost("Regjistro-Pacientin")]
        [Authorize(Roles="Doctor")]
        public async Task<IActionResult> RegisterSimplePatient([FromBody] PacientDTO dto)
        {
            var pacienti = new Pacienti
            {
                Id = dto.Id,
                Name = dto.Name,
                Surname = dto.Surname,
                Ditelindja = dto.Ditelindja
            };

            _context.Pacientet.Add(pacienti);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Patient registered successfully!", PatientId = pacienti.Id });
        }



        [HttpGet("Get-Pacientat")]
        [Authorize(Roles="Doctor")]
        public async Task<IActionResult> GetPatients()
        {
            var patients = await _context.Pacientet
                .Select(p => new
                {
                    Id = p.Id,
                    FullName = $"{p.Name} {p.Surname}"  
                })
                .ToListAsync();

            return Ok(patients);
        }




        
        [HttpPost("Shto-Detajet-Pacientit")]
        [Authorize(Roles="Doctor")]
        public async Task<IActionResult> AddPatientDetails([FromBody] PatientDetailsDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

                      var pacienti = await _context.Pacientet
              .Include(p => p.Terminet)
              .Include(p => p.Historiks)
              .Include(p => p.DhomaPacienteve)
              .FirstOrDefaultAsync(p => p.Id == request.Patient.Id); 


            if (pacienti == null)
            {
                return NotFound(new { Message = "Patient not found." });
            }


            foreach (var historiku in request.Historiku)
            {
                var historikuEntity = new Historiku
                {
                    Id = historiku.Id,
                    MjekuId = historiku.MjekuId,
                    PacientId = pacienti.Id,
                    Data = historiku.Data,
                    Anamneza_Statusi = historiku.Anamneza_Statusi,
                    Ekzaminimi = historiku.Ekzaminimi,
                    Diagnoza = historiku.Diagnoza
                };
                _context.Historiks.Add(historikuEntity);
            }

            if (request.DhomaPacienteve != null)
            {
                foreach (var dhoma in request.DhomaPacienteve)
                {
                    var dhomaPacientiEntity = new DhomaPacientit
                    {
                        Id = dhoma.Id,
                        CheckInDate = dhoma.CheckInDate,
                        CheckOutDate = dhoma.CheckOutDate,
                        PacientId = pacienti.Id
                    };
                    _context.DhomaPacienteve.Add(dhomaPacientiEntity); 
                }
            }



            await _context.SaveChangesAsync();

            return Ok(new { Message = "Patient details updated successfully!", PatientId = pacienti.Id });
        }


        
       [HttpPost("Shto-Faturen")]
       [Authorize(Roles="Doctor")]
       public async Task<IActionResult> AddInvoice([FromBody] FaturaDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
        
            var pacienti = await _context.Pacientet
                .FirstOrDefaultAsync(p => p.Id == request.PacientId);
        
            if (pacienti == null)
            {
                return NotFound(new { Message = "Patient not found." });
            }

            var fatura = new Fatura
            {
                Id = request.Id,
                SherbimiId = request.SherbimiId,
                Shuma = request.Shuma,
                Data = request.Data,
                Paguar = request.Paguar,
                PacientId = pacienti.Id
            };
        
            _context.Faturat.Add(fatura);
            await _context.SaveChangesAsync();
        
            return Ok(new { Message = "Invoice added successfully!", InvoiceId = fatura.Id });
        }

        [HttpGet("CountAllPatients")]
        [Authorize(Roles = "Doctor")] 
        public async Task<IActionResult> CountPatients()
        {
            try
            {
                var totalPatientCount = await _context.Pacientet.CountAsync();
                return Ok(new { TotalPatientCount = totalPatientCount });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "An error occurred while counting all patients.",
                    Error = ex.Message
                });
            }
        }


    }
}
