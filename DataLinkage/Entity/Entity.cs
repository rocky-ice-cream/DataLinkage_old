using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MasterDataLinkage
{
    public class Entity : IDisposable
    {
        // Dispose したかどうか 
        private bool _disposed = false;

        /// <summary>
        /// 参照元テーブル
        /// </summary>
        public string SourceTable { get; set; }

        /// <summary>
        /// 参照先テーブル
        /// </summary>
        public string DestTable { get; set; }

        /// <summary>
        /// 参照元検索SQL
        /// </summary>
        public string SourceSelectSQL { set; 
            get {
                //三項演算子 テーブル名があれば、SQL文を返す、なければ何もなし
                return (SourceTable != string.Empty ? 
                    string.Format(SourceSelectSQL,SourceTable):
                    string.Empty
                    );
            } 
        }

        /// <summary>
        /// 参照先検索SQL
        /// </summary>
        public string DestSelectSQL { set;
            get
            {
                //三項演算子 テーブル名があれば、SQL文を返す、なければ何もなし
                return (DestTable != string.Empty ?
                    string.Format(DestSelectSQL, DestTable) :
                    string.Empty
                    );
            } 
        }

        /// <summary>
        /// 参照先挿入SQL
        /// </summary>
        public string DestInsertSQL { set; }

        /// <summary>
        /// 参照先更新SQL
        /// </summary>
        public string DestUpdateSQL { set; }

        /// <summary>
        /// パラメータリスト
        /// </summary>
        public List<DBParameters> DbParamList = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Entity(){
            this.DbParamList = new List<DBParameters>(); 
        }

        /// <summary>
        /// DBパラメータの設定
        /// </summary>
        public bool SetDBParameters(string name,DbType type) {

            //Listに格納
            this.DbParamList.Add(new DBParameters(name, type));

            return true;
        }

        // IDisposable に必須のメソッドの実装 
        public void Dispose() { 
            Dispose(true); 
            // Dispose() によってリソースの解放を行ったので、 
            // GC での解放が必要が無いことを GC に通知します。 
            GC.SuppressFinalize(this); 
        }

        /// <summary>
        /// 解放処理の実行
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing) { 
	        // Dispose がまだ実行されていないときだけ実行 
            if (!_disposed)
            {
                // disposing が true の場合(Dispose() が実行された場合)は 
                // マネージリソースも解放します。 
                if (disposing)
                {
                    // マネージリソースの解放 
                    if (this.DbParamList != null)
                    {
                        this.DbParamList.Clear();
                        this.DbParamList = null;                       
                    }
                } 
                // アンマネージリソースの解放 
                this.DestTable = string.Empty;
                this.DestSelectSQL = string.Empty;
                this.DestInsertSQL = string.Empty;
                this.DestUpdateSQL = string.Empty;
                this.SourceTable = string.Empty;
                this.SourceSelectSQL = string.Empty;
                _disposed = true;
            }
        }
        
    }

}
