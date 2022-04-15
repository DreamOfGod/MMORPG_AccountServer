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
        public ResponseData<AccountEntity> Post()
        {
            var responseData = new ResponseData<AccountEntity>();

            var request = HttpContext.Current.Request;
            string deviceIdentifier = request["DeviceIdentifier"]; 
            string time = request["Time"]; 
            string sign = request["Sign"]; 
            if (!ControllerHelper.CheckSign(deviceIdentifier, time, sign))
            {
                responseData.Code = 2;
                responseData.Error = "请求错误";
                return responseData;
            }

            string username = request["Username"]; 
            string pwd = request["Pwd"]; 
            string deviceModel = request["DeviceModel"]; 
            AccountEntity entity = AccountCacheModel.Instance.Logon(username, pwd, deviceIdentifier, deviceModel);
            
            if (entity == null)
            {
                responseData.Code = 1;
                responseData.Error = "用户名或密码错误";
            }
            else
            {
                responseData.Code = 0;
                responseData.Data = entity;
            }

            return responseData;
        }
    }
}