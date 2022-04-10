using LitJson;
using Mmcoy.Framework;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace MMORPG_AccountServer.Controllers.api
{
    public class RegisterController : ApiController
    {
        public MFReturnValue<int> Post([FromBody] string value)
        {
            JsonData jsonData = JsonMapper.ToObject(value);
            MFReturnValue<int> ret;
            string deviceIdentifier = jsonData["DeviceIdentifier"].ToString();
            string time = jsonData["Time"].ToString();
            string sign = jsonData["Sign"].ToString();
            if (!ControllerHelp.CheckSign(deviceIdentifier, time, sign))
            {
                ret = new MFReturnValue<int>();
                ret.HasError = true;
                ret.Message = "请求错误";
                return ret;
            }

            string username = jsonData["Username"].ToString();
            string pwd = jsonData["Pwd"].ToString();
            short channelId = (short)jsonData["ChannelId"];
            string deviceModel = jsonData["DeviceModel"].ToString();
            ret = AccountCacheModel.Instance.Register(username, pwd, channelId, deviceIdentifier, deviceModel);
            return ret;
        }
    }
}