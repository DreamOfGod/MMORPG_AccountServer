using LitJson;
using Mmcoy.Framework;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;

namespace MMORPG_AccountServer.Controllers
{
    [Route("register")]
    public class RegisterController : ApiController
    {
        public enum CodeType
        {
            Success, DuplicateUsername, BadRequest
        }

        public ResponseValue<AccountEntity> Post()
        {
            var responseVal = new ResponseValue<AccountEntity>();

            HttpRequest request = HttpContext.Current.Request;
            string deviceIdentifier = request["DeviceIdentifier"];
            string time = request["Time"]; 
            string sign = request["Sign"]; 
            if (!ControllerHelper.CheckSign(deviceIdentifier, time, sign))
            {
                responseVal.Code = 2;
                responseVal.Error = "请求错误";
                return responseVal;
            }

            string username = request["Username"];
            string pwd = request["Pwd"];
            short channelId = short.Parse(HttpContext.Current.Request["ChannelId"]);
            string deviceModel = request["DeviceModel"];
            AccountEntity entity = AccountCacheModel.Instance.Register(username, pwd, channelId, deviceIdentifier, deviceModel);
            if(entity != null)
            {
                responseVal.Code = 0;
                responseVal.Value = entity;
            }
            else
            {
                responseVal.Code = 1;
                responseVal.Error = "用户名重复";
            }

            return responseVal;
        }
    }
}