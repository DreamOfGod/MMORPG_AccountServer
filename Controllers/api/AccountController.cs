using LitJson;
using MMORPG_AccountServer.DBModel;
using MMORPG_AccountServer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MMORPG_AccountServer.Controllers.api
{
    public class AccountController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public AccountEntity Get(int id)
        {
            return AccountDBModel.Instance.Get(id);
        }

        // POST api/<controller>
        public string Post([FromBody] string value)
        {
            return value + " post resp";
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