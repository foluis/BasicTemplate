using NA.Template.DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace NA.Template.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttritionController : ControllerBase
    {
        private readonly IAttritionRepository _attritionRepository;

        public AttritionController(IAttritionRepository attritionRepository)
        {
            _attritionRepository = attritionRepository;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAppUser(int year)
        {
            try
            {
                if (year == 0)
                {
                    return BadRequest("Invalid year");
                }

                var usersList = await _attritionRepository.GetAttritionByYear(year);

                if (usersList == null)
                {
                    return NotFound();
                }

                return Ok(usersList);
            }
            catch (Exception)
            {
                //_logger.LogError(e.ToString()); 
                return StatusCode(500);
            }
        }
    }
}