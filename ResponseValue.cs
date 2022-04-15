namespace MMORPG_AccountServer
{
    /// <summary>
    /// 控制器返回值
    /// </summary>
    /// <typeparam name="DataType">数据的类型</typeparam>
    public struct ResponseData<DataType>
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Code;
        /// <summary>
        /// 数据
        /// </summary>
        public DataType Data;
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Error;

        public ResponseData(int code, DataType data, string error)
        {
            Code = code;
            Data = data;
            Error = error;
        }
    }
}