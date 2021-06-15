using EXE1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
 

namespace EXE1.Controllers
{
    public class EpisodesController : ApiController
    {
       // GET api/<controller>
        public IEnumerable<string> Get()
        {
            Episode Ep = new Episode();
            List<string> EpList = Ep.Get();
            return EpList;
        }


        public IEnumerable<Episode> Get(string nameTvshows)
        {
            Episode Ep = new Episode();
            List<Episode> filList = Ep.Get(nameTvshows);
            return filList;
        }

        // POST api/<controller>
        public int Post([FromBody] Preferences preferences)
        {   
            preferences.Episode.Insert();
            preferences.Insert();
            return 1;
        }
        [HttpPost]
        [Route("api/Episodes/Tv")]
        public int PostTv([FromBody] Tv tv)
        {
            tv.Insert();
            return 1;
        }
       
        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}