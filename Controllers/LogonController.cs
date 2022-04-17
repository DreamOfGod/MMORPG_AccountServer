using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MMORPG_AccountServer.Controllers
{
    [Route("logon")]
    public class LogonController : ApiController
    {
        public async Task<ResponseData<int>> Post()
        {
            var responseData = new ResponseData<int>();

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

            try
            {
                int id = await AccountCacheModel.Instance.Logon(username, pwd, deviceIdentifier, deviceModel);

                if (id >= 0)
                {
                    responseData.Code = 0;
                    responseData.Data = id;
                }
                else
                {
                    responseData.Code = 1;
                    responseData.Error = "用户名或密码错误";
                }
            }
            catch(Exception ex)
            {
                responseData.Code = 3;
                responseData.Error = ex.Message;
            }

            return responseData;
        }
    }
}