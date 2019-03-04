using Microsoft.AspNetCore.Mvc;

namespace NA.Template.Web.Controllers
{
    [Route("api/")]
    [ApiController]
    public class RootController : ControllerBase
    {
        [HttpGet(Name = nameof(GetRoot))]
        public IActionResult GetRoot()
        {
            var response = new
            {
                href = Url.Link(nameof(GetRoot), null),
                ClientDivisions_GetClientDivisions = new { href = Url.Link(nameof(ClientDivisionController.GetClientDivisions), null) },
                ClientDivisions_GetById = new { href = Url.Link(nameof(ClientDivisionController.GetById), new { id=0}) },
                ClientDivisions_Create = new { href = Url.Link(nameof(ClientDivisionController.Post), null) },
                LineChart_GetWeatherForecast = new { href = Url.Link(nameof(LineChartController.GetWeatherForecast), null) }
            };

            return Ok(response);
        }
    }
}