using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.ParamCollection;
using WebBiz.Interface;
using WebDTO;

namespace WebApplication1.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginBiz loginBiz;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginBiz"></param>
        public LoginController(ILoginBiz loginBiz)
        {
            this.loginBiz = loginBiz;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ResultModel<int> LoginWithHash([FromBody]LoginInfoParams loginInfoParams)
        {
            var result = new ResultModel<int>();
            int data = loginBiz.LoginWithHash(loginInfoParams.UserName, loginInfoParams.PwdHash);
            if (data > 0)
            {
                result.Code = ResultCode.Success;
                result.Message = "成功";
            }
            else
            {
                result.Code = ResultCode.Fail;
                result.Message = "失败";
            }
            return result;
        }
    }
}