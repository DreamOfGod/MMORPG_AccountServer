using System;
using System.Web.Http;

namespace MMORPG_AccountServer.Controllers
{
    [Route("time")]
    public class TimeController : ApiController
    {
        public enum CodeType
        {
            Success
        }

        /// <summary>
        /// 返回当前时间戳，单位ms
        /// </summary>
        /// <returns></returns>
        public ResponseValue<CodeType, long> Get()
        {
            var responseValue = new ResponseValue<CodeType, long>();
            responseValue.Value = TimeHelper.NowTimestamp;
            return responseValue;
        }
    }
}