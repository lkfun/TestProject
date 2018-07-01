using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace lkfunWebService.Controllers
{
    [Route("[controller]")]
    public class Md5Controller : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "你好" };
        }

        // GET api/<controller>/5
        [HttpGet("{inText}")]
        public Md5 Get(string inText)
        {
            return new Md5(inText);
        }
        // GET api/<controller>/5
        [HttpGet("{inText}/{type}")]
        public Md5 Get(string inText,string type)
        {
            return new Md5(inText, type);
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
