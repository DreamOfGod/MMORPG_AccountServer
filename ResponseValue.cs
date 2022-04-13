namespace MMORPG_AccountServer
{
    /// <summary>
    /// 控制器返回值
    /// </summary>
    /// <typeparam name="ValueType"></typeparam>
    public class ResponseValue<ValueType>
    {
        public int Code;
        public ValueType Value;
        public string Error;
        public ResponseValue() { }
        public ResponseValue(int code, ValueType value, string error) 
        {
            Code = code;
            Value = value;
            Error = error;
        }
    }
}