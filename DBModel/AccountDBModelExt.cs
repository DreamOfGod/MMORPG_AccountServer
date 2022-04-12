using Mmcoy.Framework;
using Mmcoy.Framework.AbstractBase;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public partial class AccountDBModel
{
    public AccountEntity Register(string username, string pwd, short channelId, string deviceIdentifier, string deviceModel)
    {
        using (SqlConnection conn = new SqlConnection(DBConn.MMORPG_Account))
        {
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction();
            List<AccountEntity> lst = GetListWithTran(TableName, "Id", string.Format("Username='{0}'", username), trans: trans, isAutoStatus: false);
            if(lst == null || lst.Count == 0)
            {
                AccountEntity entity = new AccountEntity();
                entity.Status = EnumEntityStatus.Released;
                entity.Username = username;
                entity.Pwd = MFEncryptUtil.Md5(pwd);
                entity.ChannelId = channelId;
                entity.LastLogonServerTime = DateTime.Now;
                entity.CreateTime = DateTime.Now;
                entity.UpdateTime = DateTime.Now;
                entity.DeviceIdentifier = deviceIdentifier;
                entity.DeviceModel = deviceModel;

                MFReturnValue<object> result = Create(entity);
                if(result.HasError)
                {
                    trans.Rollback();
                    return null;
                }
                else
                {
                    trans.Commit();
                    entity.Id = (int)result.OutputValues["Id"];
                    return entity;
                }
            }
            else
            {
                return null;
            }
        }
    }

    public AccountEntity Logon(string username, string pwd, string deviceIdentifier, string deviceModel)
    {
        string condition = string.Format("Username='{0}' and Pwd='{1}'", username, MFEncryptUtil.Md5(pwd));
        AccountEntity entity = GetEntity(condition);
        if(entity != null)
        {
            entity.DeviceIdentifier = deviceIdentifier;
            entity.DeviceModel = deviceModel;
            Update(entity);
        }
        return entity;
    }
}