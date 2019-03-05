using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace NA.Template.Web.Controllers
{   
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [Authorize(Policy = "UserCanCreateSecurity")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Authorize(Policy = "UserCanCreateUserGeneral")]
        [HttpGet("[Action]")]
        public IEnumerable<string> Otro()
        {
            return new string[] { "otro", "otro" };
        }

        [Authorize(Roles = "Administrator")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//Funciona
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return $"value {id.ToString()}";
        }
        
        [HttpGet("[Action]")]
        public IEnumerable<string> SinNada()
        {
            return new string[] { "SinNada", "SinNada" };
        }
        
        [Authorize(Policy = "ClaimTypesPrueba")]
        [HttpGet("[Action]")]
        public IEnumerable<string> ElevatedRightsAdministrator()
        {
            return new string[] { "ElevatedRights Administrator", "ElevatedRights Administrator" };
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
