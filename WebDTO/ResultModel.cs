using System;
using System.Collections.Generic;
using System.Text;

namespace WebDTO
{
    public class ResultModel<T>
    {
        private ResultCode code;
        public ResultCode Code { get => code; set => code = value; }
        public T Data;
        public string Message;
    }
}
