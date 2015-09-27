using Microsoft.SqlServer.Server;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace MasterDataLinkage
{
    /// <summary>
    /// 顧客テーブル用の処理
    /// </summary>
    public class BizWorker
    {
        Entity myEntity = null;

        ///// <summary>
        ///// 参照元テーブル名
        ///// </summary>
        //private string m_SourceTable = "[dbo].[tmp_dtb_customer]";

        ///// <summary>
        ///// 参照先テーブル名
        ///// </summary>
        //private string m_DestTable = "[dbo].[dtb_customer_test]";

        ///// <summary>
        ///// 出力元SQL
        ///// </summary>
        //private StringBuilder m_SourceSQL = new StringBuilder(); 

        /////// <summary>
        /////// 出力先SQL
        /////// </summary>
        //private StringBuilder m_DestSQL = new StringBuilder();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BizWorker()
        {
            //m_SourceSQL.AppendFormat("select * from {0}",m_SourceTable);
            //m_DestSQL.AppendFormat("select * from {0}", m_DestTable);
        }

        public BizWorker(Entity _myEntity)
        {
            //実態を渡す
            myEntity = _myEntity;

            //m_SourceSQL.AppendFormat("select * from {0}", m_SourceTable);
            //m_DestSQL.AppendFormat("select * from {0}", m_DestTable);
        }


        /// <summary>
        /// 主処理関数
        /// </summary>
        public void Worker()
        {

            try
            {
                DataTable tempTable = new DataTable();

                // コードをここに記述してください
                using (SqlConnection conn =
                    new SqlConnection(Utility.CONNECTION_STRING))
                {
                    // オープン
                    conn.Open();

                    //TEMPテーブルを取得
                    using (SqlCommand selectCommand
                        = new SqlCommand(myEntity.SourceSelectSQL, conn))
                    {
                        // TEMPテーブルの取得
                        using (SqlDataReader reader = selectCommand.ExecuteReader())
                        {
                            //TEMPテーブルの全取得
                            tempTable.Load(reader);
                        }
                    }

                    //更新処理用テーブル
                    using (SqlDataAdapter dtadapbter
                        = new SqlDataAdapter(myEntity.DestSelectSQL, conn))
                    {
                        //Insert用のパラメータを作成
                        dtadapbter.InsertCommand = SetSqlCommand(Utility.FUNC_TYPE.INSERT, conn);

                        //Update用のパラメータを作成
                        dtadapbter.UpdateCommand = SetSqlCommand(Utility.FUNC_TYPE.UPDATE, conn);

                        DataSet dataset = new DataSet();
                        dtadapbter.Fill(dataset, myEntity.DestTable);

                        DataTable distTable = new DataTable();
                        distTable = dataset.Tables[myEntity.DestTable];
                        distTable.PrimaryKey
                        = new DataColumn[] { distTable.Columns["common_no"] };

                        //更新系の処理
                        Upsert(tempTable, distTable);

                        // updeteの実施
                        dtadapbter.Update(distTable);
                    }
                }
                SqlContext.Pipe.Send("Mergeの正常終了");
            }
            catch (SqlException e)
            {
                SqlContext.Pipe.Send(e.Message);
                throw e;
            }
            catch (Exception e) 
            {
                SqlContext.Pipe.Send(e.Message);
                throw e;
            }
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="_sourceTbl"></param>
        /// <param name="_distTbl"></param>
        /// <remarks>
        /// </remarks>
        private void Upsert(DataTable _sourceTbl, DataTable _distTbl)
        {
            // TEMPテーブル文の処理
            foreach (DataRow row in _sourceTbl.Rows)
            {
                // 挿入フラグ
                bool insertFlg = false;

                DataRow findRow = _distTbl.Rows.Find(row["common_no"]);

                // not matched のみ
                if (findRow == null)
                {
                    insertFlg = true;
                    findRow = _distTbl.NewRow();
                }

                // 行の追加・更新
                // 条件が必要なときはここを修正
                OverWriteRows(findRow, row);

                // 新規挿入の場合
                if (insertFlg)
                {
                    _distTbl.Rows.Add(findRow);
                }

            }
        }

        #region このあたりの処理はテーブルごと機能ごとに異なる為、ここのロジックを個別にすれば、共通化できそう。

        /// <summary>
        /// 参照先データの上書き
        /// </summary>
        /// <param name="_findRow">参照先テーブル</param>
        /// <param name="_row">参照元テーブル</param>
        /// <remarks>
        /// 該当レコードがない場合は新規行、
        /// すでにある場合は既存行の更新を行う
        /// </remarks>
        private void OverWriteRows(DataRow _findRow,DataRow _row)
        {
            _findRow["influx_source"] = _row["influx_source"];
            _findRow["customer_id"] = _row["customer_id"];
            _findRow["name01"] = _row["name01"];
            _findRow["name02"] = _row["name02"];
            _findRow["kana01"] = _row["kana01"];
            _findRow["kana02"] = _row["kana02"];
            _findRow["zipcode"] = _row["zipcode"];
            _findRow["pref"] = _row["pref"];
            _findRow["addr01"] = _row["addr01"];
            _findRow["addr02"] = _row["addr02"];
            _findRow["tel"] = _row["tel"];
            _findRow["sex"] = _row["sex"];
            _findRow["job"] = _row["job"];
            _findRow["birth"] = _row["birth"];
            _findRow["email"] = _row["email"];
            _findRow["mailmaga_flg"] = _row["mailmaga_flg"];
            _findRow["first_buy_date"] = _row["first_buy_date"];
            _findRow["last_buy_date"] = _row["last_buy_date"];
            _findRow["buy_times"] = _row["buy_times"];
            _findRow["buy_total"] = _row["buy_total"];
            _findRow["point"] = _row["point"];
            _findRow["use_point"] = _row["use_point"];
            _findRow["coupon"] = _row["coupon"];
            _findRow["note"] = _row["note"];
            _findRow["status"] = _row["status"];
            _findRow["create_date"] = _row["create_date"];
            _findRow["create_time"] = _row["create_time"];
            _findRow["create_user"] = _row["create_user"];
            _findRow["update_date"] = _row["update_date"];
            _findRow["update_time"] = _row["update_time"];
            _findRow["update_user"] = _row["update_user"];
            _findRow["del_flg"] = _row["del_flg"];
            _findRow["common_no"] = _row["common_no"];
        }

        /// <summary>
        /// SQLパラメータを設定する
        /// </summary>
        /// <param name="_command"></param>
        /// <returns></returns>
        private SqlCommand SetSqlCommand(Utility.FUNC_TYPE _type, SqlConnection _conn)
        {
            //SQL文の作成
            SqlCommand sqlCommand = _conn.CreateCommand();

            switch (_type)
            {
                case Utility.FUNC_TYPE.INSERT:

                    sqlCommand.CommandText = GetInsertCommandText();

                    break;
                case Utility.FUNC_TYPE.UPDATE:

                    sqlCommand.CommandText = GetUpdateCommandText();
                    break;

                default:
                    return null;
            }


            ////パラメータの作成
            SqlParameter param = sqlCommand.CreateParameter();

            //パラメータ全てを設定
            foreach (DBParameters list in myEntity.DbParamList)
            {
                param = sqlCommand.CreateParameter();
                param.ParameterName = string.Format("@{0}",list.ParameterName);
                param.DbType = list.DbType;
                param.SourceColumn = list.ParameterName;
                sqlCommand.Parameters.Add(param);
            }

            //param = sqlCommand.CreateParameter();
            //param.ParameterName = "@influx_source";
            //param.DbType = DbType.String;
            //param.SourceColumn = "influx_source";
            //sqlCommand.Parameters.Add(param);

            //param = sqlCommand.CreateParameter();
            //param.ParameterName = "@name01";
            //param.DbType = DbType.String;
            //param.SourceColumn = "name01";
            //sqlCommand.Parameters.Add(param);

            //param = sqlCommand.CreateParameter();
            //param.ParameterName = "@name02";
            //param.DbType = DbType.String;
            //param.SourceColumn = "name02";
            //sqlCommand.Parameters.Add(param);

            //param = sqlCommand.CreateParameter();
            //param.ParameterName = "@kana01";
            //param.DbType = DbType.String;
            //param.SourceColumn = "kana01";
            //sqlCommand.Parameters.Add(param);

            //param = sqlCommand.CreateParameter();
            //param.ParameterName = "@kana02";
            //param.DbType = DbType.String;
            //param.SourceColumn = "kana02";
            //sqlCommand.Parameters.Add(param);

            //param = sqlCommand.CreateParameter();
            //param.ParameterName = "@zipcode";
            //param.DbType = DbType.String;
            //param.SourceColumn = "zipcode";
            //sqlCommand.Parameters.Add(param);

            //param = sqlCommand.CreateParameter();
            //param.ParameterName = "@addr01";
            //param.DbType = DbType.String;
            //param.SourceColumn = "addr01";
            //sqlCommand.Parameters.Add(param);

            //param = sqlCommand.CreateParameter();
            //param.ParameterName = "@addr02";
            //param.DbType = DbType.String;
            //param.SourceColumn = "addr02";
            //sqlCommand.Parameters.Add(param);

            //param = sqlCommand.CreateParameter();
            //param.ParameterName = "@tel";
            //param.DbType = DbType.String;
            //param.SourceColumn = "tel";
            //sqlCommand.Parameters.Add(param);

            //param = sqlCommand.CreateParameter();
            //param.ParameterName = "@email";
            //param.DbType = DbType.String;
            //param.SourceColumn = "email";
            //sqlCommand.Parameters.Add(param);

            //param = sqlCommand.CreateParameter();
            //param.ParameterName = "@note";
            //param.DbType = DbType.String;
            //param.SourceColumn = "note";
            //sqlCommand.Parameters.Add(param);

            //param = sqlCommand.CreateParameter();
            //param.ParameterName = "@customer_id";
            //param.DbType = DbType.Int32;
            //param.SourceColumn = "customer_id";
            //sqlCommand.Parameters.Add(param);

            //param = sqlCommand.CreateParameter();
            //param.ParameterName = "@pref";
            //param.DbType = DbType.Int16;
            //param.SourceColumn = "pref";
            //sqlCommand.Parameters.Add(param);

            //param = sqlCommand.CreateParameter();
            //param.ParameterName = "@sex";
            //param.DbType = DbType.Int16;
            //param.SourceColumn = "sex";
            //sqlCommand.Parameters.Add(param);

            //param = sqlCommand.CreateParameter();
            //param.ParameterName = "@job";
            //param.DbType = DbType.Int16;
            //param.SourceColumn = "job";
            //sqlCommand.Parameters.Add(param);

            //param = sqlCommand.CreateParameter();
            //param.ParameterName = "@birth";
            //param.DbType = DbType.Int32;
            //param.SourceColumn = "birth";
            //sqlCommand.Parameters.Add(param);

            //param = sqlCommand.CreateParameter();
            //param.ParameterName = "@mailmaga_flg";
            //param.DbType = DbType.Int16;
            //param.SourceColumn = "mailmaga_flg";
            //sqlCommand.Parameters.Add(param);

            //param = sqlCommand.CreateParameter();
            //param.ParameterName = "@first_buy_date";
            //param.DbType = DbType.Int32;
            //param.SourceColumn = "first_buy_date";
            //sqlCommand.Parameters.Add(param);

            //param = sqlCommand.CreateParameter();
            //param.ParameterName = "@last_buy_date";
            //param.DbType = DbType.Int32;
            //param.SourceColumn = "last_buy_date";
            //sqlCommand.Parameters.Add(param);

            //param = sqlCommand.CreateParameter();
            //param.ParameterName = "@point";
            //param.DbType = DbType.Int32;
            //param.SourceColumn = "point";
            //sqlCommand.Parameters.Add(param);

            //param = sqlCommand.CreateParameter();
            //param.ParameterName = "@use_point";
            //param.DbType = DbType.Int32;
            //param.SourceColumn = "use_point";
            //sqlCommand.Parameters.Add(param);

            //param = sqlCommand.CreateParameter();
            //param.ParameterName = "@coupon";
            //param.DbType = DbType.Int32;
            //param.SourceColumn = "coupon";
            //sqlCommand.Parameters.Add(param);

            //param = sqlCommand.CreateParameter();
            //param.ParameterName = "@status";
            //param.DbType = DbType.Int16;
            //param.SourceColumn = "status";
            //sqlCommand.Parameters.Add(param);

            //param = sqlCommand.CreateParameter();
            //param.ParameterName = "@create_date";
            //param.DbType = DbType.Int32;
            //param.SourceColumn = "create_date";
            //sqlCommand.Parameters.Add(param);

            //param = sqlCommand.CreateParameter();
            //param.ParameterName = "@create_time";
            //param.DbType = DbType.Int32;
            //param.SourceColumn = "create_time";
            //sqlCommand.Parameters.Add(param);

            //param = sqlCommand.CreateParameter();
            //param.ParameterName = "@create_user";
            //param.DbType = DbType.Int32;
            //param.SourceColumn = "create_user";
            //sqlCommand.Parameters.Add(param);

            //param = sqlCommand.CreateParameter();
            //param.ParameterName = "@update_date";
            //param.DbType = DbType.Int32;
            //param.SourceColumn = "update_date";
            //sqlCommand.Parameters.Add(param);

            //param = sqlCommand.CreateParameter();
            //param.ParameterName = "@update_time";
            //param.DbType = DbType.Int32;
            //param.SourceColumn = "update_time";
            //sqlCommand.Parameters.Add(param);

            //param = sqlCommand.CreateParameter();
            //param.ParameterName = "@update_user";
            //param.DbType = DbType.Int32;
            //param.SourceColumn = "update_user";
            //sqlCommand.Parameters.Add(param);

            //param = sqlCommand.CreateParameter();
            //param.ParameterName = "@del_flg";
            //param.DbType = DbType.Int16;
            //param.SourceColumn = "del_flg";
            //sqlCommand.Parameters.Add(param);

            //param = sqlCommand.CreateParameter();
            //param.ParameterName = "@buy_times";
            //param.DbType = DbType.Int32;
            //param.SourceColumn = "buy_times";
            //sqlCommand.Parameters.Add(param);

            //param = sqlCommand.CreateParameter();
            //param.ParameterName = "@buy_total";
            //param.DbType = DbType.Int16;
            //param.SourceColumn = "buy_total";
            //sqlCommand.Parameters.Add(param);

            //param = sqlCommand.CreateParameter();
            //param.ParameterName = "@common_no";
            //param.DbType = DbType.Int32;
            //param.SourceColumn = "common_no";
            //sqlCommand.Parameters.Add(param);

            return sqlCommand;
        }

        /// <summary>
        /// InsertSQL（commandText）を返す
        /// </summary>
        /// <returns>CommandText</returns>
        private string GetInsertCommandText()
        {
            StringBuilder strInsertCommand = new StringBuilder();

            strInsertCommand.Append("INSERT INTO ");
            strInsertCommand.Append(myEntity.DestTable);
            strInsertCommand.Append("           ([influx_source]");
            //strInsertCommand.Append("           ,[customer_id]");
            strInsertCommand.Append("           ,[name01]");
            strInsertCommand.Append("           ,[name02]");
            strInsertCommand.Append("           ,[kana01]");
            strInsertCommand.Append("           ,[kana02]");
            strInsertCommand.Append("           ,[zipcode]");
            strInsertCommand.Append("           ,[pref]");
            strInsertCommand.Append("           ,[addr01]");
            strInsertCommand.Append("           ,[addr02]");
            strInsertCommand.Append("           ,[tel]");
            strInsertCommand.Append("           ,[sex]");
            strInsertCommand.Append("           ,[job]");
            strInsertCommand.Append("           ,[birth]");
            strInsertCommand.Append("           ,[email]");
            strInsertCommand.Append("           ,[mailmaga_flg]");
            strInsertCommand.Append("           ,[first_buy_date]");
            strInsertCommand.Append("           ,[last_buy_date]");
            strInsertCommand.Append("           ,[buy_times]");
            strInsertCommand.Append("           ,[buy_total]");
            strInsertCommand.Append("           ,[point]");
            strInsertCommand.Append("           ,[use_point]");
            strInsertCommand.Append("           ,[coupon]");
            strInsertCommand.Append("           ,[note]");
            strInsertCommand.Append("           ,[status]");
            strInsertCommand.Append("           ,[create_date]");
            strInsertCommand.Append("           ,[create_time]");
            strInsertCommand.Append("           ,[create_user]");
            strInsertCommand.Append("           ,[update_date]");
            strInsertCommand.Append("           ,[update_time]");
            strInsertCommand.Append("           ,[update_user]");
            strInsertCommand.Append("           ,[del_flg]");
            strInsertCommand.Append("           ,[common_no])"); //テスト的に共通番号列を作成
            strInsertCommand.Append("     VALUES");
            strInsertCommand.Append("           (@influx_source");
            //strInsertCommand.Append("           ,@customer_id");
            strInsertCommand.Append("           ,@name01");
            strInsertCommand.Append("           ,@name02");
            strInsertCommand.Append("           ,@kana01");
            strInsertCommand.Append("           ,@kana02");
            strInsertCommand.Append("           ,@zipcode");
            strInsertCommand.Append("           ,@pref");
            strInsertCommand.Append("           ,@addr01");
            strInsertCommand.Append("           ,@addr02");
            strInsertCommand.Append("           ,@tel");
            strInsertCommand.Append("           ,@sex");
            strInsertCommand.Append("           ,@job");
            strInsertCommand.Append("           ,@birth");
            strInsertCommand.Append("           ,@email");
            strInsertCommand.Append("           ,@mailmaga_flg");
            strInsertCommand.Append("           ,@first_buy_date");
            strInsertCommand.Append("           ,@last_buy_date");
            strInsertCommand.Append("           ,@buy_times");
            strInsertCommand.Append("           ,@buy_total");
            strInsertCommand.Append("           ,@point");
            strInsertCommand.Append("           ,@use_point");
            strInsertCommand.Append("           ,@coupon");
            strInsertCommand.Append("           ,@note");
            strInsertCommand.Append("           ,@status");
            strInsertCommand.Append("           ,@create_date");
            strInsertCommand.Append("           ,@create_time");
            strInsertCommand.Append("           ,@create_user");
            strInsertCommand.Append("           ,@update_date");
            strInsertCommand.Append("           ,@update_time");
            strInsertCommand.Append("           ,@update_user");
            strInsertCommand.Append("           ,@del_flg");
            strInsertCommand.Append("           ,@common_no)");//テスト的に共通番号列を作成
            return strInsertCommand.ToString();
        }

        /// <summary>
        /// UpdateSQL（commandText）を返す
        /// </summary>
        /// <returns>CommandText</returns>
        private string GetUpdateCommandText()
        {
            StringBuilder strUpdateCommand = new StringBuilder();

            strUpdateCommand.Append("UPDATE ");
            strUpdateCommand.Append(myEntity.DestTable);
            strUpdateCommand.Append("   SET [influx_source] = @influx_source");
            strUpdateCommand.Append("      ,[name01] = @name01");
            strUpdateCommand.Append("      ,[name02] = @name02");
            strUpdateCommand.Append("      ,[kana01] = @kana01");
            strUpdateCommand.Append("      ,[kana02] = @kana02");
            strUpdateCommand.Append("      ,[zipcode] = @zipcode");
            strUpdateCommand.Append("      ,[pref] = @pref");
            strUpdateCommand.Append("      ,[addr01] = @addr01");
            strUpdateCommand.Append("      ,[addr02] = @addr02");
            strUpdateCommand.Append("      ,[tel] = @tel");
            strUpdateCommand.Append("      ,[sex] = @sex");
            strUpdateCommand.Append("      ,[job] = @job");
            strUpdateCommand.Append("      ,[birth] = @birth");
            strUpdateCommand.Append("      ,[email] = @email");
            strUpdateCommand.Append("      ,[mailmaga_flg] = @mailmaga_flg");
            strUpdateCommand.Append("      ,[first_buy_date] = @first_buy_date");
            strUpdateCommand.Append("      ,[last_buy_date] = @last_buy_date");
            strUpdateCommand.Append("      ,[buy_times] = @buy_times");
            strUpdateCommand.Append("      ,[buy_total] = @buy_total");
            strUpdateCommand.Append("      ,[point] = @point");
            strUpdateCommand.Append("      ,[use_point] = @use_point");
            strUpdateCommand.Append("      ,[coupon] = @coupon");
            strUpdateCommand.Append("      ,[note] = @note");
            strUpdateCommand.Append("      ,[status] = @status");
            strUpdateCommand.Append("      ,[create_date] = @create_date");
            strUpdateCommand.Append("      ,[create_time] = @create_time");
            strUpdateCommand.Append("      ,[create_user] = @create_user");
            strUpdateCommand.Append("      ,[update_date] = @update_date");
            strUpdateCommand.Append("      ,[update_time] = @update_time");
            strUpdateCommand.Append("      ,[update_user] = @update_user");
            strUpdateCommand.Append("      ,[del_flg] = @del_flg");
            strUpdateCommand.Append("      ,[common_no] = @common_no");//テスト的に共通番号列を作成

            return strUpdateCommand.ToString();
        }


        #endregion
    }
}

                    
