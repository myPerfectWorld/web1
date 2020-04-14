using System;
using System.Collections.Generic;
using System.Text;

namespace WebDTO
{
    public enum ResultCode
    {
        Success = 1000,
        Fail = 1001,
        PassWordError = 1002,
        UserNameError = 1004,
        NotLogin = 1027
    }
}
