using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FireCodeCreatorWeb.Models
{
    public class TableName
    {
        public int id { set; get; }
        public int pId { set; get; }
        public string name { set; get; }
        public bool open { set; get; }
    }
}