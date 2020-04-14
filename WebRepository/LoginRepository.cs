using System;
using System.Collections.Generic;
using System.Text;
using WebRepository.Interface;
using MySql.Data.MySqlClient;
using System.Data;

namespace WebRepository
{
    public class LoginRepository : ILoginRepository
    {
        private DBContext DBContext;
        public LoginRepository(DBContext DBContext)
        {
            this.DBContext = DBContext;
        }
        public int LoginWithHash(string UserName, string PwdHash)
        {
            int retData = 0;
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@userName",MySqlDbType.String,20),
                new MySqlParameter("@PwdHash",MySqlDbType.String,100),
                new MySqlParameter("@total",MySqlDbType.Int32,20)
            };
            parameters[0].Direction = ParameterDirection.Input;
            parameters[0].Value = UserName;
            parameters[1].Direction = ParameterDirection.Input;
            parameters[1].Value = PwdHash;
            parameters[2].Direction = ParameterDirection.Output;
            DBContext.ExecuteProc("sp_loginHash", parameters);
            if (parameters[2].Value == DBNull.Value)
            {
                parameters[2].Value = 0;
            }
            else
            {
                retData = (Int32)parameters[2].Value;
            }
            return retData;
        }
    }
}
