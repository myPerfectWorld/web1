using System;
using System.Collections.Generic;
using System.Text;
using WebBiz.Interface;
using WebRepository.Interface;

namespace WebBiz
{
    public class LoginBiz : ILoginBiz
    {
        private ILoginRepository loginRepository;
        public LoginBiz(ILoginRepository loginRepository)
        {
            this.loginRepository = loginRepository;
        }
        public int LoginWithHash(string UserName, string PwdHash)
        {
            int retData = loginRepository.LoginWithHash(UserName,PwdHash);
            return retData;
        }
    }
}
