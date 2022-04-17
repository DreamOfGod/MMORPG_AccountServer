namespace MMORPG_AccountServer.Bean.GameServer
{
    /// <summary>
    /// 区服分组
    /// </summary>
    public struct GameServerGroupBean
    {
        /// <summary>
        /// 分组中的第一个区服ID
        /// </summary>
        public int FirstId;

        /// <summary>
        /// 分组中的最后一个区服ID
        /// </summary>
        public int LastId;
    }
}