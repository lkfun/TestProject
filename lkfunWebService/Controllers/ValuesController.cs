using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using lkfunWebService.Class;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http;
using MidiSharp;
using MidiSharp.Events;

namespace lkfunWebService.Controllers
{
    [Produces("application/json")]
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
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult UploadFile()
        {
            try
            {
                var file = Request.Form.Files[0];
                string folderName = "Upload";
                string webRootPath = "C:/";
                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fullPath = Path.Combine(newPath, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    MidiSequence sequence;
                    using (Stream inputStream = System.IO.File.OpenRead(fullPath))
                    {
                        sequence = MidiSequence.Open(inputStream);
                    }

                    int index = -1;
                    for (int i = 0; i < sequence.Tracks.Count && index == -1; i++)
                    {
                        for (int j = 0; j < sequence.Tracks[i].Events.Count; j++)
                        {
                            if (sequence.Tracks[i].Events[j].ToString().Contains("0x51"))
                            {
                                index = i;
                            }
                        }
                    }

                    if (index > 0)
                    {
                        foreach (MidiEvent Event in sequence.Tracks[0].Events)
                        {
                            if (Event.ToString().Contains("0x2F"))
                            {
                                sequence.Tracks[0].Events.Remove(Event);
                                break;
                            }
                        }
                        sequence.Tracks[0].Events.AddRange(sequence.Tracks[index].Events);
                        sequence.Tracks.Remove(sequence.Tracks[index]);
                    }

                    using (Stream outputStream = System.IO.File.OpenWrite(fullPath))
                    {
                        sequence.Save(outputStream);
                    }
                    FileStream fs = new FileStream(fullPath, FileMode.Open);
                    return File(fs, "application/vnd.android.package-archive", fileName);
                }
                return null;
            }
            catch (System.Exception ex)
            {
                return null;
            }
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
