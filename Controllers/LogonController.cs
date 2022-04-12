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
        public enum CodeType
        {
            Success,
            WrongUsernamePassword,//用户名或密码错误
            BadRequest
        }

        public ResponseValue<CodeType, AccountEntity> Post()
        {
            ResponseValue<CodeType, AccountEntity> responseValue = new ResponseValue<CodeType, AccountEntity>();

            var request = HttpContext.Current.Request;
            string deviceIdentifier = request["DeviceIdentifier"]; 
            string time = request["Time"]; 
            string sign = request["Sign"]; 
            if (!ControllerHelper.CheckSign(deviceIdentifier, time, sign))
            {
                responseValue.Code = CodeType.BadRequest;
                responseValue.Error = "请求错误";
                return responseValue;
            }

            string username = request["Username"]; 
            string pwd = request["Pwd"]; 
            string deviceModel = request["DeviceModel"]; 
            AccountEntity entity = AccountCacheModel.Instance.Logon(username, pwd, deviceIdentifier, deviceModel);
            
            if (entity == null)
            {
                responseValue.Code = CodeType.WrongUsernamePassword;
                responseValue.Error = "用户名或密码错误";
                
            }
            else
            {
                responseValue.Code = CodeType.Success;
                responseValue.Value = entity;
            }

            return responseValue;
        }
    }
}