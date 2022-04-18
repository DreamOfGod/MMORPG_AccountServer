using MMORPG_AccountServer.Bean.Logon;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.SessionState;

namespace MMORPG_AccountServer.Controllers
{
    public class LogonController : ApiController
    {
        [HttpPost][Route("register")]
        public async Task<ResponseData<int>> Register()
        {
            var responseData = new ResponseData<int>();

            HttpRequest request = HttpContext.Current.Request;
            string deviceIdentifier = request["DeviceIdentifier"];
            string time = request["Time"];
            string sign = request["Sign"];
            if (!ControllerHelper.CheckSign(deviceIdentifier, time, sign))
            {
                responseData.Code = 3;
                responseData.Error = "请求错误";
                return responseData;
            }

            string username = request["Username"];
            string pwd = request["Pwd"];
            short channelId = short.Parse(HttpContext.Current.Request["ChannelId"]);
            string deviceModel = request["DeviceModel"];

            try
            {
                int id = await AccountCacheModel.Instance.Register(username, pwd, channelId, deviceIdentifier, deviceModel);
                if (id >= 0)
                {
                    responseData.Code = 0;
                    responseData.Data = id;
                }
                else if (id == -1)
                {
                    responseData.Code = 1;
                    responseData.Error = "用户名重复";
                }
                else if (id == -2)
                {
                    responseData.Code = 2;
                    responseData.Error = "注册失败";
                }
            }
            catch (Exception ex)
            {
                responseData.Code = 4;
                responseData.Error = ex.Message;
            }


            return responseData;
        }

        [HttpPost][Route("logon")]
        public async Task<ResponseData<AccountBean>> Logon()
        {
            var responseData = new ResponseData<AccountBean>();

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
                HttpSessionState session = HttpContext.Current.Session;
                var account = await AccountCacheModel.Instance.Logon(username, pwd, deviceIdentifier, deviceModel);

                if (account != null)
                {
                    HttpContext.Current.Session.Add(SessionKey.AccountID, account.Id);
                    responseData.Code = 0;
                    responseData.Data = account;
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