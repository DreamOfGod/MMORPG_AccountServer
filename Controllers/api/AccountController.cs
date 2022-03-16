using LitJson;
using MMORPG_AccountServer.DBModel;
using MMORPG_AccountServer.Entity;
using System.Web.Http;

namespace MMORPG_AccountServer.Controllers.api
{
    public class AccountController : ApiController
    {
        // GET api/<controller>/5
        public AccountEntity Get(int id)
        {
            return AccountDBModel.Instance.Get(id);
        }

        // POST api/<controller>
        public string Post([FromBody] string value)//POST表单数据只能有一项，而且key必须为空字符串，FromBody参数才能接收。否则会是null
        {
            return value + " post resp";
        }
    }
}