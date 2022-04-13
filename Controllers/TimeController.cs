using System;
using System.Web.Http;

namespace MMORPG_AccountServer.Controllers
{
    [Route("time")]
    public class TimeController : ApiController
    {
        /// <summary>
        /// 返回当前时间戳，单位ms
        /// </summary>
        /// <returns></returns>
        public ResponseValue<long> Get()
        {
            var responseValue = new ResponseValue<long>(0, TimeHelper.NowTimestamp, null);
            return responseValue;
        }
    }
}