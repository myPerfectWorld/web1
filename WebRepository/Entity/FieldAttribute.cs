using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebRepository.Entity
{
    public class FieldAttribute : System.Attribute
    {
        private string columnName;
        private MySqlDbType columnType;
        private object defaultValue;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="columnType">列类型</param>
        /// <param name="defaultValue">默认值</param>
        public FieldAttribute(string columnName, MySqlDbType columnType, object defaultValue = null)
        {
            this.columnName = columnName;
            this.columnType = columnType;
            this.defaultValue = defaultValue;
        }
        public string ColumnName { get { return this.columnName; } }
        public MySqlDbType ColumnType { get { return this.columnType; } }
        public object DefaultValue { get { return this.defaultValue; } }
    }
}
