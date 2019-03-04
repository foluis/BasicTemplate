using NA.Template.DataAccess.Interfaces;
using NA.Template.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace NA.Template.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientDivisionController : ControllerBase
    {
        private readonly IClientDivisionRepository _clientDivisionRepository;

        public ClientDivisionController(IClientDivisionRepository clientDivisionRepository)
        {
            _clientDivisionRepository = clientDivisionRepository;
        }
        
        [HttpGet (Name = nameof(GetClientDivisions))]
        public async Task<IActionResult> GetClientDivisions()
        {
            try
            {
                var clientDivision = await _clientDivisionRepository.GetAllClientDivision();

                if (clientDivision == null)
                {
                    return NotFound();
                }

                return Ok(clientDivision);
            }
            catch (Exception ex)
            {
                //_logger.LogError(e.ToString()); 
                return StatusCode(500);
            }
        }
        
        [HttpGet("{id}", Name = nameof(GetById))]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest("Missing Id");
                }

                var clientDivision = await _clientDivisionRepository.GetClientDivisionById(id);

                if (clientDivision == null)
                {
                    return NotFound();
                }

                return Ok(clientDivision);
            }
            catch (Exception ex)
            {
                //_logger.LogError(e.ToString()); 
                return StatusCode(500);
            }
        }

        // POST: api/ClientDivision
        [HttpPost (Name = nameof(Post))]
        public async Task<IActionResult> Post([FromBody] ClientDivision clientDivision)
        {
            try
            {
                var result = await _clientDivisionRepository.CreateClientDivision(clientDivision);
                
                return CreatedAtRoute(nameof(GetById), new { id = clientDivision.Id, name = clientDivision.Name }, clientDivision);
            }
            catch (Exception)
            {
                //_logger.LogError(e.ToString()); 
                return StatusCode(500);
            }
        }

        // PUT: api/ClientDivision/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
