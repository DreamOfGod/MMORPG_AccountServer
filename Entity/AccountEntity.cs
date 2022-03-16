﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMORPG_AccountServer.Entity
{
    public class AccountEntity
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Pwd { get; set; }

        public int YuanBao { get; set; }

        public int LastServerId { get; set; }

        public string LastServerName { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}