using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Common.Dto
{
    public class SysLSHInput
    {
        /// <summary> 
        /// 分类
        /// </summary> 
        public string Category { set; get; }

        /// <summary> 
        /// 名称
        /// </summary> 
        public string Name { set; get; }
    
        /// <summary> 
        /// 获取个数
        /// </summary> 
        public int Num { set; get; }

    }


    public class SysLSHOutput
    {

        public int ID { get; set; }
        /// <summary> 
        /// 分类
        /// </summary> 
        public string Category { set; get; }

        /// <summary> 
        /// 流水号
        /// </summary> 
        public Int64 Code { set; get; }


        /// <summary> 
        /// 名称
        /// </summary> 
        public string Name { set; get; }

        /// <summary> 
        /// 获取个数
        /// </summary> 
        public int Num { set; get; }
    }
}
