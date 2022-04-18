namespace MMORPG_AccountServer.Bean.GameServer
{
    /// <summary>
    /// 区服
    /// </summary>
    public class GameServerBean
    {
        /// <summary>
        /// 区服编号
        /// </summary>
        public int Id;

        /// <summary>
        /// 区服状态
        /// </summary>
        public byte RunStatus;

        /// <summary>
        /// 是否推荐
        /// </summary>
        public bool IsCommand;

        /// <summary>
        /// 是否新服
        /// </summary>
        public bool IsNew;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name;

        /// <summary>
        /// IP
        /// </summary>
        public string Ip;

        /// <summary>
        /// 端口号
        /// </summary>
        public int Port;
    }
}