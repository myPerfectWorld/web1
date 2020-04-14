using System;
using System.Collections.Generic;
using System.Text;

namespace WebBiz.Interface
{
    public interface ILoginBiz
    {
        int LoginWithHash(string UserName,string PwdHash);
    }
}
