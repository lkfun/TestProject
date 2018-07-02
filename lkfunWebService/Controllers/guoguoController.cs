using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace lkfunWebService.Controllers
{
    [Route("api/[controller]")]
    [Route("[controller]")]
    [EnableCors("any")] //设置跨域处理的 代理
    public class GuoguoController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public double Get(Wci wci)
        {
            return wci.Getwci();
        }

        // GET api/<controller>/5
        [HttpGet("{str}")]
        public string Get(string str)
        {
            return str;
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
