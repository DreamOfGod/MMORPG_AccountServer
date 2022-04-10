using Mmcoy.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public partial class AccountCacheModel
{
    public MFReturnValue<int> Register(string username, string pwd, short channelId, string deviceIdentifier, string deviceModel)
    {
        return DBModel.Register(username, pwd, channelId, deviceIdentifier, deviceModel);
    }

    public AccountEntity Logon(string username, string pwd, string deviceIdentifier, string deviceModel)
    {
        return DBModel.Logon(username, pwd, deviceIdentifier, deviceModel);
    }
}