using LitJson;
using MMORPG_AccountServer.DBModel;
using MMORPG_AccountServer.Entity;
using System.Web.Http;

namespace MMORPG_AccountServer.Controllers.api
{
    public class RegisterController : ApiController
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Post([FromBody] string value)
        {
            AccountEntity account = JsonMapper.ToObject<AccountEntity>(value);
            return AccountDBModel.Instance.Register(account);
        }
    }
}