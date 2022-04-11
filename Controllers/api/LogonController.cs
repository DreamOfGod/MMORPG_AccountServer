using LitJson;
using Mmcoy.Framework;
using System;
using System.Web.Http;

namespace MMORPG_AccountServer.Controllers.api
{
    public class LogonController : ApiController
    {
        public MFReturnValue<int> Post([FromBody] string value)//POST表单数据只能有一项，而且key必须为空字符串，FromBody参数才能接收。否则会是null
        {
            JsonData jsonData = JsonMapper.ToObject(value);
            MFReturnValue<int> ret = new MFReturnValue<int>();
            string deviceIdentifier = jsonData["DeviceIdentifier"].ToString();
            string time = jsonData["Time"].ToString();
            string sign = jsonData["Sign"].ToString();
            if (!ControllerHelp.CheckSign(deviceIdentifier, time, sign))
            {
                ret.HasError = true;
                ret.Message = "请求错误";
                return ret;
            }

            string username = jsonData["Username"].ToString();
            string pwd = jsonData["Pwd"].ToString();
            string deviceModel = jsonData["DeviceModel"].ToString();
            AccountEntity entity = AccountCacheModel.Instance.Logon(username, pwd, deviceIdentifier, deviceModel);
            
            if (entity != null)
            {
                ret.Value = (int)entity.Id;
            }
            else
            {
                ret.HasError = true;
                ret.Message = "账户不存在";
            }

            return ret;
        }
    }
}