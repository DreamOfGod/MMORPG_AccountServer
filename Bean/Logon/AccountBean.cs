using MMORPG_AccountServer.Bean.GameServer;
using System;

namespace MMORPG_AccountServer.Bean.Logon
{
    public class AccountBean
    {
        public int Id;
        public string Username;
        public string Mobile;
        public string Mail;
        public int Money;
        public short ChannelId;
        public GameServerBean LastLogonGameServer;
        public DateTime? LastLogonServerTime;
        public int? LastLogonRoleId;
        public string DeviceIdentifier;
        public string DeviceModel;
    }
}