using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using WebRepository.Entity;

namespace WebRepository
{
    public class DBContext
    {
        public string ConnectionString { get; set; }
        public DBContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
        /// <summary>
        /// 获取新的数据库连接
        /// </summary>
        /// <returns></returns>
        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
        /// <summary>
        /// 查询Sql
        /// </summary>
        /// <typeparam name="T">DBContext类型</typeparam>
        /// <param name="sql">sql语句</param>            
        /// <param name="ps">参数</param>
        /// <returns></returns>
        public IList<T> Query<T>(string sql, System.Data.CommandType commandType = System.Data.CommandType.Text, params MySqlParameter[] ps)
        {
            IList<T> list = new List<T>();
            using (var conn = GetConnection())
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.CommandType = commandType;
                cmd.Parameters.AddRange(ps);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    //读取数据
                    while (reader.Read())
                    {
                        T t = DataRowTo<T>(reader);

                        list.Add(t);
                    }
                }
            }
            return list;
        }

        public int Execute(string sql, params MySqlParameter[] ps)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddRange(ps);
                int i = cmd.ExecuteNonQuery();
                return i;
            }
        }

        //执行不带参数的存储过程
        public IList<T> ExecuteProc<T>(string procName)
        {
            try
            {
                IList<T> list = new List<T>();
                using (var conn = GetConnection())
                {
                    MySqlCommand mySqlCommandPro = getSqlCommand(procName, conn);//定义存储过程接口
                    try
                    {
                        mySqlCommandPro.CommandType = CommandType.StoredProcedure;//设置调用的类型为存储过程 
                        conn.Open();
                        using (MySqlDataReader reader = mySqlCommandPro.ExecuteReader())
                        {
                            //读取数据     
                            while (reader.Read())
                            {
                                T t = DataRowTo<T>(reader);

                                list.Add(t);
                            }
                        }
                        conn.Close();

                        return list;
                    }
                    catch (MySqlException e)
                    {
                        throw e;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
        //Function: 建立执行命令语句对象
        //Function:执行存储过程,带参数
        public IList<T> ExecuteProc<T>(string procName, MySqlParameter days)
        {
            try
            {
                IList<T> list = new List<T>();
                using (var conn = GetConnection())
                {
                    MySqlCommand mySqlCommandPro = getSqlCommand(procName, conn);//定义存储过程接口
                    try
                    {
                        mySqlCommandPro.CommandType = CommandType.StoredProcedure;//设置调用的类型为存储过程 
                        mySqlCommandPro.Parameters.Add(days);
                        conn.Open();
                        //mySqlCommandPro.ExecuteNonQuery();//执行存储过程         
                        using (MySqlDataReader reader = mySqlCommandPro.ExecuteReader())
                        {
                            //读取数据     
                            while (reader.Read())
                            {
                                T t = DataRowTo<T>(reader);

                                list.Add(t);
                            }
                        }
                        conn.Close();

                        return list;
                    }
                    catch (MySqlException e)
                    {
                        throw e;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        //Function:执行存储过程,带参数
        public IList<T> ExecuteProc<T>(string procName, MySqlParameter[] parameters)
        {
            try
            {
                IList<T> list = new List<T>();
                using (var conn = GetConnection())
                {
                    MySqlCommand mySqlCommandPro = getSqlCommand(procName, conn);//定义存储过程接口
                    try
                    {
                        mySqlCommandPro.CommandType = CommandType.StoredProcedure;//设置调用的类型为存储过程
                        for (int i = 0; i < parameters.Length; i++)
                            mySqlCommandPro.Parameters.Add(parameters[i]);
                        conn.Open();
                        //mySqlCommandPro.ExecuteNonQuery();//执行存储过程         
                        using (MySqlDataReader reader = mySqlCommandPro.ExecuteReader())
                        {
                            //读取数据     
                            while (reader.Read())
                            {
                                T t = DataRowTo<T>(reader);

                                list.Add(t);
                            }
                        }
                        conn.Close();

                        return list;
                    }
                    catch (MySqlException e)
                    {
                        throw e;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }

        }
        //Function: 建立执行命令语句对象
        public int ExecuteProc(string procName, MySqlParameter[] parameters)
        {
            try
            {
                using (var conn = GetConnection())
                {

                    MySqlCommand mySqlCommandPro = getSqlCommand(procName, conn);//定义存储过程接口
                    try
                    {
                        mySqlCommandPro.CommandType = CommandType.StoredProcedure;//设置调用的类型为存储过程
                        for (int i = 0; i < parameters.Length; i++)
                            mySqlCommandPro.Parameters.Add(parameters[i]);
                        conn.Open();
                        //mySqlCommandPro.ExecuteNonQuery();//执行存储过程         
                        int result = mySqlCommandPro.ExecuteNonQuery();

                        conn.Close();

                        return result;
                    }
                    catch (MySqlException e)
                    {
                        throw e;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }
        public static MySqlCommand getSqlCommand(String sql, MySqlConnection mysql)
        {
            MySqlCommand mySqlCommand = new MySqlCommand(sql, mysql);
            return mySqlCommand;
        }
        /// <summary>  
        /// DataReader的当前记录转换为T
        /// </summary>  
        /// <typeparam name="T">泛型</typeparam>  
        /// <param name="reader">datareader</param>  
        /// <returns>返回泛型类型</returns>  
        protected T DataRowTo<T>(MySqlDataReader reader)
        {
            string fieldName = "";
            try
            {
                T t = System.Activator.CreateInstance<T>();
                Type obj = t.GetType();
                var ps = obj.GetProperties();
                // 循环字段  
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    fieldName = reader.GetName(i);
                    PropertyInfo propertyInfo = null;

                    foreach (var p in ps)
                    {
                        if (p.GetCustomAttribute<FieldAttribute>().ColumnName == reader.GetName(i))
                        {
                            propertyInfo = p;
                            break;
                        }
                    }
                    //如果没有对应字段的属性则继续
                    if (propertyInfo == null) continue;

                    object tempValue = null;
                    bool isDBNull = true;
                    try
                    {
                        isDBNull = reader.IsDBNull(i);
                    }
                    catch (MySql.Data.Types.MySqlConversionException e)
                    {
                        isDBNull = true;
                    }
                    if (isDBNull)
                    {
                        MySqlDbType type = propertyInfo.GetCustomAttribute<FieldAttribute>().ColumnType;
                        if (propertyInfo.GetCustomAttribute<FieldAttribute>().DefaultValue != null)
                            tempValue = propertyInfo.GetCustomAttribute<FieldAttribute>().DefaultValue;
                        else
                            tempValue = GetDBNullValue(type);
                    }
                    else
                    {
                        tempValue = reader.GetValue(i);
                    }
                    propertyInfo.SetValue(t, tempValue, null);

                }
                return t;
            }
            catch (Exception e)
            {
                Console.WriteLine("获取字段" + fieldName + "时出错");
                throw e;
            }
        }
        /// <summary>  
        /// DataReader的当前记录转换为数组
        /// </summary>  
        /// <typeparam name="T">数组</typeparam>  
        /// <param name="reader">datareader</param>  
        /// <returns>返回数组类型</returns>  
        protected ArrayList DataRowTo1<ArrayList>(MySqlDataReader reader)
        {
            ArrayList t = System.Activator.CreateInstance<ArrayList>();
            Type obj = t.GetType();
            var ps = obj.GetProperties();

            // 循环字段  
            for (int i = 0; i < reader.FieldCount; i++)
            {

                PropertyInfo propertyInfo = null;

                foreach (var p in ps)
                {
                    if (p.GetCustomAttribute<FieldAttribute>().ColumnName == reader.GetName(i))
                    {
                        propertyInfo = p;
                    }
                }
                //如果没有对应字段的属性则继续
                if (propertyInfo == null) continue;

                object tempValue = null;
                bool isDBNull = true;
                try
                {
                    isDBNull = reader.IsDBNull(i);
                }
                catch (MySql.Data.Types.MySqlConversionException e)
                {
                    isDBNull = true;
                }
                if (isDBNull)
                {
                    MySqlDbType type = propertyInfo.GetCustomAttribute<FieldAttribute>().ColumnType;
                    if (propertyInfo.GetCustomAttribute<FieldAttribute>().DefaultValue != null)
                        tempValue = propertyInfo.GetCustomAttribute<FieldAttribute>().DefaultValue;
                    else
                        tempValue = GetDBNullValue(type);
                }
                else
                {
                    tempValue = reader.GetValue(i);
                }
                propertyInfo.SetValue(t, tempValue, null);

            }
            return t;
        }
        public object ExecuteScalar(string sql, CommandType cmdType, params MySqlParameter[] pms)
        {
            using (var con = GetConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    cmd.CommandType = cmdType;
                    if (pms != null)
                    {
                        cmd.Parameters.AddRange(pms);
                    }
                    con.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }
        /// <summary>  
        /// 返回值为DBnull的默认值  
        /// </summary>  
        /// <param name="typeFullName">数据库数据类型</param>  
        /// <returns>返回的默认值</returns>  
        private object GetDBNullValue(MySqlDbType typeFullName)
        {
            if (typeFullName == MySqlDbType.String)
            {
                return String.Empty;
            }
            if (typeFullName == MySqlDbType.Int32)
            {
                return 0;
            }
            if (typeFullName == MySqlDbType.DateTime)
            {
                return default(DateTime);
            }
            if (typeFullName == MySqlDbType.Bit)
            {
                return false;
            }
            if (typeFullName == MySqlDbType.Int16)
            {
                return 0;
            }

            return null;
        }
    }
}
