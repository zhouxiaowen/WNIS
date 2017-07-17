using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FireCodeCreatorWeb.Models
{
    public class TableColunmName
    {
        public static TableColunmName CoventTableColunmName(TableColunmName objNew, TableColunmName objOld)
        {
            return new TableColunmName()
            {
                Name = objNew.Name,
                DurName = objOld.DurName,
                Category = objNew.Category,
                Length = objNew.Length,
                IsNull = objNew.IsNull,
                IsKey = objNew.IsKey,
                IsMyKey = objOld.IsMyKey,
                IsQueryInput = objOld.IsQueryInput,
                IsQueryOutput = objOld.IsQueryOutput,
                IsInput = objOld.IsInput,
                IsOutput = objOld.IsOutput,
                IsEmpty = objOld.IsEmpty,
                IsLength = objOld.IsLength,
                IsExist = objOld.IsExist
            };
        }

        /// <summary>
        /// 字段名
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 对应名称
        /// </summary>
        public string DurName { set; get; }
        
        /// <summary>
        /// 类型
        /// </summary>
        public string Category { set; get; }


        /// <summary>
        /// 对应长度
        /// </summary>
        public string Length { set; get; }

        /// <summary>
        /// 是否为空
        /// </summary>
        public string IsNull { set; get; }

        /// <summary>
        /// 是否为主键
        /// </summary>
        public string IsKey { set; get; }

        /// <summary>
        /// 实际主键
        /// </summary>
        public bool IsMyKey { set; get; }


        /// <summary>
        /// 是否作为查询参数
        /// </summary>
        public bool IsQueryInput { set; get; }

        /// <summary>
        /// 是否作为查询列表返回
        /// </summary>
        public bool IsQueryOutput { set; get; }

        /// <summary>
        /// 是否作为输入参数
        /// </summary>
        public bool IsInput { set; get; }

        /// <summary>
        /// 是否可为空,input 判断使用
        /// </summary>
        public bool IsEmpty { set; get; }

        /// <summary>
        /// 判断长度
        /// </summary>
        public string IsLength { set; get; }

        /// <summary>
        /// 是否作为输出参数
        /// </summary>
        public bool IsOutput { set; get; }

        /// <summary>
        /// 是否作为唯一性判断
        /// </summary>
        public bool IsExist { set; get; }
    }
}