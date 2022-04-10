using Mmcoy.Framework;
using Mmcoy.Framework.AbstractBase;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public partial class AccountDBModel
{
    public MFReturnValue<int> Register(string username, string pwd, short channelId, string deviceIdentifier, string deviceModel)
    {
        MFReturnValue<int> retValue = new MFReturnValue<int>();

        using (SqlConnection conn = new SqlConnection(DBConn.MMORPG_Account))
        {
            conn.Open();
            //开始事务
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
                    retValue.HasError = true;
                    retValue.Message = result.Message;
                    trans.Rollback();
                    
                }
                else
                {
                    retValue.Value = (int)result.OutputValues["Id"];
                    trans.Commit();
                }
            }
            else
            {
                retValue.HasError = true;
                retValue.Message = "用户名已经存在";
            }
        }

        return retValue;
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