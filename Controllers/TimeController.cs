using System;
using System.Web;
using System.Web.Http;
using System.Web.SessionState;

namespace MMORPG_AccountServer.Controllers
{
    [Route("time")]
    public class TimeController : ApiController
    {
        /// <summary>
        /// 返回当前时间戳，单位ms
        /// </summary>
        /// <returns></returns>
        public ResponseData<long> Get()
        {
            HttpSessionState session = HttpContext.Current.Session;
            var responseData = new ResponseData<long>(0, TimeHelper.NowTimestamp, null);
            return responseData;
        }
    }
}