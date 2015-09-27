using MasterDataLinkage;
using System.Data;

public partial class StoredProcedures
{
    /// <summary>
    /// 基底のプロシージャ
    /// </summary>
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void import_Customer ()
    {
        Entity entity = new EntCustomer();
        entity.SourceTable = "[dbo].[tmp_dtb_customer]";
        entity.DestTable = "[dbo].[dtb_customer_test]";

        entity.SourceSelectSQL = "select * from {0}";
        entity.DestSelectSQL = "select * from {0}";

        // SQL用のパラメータの設定
        entity.SetDBParameters("influx_source", DbType.String);
        entity.SetDBParameters("name01", DbType.String);
        entity.SetDBParameters("name02", DbType.String);
        entity.SetDBParameters("kana01", DbType.String);
        entity.SetDBParameters("kana02", DbType.String);
        entity.SetDBParameters("zipcode", DbType.String);
        entity.SetDBParameters("addr01", DbType.String);
        entity.SetDBParameters("addr02", DbType.String);
        entity.SetDBParameters("tel", DbType.String);
        entity.SetDBParameters("email", DbType.String);
        entity.SetDBParameters("note", DbType.String);
        entity.SetDBParameters("customer_id", DbType.Int32);
        entity.SetDBParameters("pref", DbType.Int16);
        entity.SetDBParameters("sex", DbType.Int16);
        entity.SetDBParameters("job", DbType.Int16);
        entity.SetDBParameters("birth", DbType.Int32);
        entity.SetDBParameters("mailmaga_flg", DbType.Int16);
        entity.SetDBParameters("first_buy_date", DbType.Int32);
        entity.SetDBParameters("last_buy_date", DbType.Int32);
        entity.SetDBParameters("point", DbType.Int32);
        entity.SetDBParameters("use_point", DbType.Int32);
        entity.SetDBParameters("coupon", DbType.Int32);
        entity.SetDBParameters("status", DbType.Int16);
        entity.SetDBParameters("create_date", DbType.Int32);
        entity.SetDBParameters("create_time", DbType.Int32);
        entity.SetDBParameters("create_user", DbType.Int32);
        entity.SetDBParameters("update_date", DbType.Int32);
        entity.SetDBParameters("update_time", DbType.Int32);
        entity.SetDBParameters("update_user", DbType.Int32);
        entity.SetDBParameters("buy_times", DbType.Int32);
        entity.SetDBParameters("common_no", DbType.Int32);
        entity.SetDBParameters("del_flg", DbType.Int16);
        entity.SetDBParameters("buy_total", DbType.Int16);

        // 処理用クラス
        BizWorker customer = new BizWorker(entity);

        // 処理の実行
        customer.Worker();
    }

}
