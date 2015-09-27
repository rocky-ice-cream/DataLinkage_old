using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Text;


public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void import_Order ()
    {



        //conn.Open();
        //StringBuilder strcommand = new StringBuilder("select * into ");
        //strcommand.Append("#" + tblname + " from");
        //strcommand.Append(" " + tblname);

        ////自動更新用（今はまだ作らない）
        //SqlCommandBuilder cmd_build = new SqlCommandBuilder();

        ////テスト的に一時テーブルに退避
        //SqlCommand cmd = new SqlCommand(strcommand.ToString(), conn);
        //SqlDataReader dr = cmd.ExecuteReader();
        //SqlContext.Pipe.Send(dr);


        //dr.Close();
        //strcommand.Clear();

        //strcommand = new StringBuilder("insert into ");
        //strcommand.Append(tblname + "2 ");
        //strcommand.Append("select * from #" + tblname);

        ////テスト的に一時テーブルからワークテーブルに退避
        //cmd = new SqlCommand(strcommand.ToString(), conn);
        //dr = cmd.ExecuteReader();
        //SqlContext.Pipe.Send(dr);

        //dr.Close();

    }
}
