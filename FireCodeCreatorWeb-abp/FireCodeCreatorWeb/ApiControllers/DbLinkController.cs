using FireCodeCreatorWeb.Helper;
using FireCodeCreatorWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace FireCodeCreatorWeb.ApiControllers
{
    public class DbLinkController : ApiController
    {
        string txtkeyurl = System.Web.Hosting.HostingEnvironment.MapPath("~/") + "Log\\keystringlog.txt";
        string txturl = System.Web.Hosting.HostingEnvironment.MapPath("~/") + "Log\\tablelog.txt";

        public DbLinkController() : base()
        {

        }

        [HttpGet]
        public List<string> GetDBNameList(string ServerName, string UserName, string Pwd)
        {
            try
            {
                string connstr = "Data Source=" + ServerName + ";Initial Catalog=master;Persist Security Info=True;User ID=" + UserName + ";Password=" + Pwd + "";
                SqlConnection conn = new SqlConnection(connstr);

                string sql = "sp_helpdb";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Connection.Open();

                IList<string> datalist = new List<string>();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    datalist.Add(dr["name"].ToString());
                }

                return datalist.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        [HttpGet]
        public List<TableName> GetTableNameTreeList(string DBName,string ServerName, string UserName, string Pwd)
        {
            try
            {
                List<TableName> tableNameTreeList = new List<TableName>();


                string connstr = "Data Source=" + ServerName + ";Initial Catalog=" + DBName + ";Persist Security Info=True;User ID=" + UserName + ";Password=" + Pwd + "";
                SqlConnection oleConn = new SqlConnection(connstr);

                string sql = "SELECT   name   FROM   sysobjects   WHERE   xtype   =   'U'   AND   name   <>   'dtproperties'";

                sql += "  use " + DBName + " select  a.name from  sysobjects  a where  a.xtype='P'  and  a.status>=0 order  by  a.name ";
                sql += " use " + DBName + " select name from sysobjects where type='V'";
                SqlCommand oleCmd = new SqlCommand(sql, oleConn);
                try
                {
                    oleCmd.Connection.Open();
                }
                catch
                {
                    //MessageBox.Show("数据库连接失败!");
                    throw new Exception("数据库连接失败!");
                }
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(oleCmd);
                da.Fill(ds);

                int index = 4;
                //获取表
                tableNameTreeList.Add(new TableName()
                {
                    id = 1,
                    name = "表",
                    open = false
                });

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    tableNameTreeList.Add(new TableName() {
                        id = index,
                        name = ds.Tables[0].Rows[i]["name"].ToString(),
                        pId = 1,
                        open = false
                    });
                    index++;
                    //mylist.Add(ds.Tables[0].Rows[i]["name"].ToString());
                }

                //获取视图
                tableNameTreeList.Add(new TableName()
                {
                    id = 2,
                    name = "视图",
                    open = false
                });

                for (int k = 0; k < ds.Tables[2].Rows.Count; k++)
                {
                    tableNameTreeList.Add(new TableName()
                    {
                        id = index,
                        name = ds.Tables[2].Rows[k]["name"].ToString(),
                        pId = 2,
                        open = false
                    });
                    index++;
                }
                return tableNameTreeList.OrderBy(w => w.name).ToList() ;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// 根据条件查询对应的表的字段列表
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="DBName"></param>
        /// <param name="ServerName"></param>
        /// <param name="UserName"></param>
        /// <param name="Pwd"></param>
        /// <returns></returns>
        [HttpGet]
        public TableLog GetTableColunmNameList(string TableName, string DBName, string ServerName, string UserName, string Pwd)
        {
            try
            {
                DataOp.connstr = "Data Source=" + ServerName + ";Initial Catalog=" + DBName + ";Persist Security Info=True;User ID=" + UserName + ";Password=" + Pwd + "";

                DataTable dt = DataOp.GetTableColummn(TableName);

                List<TableColunmName> tableColunmNameList = new List<TableColunmName>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TableColunmName tableColunmName = new TableColunmName();
                    tableColunmName.Name = dt.Rows[i]["字段名"].ToString();
                    tableColunmName.Category = dt.Rows[i]["类型"].ToString();
                    tableColunmName.Length = dt.Rows[i]["长度"].ToString();
                    tableColunmName.IsNull = dt.Rows[i]["允许空"].ToString();
                    tableColunmName.IsKey = dt.Rows[i]["主键"].ToString();
                    tableColunmName.DurName = dt.Rows[i]["说明"].ToString();

                    tableColunmNameList.Add(tableColunmName);
                }

                List<TableColunmName> tableColunmNameListReturn = new List<TableColunmName>();
                TableLog logReturn = new TableLog();

                //查询本地记录有没相关记录，如果有，则要读取某些字段
                string tableLogString = FileHelper.ReadLogFile(txturl);
                List<TableLog> tableLogList = JsonHelper.JSONStringToList<TableLog>(tableLogString);

                string keystring = FileHelper.ReadLogFile(txtkeyurl);
                List<KeyStringLog> keyStringList = JsonHelper.JSONStringToList<KeyStringLog>(keystring);

                TableLog tableLog;
                tableLog = tableLogList == null ? null : tableLogList.Where(w => w.TableName == TableName).FirstOrDefault();

                if (tableLog != null)       //如果有相关记录
                {
                    logReturn = new TableLog(tableLog);         //重新初始化，引用类型不能直接赋值
                    logReturn.tableColunmList = new List<TableColunmName>();

                    foreach (var itemNew in tableColunmNameList)
                    {
                        foreach (var itemOld in tableLog.tableColunmList)
                        {
                            if (itemNew.Name == itemOld.Name)
                            {
                                logReturn.tableColunmList.Add(TableColunmName.CoventTableColunmName(itemNew, itemOld));
                            }
                        }
                    }
                }
                else {                      //如果无相关记录，则使用key对应string的对应字段
                    foreach (var item in tableColunmNameList)
                    {
                        //if (keyStringList != null)
                        //{
                        //    var old = keyStringList.Where(w => w.Key == item.Name).FirstOrDefault();
                        //    if (old != null) item.DurName = old.Strings;
                        //}
                        logReturn.tableColunmList.Add(item);
                    }
                }

                return logReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }



        [HttpGet]
        public List<TableColunmName> GetViewColunmNameList(string ViewName, string DBName, string ServerName, string UserName, string Pwd)
        {
            try
            {
                DataOp.connstr = "Data Source=" + ServerName + ";Initial Catalog=" + DBName + ";Persist Security Info=True;User ID=" + UserName + ";Password=" + Pwd + "";

                DataTable dt = DataOp.GetViewTableColumn(ViewName);

                List<TableColunmName> tableColunmNameList = new List<TableColunmName>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TableColunmName tableColunmName = new TableColunmName();
                    tableColunmName.Name = dt.Rows[i]["字段名"].ToString();
                    tableColunmName.Category = dt.Rows[i]["类型"].ToString();
                    tableColunmName.Length = dt.Rows[i]["长度"].ToString();
                    tableColunmName.IsNull = dt.Rows[i]["允许空"].ToString();
                    tableColunmName.IsKey = dt.Rows[i]["主键"].ToString();
                    tableColunmName.DurName = dt.Rows[i]["说明"].ToString();

                    tableColunmNameList.Add(tableColunmName);
                    //DataRow dr = dts.NewRow();
                    //dr["字段名"] = dt.Rows[i]["字段名"];

                    //var item = KeyStringHelper.GetKeyStringLogBy(keyStringList, dt.Rows[i]["字段名"].ToString());
                    //if (item == null)
                    //{
                    //    tableColunmName.DurName = "";
                    //}
                    //else
                    //{
                    //    dr["对应名称"] = item.Strings;
                    //}
                }

                return tableColunmNameList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpPost]
        public bool SaveViewColunmNameListLog(string TableName, TableLog log)
        {
            ///保存表相关记录
            
            string tableLogString = FileHelper.ReadLogFile(txturl);
            List<TableLog> tableLogList = JsonHelper.JSONStringToList<TableLog>(tableLogString);
            if (tableLogList == null)
            {
                tableLogList = new List<TableLog>();
            }

            bool isIn = false;
            for (int i = 0; i < tableLogList.Count; i++)
            {
                if (tableLogList[i].TableName == TableName)
                {
                    tableLogList[i] = log;
                    isIn = true;
                }
            }

            if (isIn == false)
            {
                tableLogList.Add(log);
            }
            tableLogString = JsonHelper.ListToJSONString<TableLog>(tableLogList);
            FileHelper.SaveLogFile(txturl, tableLogString);

            //保存key对应string记录
            
            string keystring = FileHelper.ReadLogFile(txtkeyurl);
            List<KeyStringLog> keyStringList = JsonHelper.JSONStringToList<KeyStringLog>(tableLogString);
            if (keyStringList == null)
            {
                keyStringList = new List<KeyStringLog>();
            }

            foreach (var item in log.tableColunmList)
            {
                var old = keyStringList.Where(w => w.Key == item.Name).FirstOrDefault();
                if (old != null)   keyStringList.Remove(old);
                keyStringList.Add(new KeyStringLog() { Key = item.Name, Strings = item.DurName });
            }
            keystring = JsonHelper.ListToJSONString<KeyStringLog>(keyStringList);
            FileHelper.SaveLogFile(txtkeyurl, keystring);

            return true;
        }

    }
}