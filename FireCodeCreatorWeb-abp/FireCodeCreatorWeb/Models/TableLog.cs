using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FireCodeCreatorWeb.Models
{
    public class TableLog
    {
        public TableLog() {
            this.tableColunmList = new List<TableColunmName>();
        }

        public TableLog(TableLog log) {
            this.TableName = log.TableName;
            this.ModelName = log.ModelName;
            this.ClassName = log.ClassName;
            this.ChinaName = log.ChinaName;
            this.tableColunmList = new List<TableColunmName>();
        }

        /// <summary>
        /// CBedType
        /// </summary>
        public string TableName { set; get; }
        /// <summary>
        /// CBedType
        /// </summary>
        public string ModelName { set; get; }
        /// <summary>
        /// BedType
        /// </summary>
        public string ClassName { set; get; }
        /// <summary>
        /// 床头卡样式
        /// </summary>
        public string ChinaName { set; get; }

        public List<TableColunmName> tableColunmList { set; get; }
    }
}