using Mmcoy.Framework;
using MMORPG_AccountServer.Bean.Logon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

public partial class AccountCacheModel
{
    public async Task<int> Register(string username, string pwd, short channelId, string deviceIdentifier, string deviceModel)
    {
        return await DBModel.Register(username, pwd, channelId, deviceIdentifier, deviceModel);
    }

    public async Task<AccountBean> Logon(string username, string pwd, string deviceIdentifier, string deviceModel)
    {
        return await DBModel.Logon(username, pwd, deviceIdentifier, deviceModel);
    }
}