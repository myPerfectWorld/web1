using System;
using System.Collections.Generic;
using System.Text;

namespace WebRepository.Interface
{
    public interface ILoginRepository
    {
        int LoginWithHash(string UserName,string PwdHash);
    }
}
