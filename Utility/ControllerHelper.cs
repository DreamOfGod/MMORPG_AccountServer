using LitJson;
using Mmcoy.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MMORPG_AccountServer
{
    public class ControllerHelper
    {
        public static bool CheckSign(string deviceIdentifier, string timeStr, string sign) 
        {
            long time = long.Parse(timeStr);
            if (Math.Abs(TimeHelper.NowTimestamp - time) > 3000)
            {
                return false;
            }

            byte[] bytes = Encoding.Unicode.GetBytes(string.Format("{0}:{1}", deviceIdentifier, time));
            bytes = MD5.Create().ComputeHash(bytes);
            StringBuilder hexSb = new StringBuilder();
            foreach (byte b in bytes)
            {
                hexSb.AppendFormat("{0: X2}", b);
            }
            if (sign != hexSb.ToString())
            {
                return false;
            }

            return true;
        }

        public static bool CheckSign(JsonData jsonData)
        {
            return CheckSign(jsonData["DeviceIdentifier"].ToString(), jsonData["Time"].ToString(), jsonData["Sign"].ToString());
        }
    }
}