using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace FireCodeCreatorWeb.Helper
{
    public class DataOp
    {

        public static string connstr = "";


        #region 获取表的字段信息
        public IList<string> GetTableColumns(string tablename)
        {
            SqlConnection conn;
            DataTable dt;
            GetTableComm(tablename, out conn, out dt);

            IList<string> list1 = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list1.Add(dt.Rows[i]["字段名"].ToString() + "(" + dt.Rows[i]["类型"].ToString() + "," + dt.Rows[i]["占用字节数"].ToString() + ")");
            }
            conn.Close();
            return list1;
        }
        #endregion

        public static DataTable GetTableColummn(string tablename)
        {
            SqlConnection conn;
            DataTable dt;
            GetTableComm(tablename, out conn, out dt);
            return dt;
        }

        #region 获取表的字段名称
        public IList<string> GetTableColumnsName(string tablename)
        {
            SqlConnection conn;
            DataTable dt;
            GetTableComm(tablename, out conn, out dt);

            IList<string> list1 = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list1.Add(dt.Rows[i]["字段名"].ToString());
            }
            conn.Close();
            return list1;
        }
        #endregion

        #region 公共函数,获取表的相关信息
        private static void GetTableComm(string tablename, out SqlConnection conn, out DataTable dt)
        {
            string sql = @"SELECT a.colorder N'字段序号',a.name N'字段名',(case when COLUMNPROPERTY( a.id,a.name,'IsIdentity')=1 then '√'else '' end) N'标识',
                    (case when (SELECT count(*) FROM sysobjects WHERE (name in (SELECT name FROM sysindexes WHERE (id = a.id) AND 
                    (indid in (SELECT indid FROM sysindexkeys WHERE (id = a.id) AND (colid in (SELECT colid FROM syscolumns WHERE (id = a.id) 
                    AND (name = a.name))))))) AND (xtype = 'PK'))>0 then 'yes' else '' end) N'主键', b.name N'类型', a.length N'占用字节数', COLUMNPROPERTY(a.id,a.name,'PRECISION') as N'长度',
                    isnull(COLUMNPROPERTY(a.id,a.name,'Scale'),0) as N'小数位数',(case when a.isnullable=1 then '√'else '' end) N'允许空',isnull(e.text,'') N'默认值',isnull(g.[value], ' ') AS [说明] 
                    FROM syscolumns a left join systypes b  on a.xtype=b.xusertype inner join sysobjects d  on a.id=d.id and d.xtype='U' and d.name<>'dtproperties' 
                    left join syscomments e on a.cdefault=e.id left join sys.extended_properties g on a.id=g.major_id AND a.colid=g.minor_id 
                    where d.name='" + tablename + "'order by object_name(a.id),a.colorder";
            conn = new SqlConnection(connstr);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            dt = ds.Tables[0];
        }
        #endregion

        #region 获取表的字段,返回DataTable
        public DataTable GetTableColumnsDs(string tablename)
        {
            SqlConnection conn;
            DataTable dt;
            GetTableComm(tablename, out conn, out dt);
            return dt;
        }
        #endregion

        #region 生成操作类
        #region 插入操作类
        /// <summary>
        /// 插入操作类
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="strHtml"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static void InsertOpClass(string tablename, StringBuilder strHtml, DataTable dt, string strFilter)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = "字段名 in (" + strFilter + ")";

            //过滤

            strHtml.Append("/// <summary>\r\n");
            strHtml.Append("/// 插入记录\r\n");
            strHtml.Append("/// </summary>\r\n");
            strHtml.Append("/// <param name=\"paras\">参数数组(" + dv.Count + "个)</param>\r\n");
            strHtml.Append("public int Insert(string[] paras)\r\n");
            strHtml.Append("{\r\n");
            strHtml.Append("    SqlParameter[] sqlParas =\r\n");
            strHtml.Append("    {");
            string sqlParas = "";
            for (int i = 0; i < dv.Count; i++)
            {
                sqlParas += "\r\n        new SqlParameter(\"@" + dv[i]["字段名"].ToString() + "\",paras[" + i + "]),";
            }
            if (sqlParas.Length > 0)
            {
                sqlParas = sqlParas.Remove(sqlParas.Length - 1, 1);
            }
            strHtml.Append(sqlParas);
            strHtml.Append("\r\n    }");
            strHtml.Append("\r\n    return SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, \"sp_" + tablename + "_Insert\", sqlParas);");
            strHtml.Append("\r\n}");
            strHtml.Append("\r\n");
            strHtml.Append("\r\n");
            strHtml.Append("\r\n");

        }
        #endregion

        #region 修改操作类
        /// <summary>
        /// 插入操作类
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="strHtml"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static void UpdateOpClass(string tablename, StringBuilder strHtml, DataTable dt, string strFilter)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = "字段名 in (" + strFilter + ")";

            //过滤
            strHtml.Append("/// <summary>\r\n");
            strHtml.Append("/// 修改记录\r\n");
            strHtml.Append("/// </summary>\r\n");
            strHtml.Append("/// <param name=\"paras\">参数数组(" + dv.Count + "个)</param>\r\n");
            strHtml.Append("public int Update(string[] paras)\r\n");
            strHtml.Append("{\r\n");
            strHtml.Append("    SqlParameter[] sqlParas =\r\n");
            strHtml.Append("    {");
            string sqlParas = "";
            for (int i = 0; i < dv.Count; i++)
            {
                sqlParas += "\r\n        new SqlParameter(\"@" + dv[i]["字段名"].ToString() + "\",paras[" + i + "]),";
            }
            if (sqlParas.Length > 0)
            {
                sqlParas = sqlParas.Remove(sqlParas.Length - 1, 1);
            }
            strHtml.Append(sqlParas);
            strHtml.Append("\r\n    }");
            strHtml.Append("\r\n    return SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, \"sp_" + tablename + "_Update\", sqlParas);");
            strHtml.Append("\r\n}");
            strHtml.Append("\r\n");
            strHtml.Append("\r\n");
            strHtml.Append("\r\n");

        }
        #endregion

        #region 删除操作类
        /// <summary>
        /// 删除操作类
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="strHtml"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static void DeleteOpClass(string tablename, StringBuilder strHtml, DataTable dt)
        {

            strHtml.Append("/// <summary>\r\n");
            strHtml.Append("/// 删除记录\r\n");
            strHtml.Append("/// </summary>\r\n");
            strHtml.Append("public int Delete(string primaryKey)\r\n");
            strHtml.Append("{\r\n");
            strHtml.Append("    SqlParameter[] sqlParas =\r\n");
            strHtml.Append("    {");
            string sqlParas = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["主键"].ToString() == "yes")
                {
                    sqlParas = "\r\n        new SqlParameter(\"@" + dt.Rows[i]["字段名"].ToString() + "\",primaryKey)";
                    break;
                }
            }
            strHtml.Append(sqlParas);
            strHtml.Append("\r\n    }");
            strHtml.Append("\r\n    return SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, \"sp_" + tablename + "_Delete\", sqlParas);");
            strHtml.Append("\r\n}");
            strHtml.Append("\r\n");
            strHtml.Append("\r\n");
            strHtml.Append("\r\n");

        }
        #endregion

        #region 选择操作类
        /// <summary>
        /// 选择操作类
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="strHtml"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static void SelectOpClass(string tablename, StringBuilder strHtml, DataTable dt)
        {

            strHtml.Append("/// <summary>\r\n");
            strHtml.Append("/// 查询一条记录\r\n");
            strHtml.Append("/// </summary>\r\n");
            strHtml.Append("public DataTable Select(string primaryKey)\r\n");
            strHtml.Append("{\r\n");
            strHtml.Append("    SqlParameter[] sqlParas =\r\n");
            strHtml.Append("    {");
            string sqlParas = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["主键"].ToString() == "yes")
                {
                    sqlParas = "\r\n        new SqlParameter(\"@" + dt.Rows[i]["字段名"].ToString() + "\",primaryKey)";
                    break;
                }
            }
            strHtml.Append(sqlParas);
            strHtml.Append("\r\n    }");
            strHtml.Append("\r\n    return SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, \"sp_" + tablename + "_Load\", sqlParas);");
            strHtml.Append("\r\n}");
            strHtml.Append("\r\n");
            strHtml.Append("\r\n");
            strHtml.Append("\r\n");

        }
        #endregion
        #endregion

        #region 获取存储过程参数列表
        /// <summary>
        /// 获取存储过程参数列表
        /// </summary>
        /// <param name="storePName">存储过程名称</param>
        /// <returns></returns>
        public static DataTable GetStoreParasm(string storePName)
        {
            string sql = "select a.name  from   syscolumns   a  left   join   sysobjects   b   on   a.id   =   b.id  where   left(a.name,1)   =   '@' and b.name='" + storePName + "'";
            SqlCommand cmd = new SqlCommand(sql, new SqlConnection(connstr));
            cmd.Connection.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds.Tables[0];
        }
        #endregion

        #region 生成自定义函数
        /// <summary>
        /// 生成自定义函数
        /// </summary>
        /// <param name="SpName">存储过程名称</param>
        /// <param name="strFilter">过滤条件</param>
        /// <param name="className">类名</param>
        /// <param name="returnType">返回方法</param>
        /// <param name="remark">说明</param>
        public static void MakeMyselfMethod(string className, string returnType, string SpName, string strFilter, string remark, StringBuilder strHtml, DataTable dt, string isStatic)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = "name in (" + strFilter + ")";

            //过滤

            strHtml.Append("/// <summary>\r\n");
            strHtml.Append("/// " + remark + "\r\n");
            strHtml.Append("/// </summary>\r\n");
            strHtml.Append("/// <param name=\"paras\">参数数组(" + dv.Count + "个)</param>\r\n");
            strHtml.Append("public " + isStatic + " " + returnType + " " + className + "(string[] paras)\r\n");
            strHtml.Append("{\r\n");
            strHtml.Append("    SqlParameter[] sqlParas =\r\n");
            strHtml.Append("    {");
            string sqlParas = "";
            for (int i = 0; i < dv.Count; i++)
            {
                sqlParas += "\r\n        new SqlParameter(\"" + dv[i]["name"].ToString() + "\",paras[" + i + "]),";
            }
            if (sqlParas.Length > 0)
            {
                sqlParas = sqlParas.Remove(sqlParas.Length - 1, 1);
            }
            strHtml.Append(sqlParas);
            strHtml.Append("\r\n    }");
            strHtml.Append("\r\n    return SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, \"" + SpName + "\", sqlParas);");
            strHtml.Append("\r\n}");
            strHtml.Append("\r\n");
            strHtml.Append("\r\n");
            strHtml.Append("\r\n");
        }
        #endregion

        public static DataTable GetViewTableColumn(string viewTableName)
        {
            string sql = @"SELECT a.colorder N'字段序号',a.name N'字段名',(case when COLUMNPROPERTY( a.id,a.name,'IsIdentity')=1 then '√'else '' end) N'标识',(case when (SELECT count(*) FROM sysobjects WHERE (name in (SELECT name FROM sysindexes WHERE (id = a.id) AND (indid in (SELECT indid FROM sysindexkeys WHERE (id = a.id) AND (colid in (SELECT colid FROM syscolumns WHERE (id = a.id) AND (name = a.name))))))) AND (xtype = 'PK'))>0 then 'yes' else '' end) N'主键', b.name N'类型', a.length N'占用字节数', COLUMNPROPERTY(a.id,a.name,'PRECISION') as N'长度', isnull(COLUMNPROPERTY(a.id,a.name,'Scale'),0) as N'小数位数',(case when a.isnullable=1 then '√'else '' end) N'允许空',isnull(e.text,'') N'默认值'  FROM syscolumns a left join systypes b  on a.xtype=b.xusertype inner join sysobjects d  on a.id=d.id and d.xtype='V' and d.name<>'dtproperties' left join syscomments e on a.cdefault=e.id where d.name='" + viewTableName + "'order by object_name(a.id),a.colorder";
            SqlCommand cmd = new SqlCommand(sql, new SqlConnection(connstr));
            cmd.Connection.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds.Tables[0];

        }
    }
}