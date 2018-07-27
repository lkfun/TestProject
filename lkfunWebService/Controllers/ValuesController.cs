using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using lkfunWebService.Class;
using Newtonsoft.Json.Linq;

namespace lkfunWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("any")] //设置跨域处理的 代理
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public IList<Board> Get()
        {
            IList<Board> u=null;
            try
            {
                lk.MysqlHelperLk hp = new lk.MysqlHelperLk();
                DataTable dt = hp.GetData("select * from pvpa_board");
                u = lk.DataTableToObject.ConvertTo<Board>(dt);
            }
            catch (Exception)
            {

            }
            return u;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public Object Post([FromBody] Object value)
        {
            return value;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
