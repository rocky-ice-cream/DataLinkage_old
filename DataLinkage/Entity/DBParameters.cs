using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MasterDataLinkage
{

    /// <summary>
    /// DBのパラメータクラス
    /// </summary>
    /// <remarks>
    /// 別にEntity.csにまとめても良かったが可読性を上げる為、別ファイル化
    /// </remarks>
    public class DBParameters
    {
        /// <summary>
        /// パラメータ名
        /// </summary>
        public string ParameterName { get; set; }

        public DbType DbType { get; set; }

        /// <summary>
        /// 引数つきコンストラクタ
        /// </summary>
        /// <param name="name">パラメータ名</param>
        /// <param name="type">パラメータ型</param>
        public DBParameters(string name, DbType type)
        {
            this.ParameterName = name;
            this.DbType = type;
        }
    }
}
