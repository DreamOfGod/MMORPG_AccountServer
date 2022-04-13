using LitJson;
using Mmcoy.Framework;
using System;
using System.Web;
using System.Web.Http;

namespace MMORPG_AccountServer.Controllers
{
    [Route("logon")]
    public class LogonController : ApiController
    {
        public ResponseValue<AccountEntity> Post()
        {
            var responseValue = new ResponseValue<AccountEntity>();

            var request = HttpContext.Current.Request;
            string deviceIdentifier = request["DeviceIdentifier"]; 
            string time = request["Time"]; 
            string sign = request["Sign"]; 
            if (!ControllerHelper.CheckSign(deviceIdentifier, time, sign))
            {
                responseValue.Code = 2;
                responseValue.Error = "请求错误";
                return responseValue;
            }

            string username = request["Username"]; 
            string pwd = request["Pwd"]; 
            string deviceModel = request["DeviceModel"]; 
            AccountEntity entity = AccountCacheModel.Instance.Logon(username, pwd, deviceIdentifier, deviceModel);
            
            if (entity == null)
            {
                responseValue.Code = 1;
                responseValue.Error = "用户名或密码错误";
                
            }
            else
            {
                responseValue.Code = 0;
                responseValue.Value = entity;
            }

            return responseValue;
        }
    }
}