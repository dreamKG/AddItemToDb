using AddItemToDB.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AddItemToDB.Views
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string result = Additem(this.TextBox1.Text);
            this.Label1.Text = result;
        }

        public string Additem(string item)
        {
            string allowNull = ConfigurationManager.AppSettings["allowNull"];

            if (allowNull.ToLower().Equals("false")&&string.IsNullOrEmpty(item))
                return "输入不能为空";

            string connection = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            string tableName = ConfigurationManager.AppSettings["tableName"];
            string timeColName = ConfigurationManager.AppSettings["timeColumnName"];
            string columnName = ConfigurationManager.AppSettings["columnName"];

            DateTime time = DateTime.Parse(DateTime.Now.ToShortDateString()).AddDays(-1);
            DbUtility dbutil = new DbUtility(connection, dbType());
            string sql = "select count(*) from " + tableName + " where "+ timeColName + " = @dateTime";
            List<DbParameter> parameters = new List<DbParameter>
            {
                new SqlParameter { DbType = DbType.DateTime,ParameterName="@dateTime",Value=time }
            };

            //是否存在该时间数据
            int count = 0;
            DataTable dataCount = dbutil.ExecuteDataTable(sql, parameters);
            if (dataCount.Rows.Count > 0)
                count = int.Parse(dataCount.Rows[0][0].ToString());
            else
                return "未找到时间列";

            if (count == 0)
            {
                //不存在
                sql = "INSERT INTO " + tableName + " ("+ timeColName + ","+ columnName + ") Values(@dateTime,@itemValue) ";
                parameters = new List<DbParameter>
                {
                    new SqlParameter { DbType = DbType.DateTime,ParameterName="@dateTime",Value=time },
                    new SqlParameter { DbType = dataType(), ParameterName="@itemValue",Value=item }
                };
                count = dbutil.ExecuteNonQuery(sql, parameters);
                if (count > 0)
                    return "添加成功";
                else
                    return "添加失败";
            }
            else
            {
                //存在
                sql = "UPDATE " + tableName + " SET " + columnName + " =@itemValue where " + timeColName + "=@dateTime ";
                parameters = new List<DbParameter>
                {
                    new SqlParameter { DbType = DbType.DateTime,ParameterName="@dateTime",Value=time },
                    new SqlParameter { DbType = dataType(), ParameterName="@itemValue",Value=item }
                };
                count = dbutil.ExecuteNonQuery(sql, parameters);
                if (count > 0)
                    return "更新成功";
                else
                    return "更新失败";
            }
        }

        public DbType dataType()
        {
            string type = ConfigurationManager.AppSettings["dataType"];
            switch (type.ToLower())
            {
                case "string":
                    return DbType.String;
                case "date":
                    return DbType.Date;
                case "datetime":
                    return DbType.DateTime;
                case "time":
                    return DbType.Time;
                case "int32":
                    return DbType.Int32;
                case "int64":
                    return DbType.Int64;
                case "int16":
                    return DbType.Int16;
                case "double":
                    return DbType.Double;
                case "decimal":
                    return DbType.Decimal;
                case "sbyte":
                    return DbType.SByte;
                case "boolean":
                    return DbType.Boolean;
                default:
                    return DbType.String;
            }
        }

        public DbProviderType dbType()
        {
            string dbType = ConfigurationManager.AppSettings["dbType"];
            switch (dbType.ToLower())
            {
                case "sqlserver":
                    return DbProviderType.SqlServer;
                case "mysql":
                    return DbProviderType.MySql;
                case "postgresql":
                    return DbProviderType.PostgreSql;
                case "oracle":
                    return DbProviderType.Oracle;
                case "db2":
                    return DbProviderType.DB2;
                default:
                    return DbProviderType.SqlServer;
            }
        }
    }
}