using System;

namespace MMORPG_AccountServer
{
    public class ResponseValue<CodeType, ValueType> where CodeType: Enum
    {
        public CodeType Code;
        public ValueType Value;
        public string Error;
    }
}