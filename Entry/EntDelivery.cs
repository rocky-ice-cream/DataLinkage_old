using System.Data;
using System.Text;

namespace DataLinkage
{
    /// <summary>
    /// Delivery�p�N���X
    /// </summary>
    sealed class EntDelivery : Entry
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public EntDelivery()
        {

        }

        /// <summary>
        /// DB�ڑ��Ɋւ������ݒ�
        /// </summary>
        public override void SetDbAccessInfo() {

            this.SourceTable = "[dbo].[tmp_mtb_deliv]";
            this.DestTable = "[dbo].[mtb_deliv]";

            // SQL�p�̃p�����[�^�̐ݒ�
            this.SetDBParameters("deliv_id", DbType.Int16);
            this.SetDBParameters("product_type_id", DbType.Int32);
            this.SetDBParameters("name", DbType.String);
            this.SetDBParameters("service_name", DbType.String);
            this.SetDBParameters("remark", DbType.String);
            this.SetDBParameters("confirm_url", DbType.String);
            this.SetDBParameters("rank", DbType.Int16);
            this.SetDBParameters("del_flg", DbType.Int16);
            this.SetDBParameters("status", DbType.Int16);
            this.SetDBParameters("create_date", DbType.Int32);
            this.SetDBParameters("create_time", DbType.Int32);
            this.SetDBParameters("create_user", DbType.Int32);
            this.SetDBParameters("update_date", DbType.Int32);
            this.SetDBParameters("update_time", DbType.Int32);
            this.SetDBParameters("update_user", DbType.Int32);
        }

        /// <summary>
        /// �Q�ƌ�Select����Ԃ�
        /// </summary>
        /// <returns>�Q�ƌ�����SQL</returns>
        public override string GetSourceSelectCommandText()
        {
            StringBuilder strSelectCommand = new StringBuilder();

            strSelectCommand.Append("SELECT [deliv_id]");
            strSelectCommand.Append("      ,[product_type_id]");
            strSelectCommand.Append("      ,[name]");
            strSelectCommand.Append("      ,[service_name]");
            strSelectCommand.Append("      ,[remark]");
            strSelectCommand.Append("      ,[confirm_url]");
            strSelectCommand.Append("      ,[rank]");
            strSelectCommand.Append("      ,[status]");
            strSelectCommand.Append("      ,[del_flg]");
            strSelectCommand.Append("      ,[create_date]");
            strSelectCommand.Append("      ,[create_time]");
            strSelectCommand.Append("      ,[create_user]");
            strSelectCommand.Append("      ,[update_date]");
            strSelectCommand.Append("      ,[update_time]");
            strSelectCommand.Append("      ,[update_user]");
            strSelectCommand.Append("FROM ");
            strSelectCommand.Append(SourceTable);
            strSelectCommand.Append(" WITH (NOLOCK) ");

            return strSelectCommand.ToString();
        }

        /// <summary>
        /// �Q�Ɛ�Select����Ԃ�
        /// </summary>
        /// <returns>�Q�Ɛ�SQL</returns>
        public override string GetDestSelectCommandText()
        {
            StringBuilder strSelectCommand = new StringBuilder();
            strSelectCommand.Append("SELECT [deliv_id]");
            strSelectCommand.Append("      ,[product_type_id]");
            strSelectCommand.Append("      ,[name]");
            strSelectCommand.Append("      ,[service_name]");
            strSelectCommand.Append("      ,[remark]");
            strSelectCommand.Append("      ,[confirm_url]");
            strSelectCommand.Append("      ,[rank]");
            strSelectCommand.Append("      ,[status]");
            strSelectCommand.Append("      ,[del_flg]");
            strSelectCommand.Append("      ,[create_date]");
            strSelectCommand.Append("      ,[create_time]");
            strSelectCommand.Append("      ,[create_user]");
            strSelectCommand.Append("      ,[update_date]");
            strSelectCommand.Append("      ,[update_time]");
            strSelectCommand.Append("      ,[update_user]");
            strSelectCommand.Append("FROM ");
            strSelectCommand.Append(DestTable);

            return strSelectCommand.ToString();
        }
        
        /// <summary>
        /// InsertSQL�icommandText�j��Ԃ�
        /// </summary>
        /// <returns>�Q�Ɛ�}��SQL</returns>
        public override string GetInsertCommandText()
        {
            StringBuilder strInsertCommand = new StringBuilder();

            strInsertCommand.Append("INSERT INTO ");
            strInsertCommand.Append(DestTable);
            strInsertCommand.Append("           ([deliv_id]");
            strInsertCommand.Append("           ,[product_type_id]");
            strInsertCommand.Append("           ,[name]");
            strInsertCommand.Append("           ,[service_name]");
            strInsertCommand.Append("           ,[remark]");
            strInsertCommand.Append("           ,[confirm_url]");
            strInsertCommand.Append("           ,[rank]");
            strInsertCommand.Append("           ,[status]");
            strInsertCommand.Append("           ,[del_flg]");
            strInsertCommand.Append("           ,[create_date]");
            strInsertCommand.Append("           ,[create_time]");
            strInsertCommand.Append("           ,[create_user]");
            strInsertCommand.Append("           ,[update_date]");
            strInsertCommand.Append("           ,[update_time]");
            strInsertCommand.Append("           ,[update_user])");
            strInsertCommand.Append("     VALUES");
            strInsertCommand.Append("           (@deliv_id");
            strInsertCommand.Append("           ,@product_type_id");
            strInsertCommand.Append("           ,@name");
            strInsertCommand.Append("           ,@service_name");
            strInsertCommand.Append("           ,@remark");
            strInsertCommand.Append("           ,@confirm_url");
            strInsertCommand.Append("           ,@rank");
            strInsertCommand.Append("           ,@status");
            strInsertCommand.Append("           ,@del_flg");
            strInsertCommand.Append("           ,@create_date");
            strInsertCommand.Append("           ,@create_time");
            strInsertCommand.Append("           ,@create_user");
            strInsertCommand.Append("           ,@update_date");
            strInsertCommand.Append("           ,@update_time");
            strInsertCommand.Append("           ,@update_user)");
            return strInsertCommand.ToString();
        }

        /// <summary>
        /// UpdateSQL�icommandText�j��Ԃ�
        /// </summary>
        /// <returns>�Q�Ɛ�X�VSQL</returns>
        public override string GetUpdateCommandText()
        {
            StringBuilder strUpdateCommand = new StringBuilder();

            strUpdateCommand.Append("UPDATE ");
            strUpdateCommand.Append(DestTable);
            strUpdateCommand.Append("   SET [deliv_id] = @deliv_id");
            strUpdateCommand.Append("      ,[product_type_id] = @product_type_id");
            strUpdateCommand.Append("      ,[name] = @name");
            strUpdateCommand.Append("      ,[service_name] = @service_name");
            strUpdateCommand.Append("      ,[remark] = @remark");
            strUpdateCommand.Append("      ,[confirm_url] = @confirm_url");
            strUpdateCommand.Append("      ,[rank] = @rank");
            strUpdateCommand.Append("      ,[status] = @status");
            strUpdateCommand.Append("      ,[del_flg] = @del_flg");
            strUpdateCommand.Append("      ,[create_date] = @create_date");
            strUpdateCommand.Append("      ,[create_time] = @create_time");
            strUpdateCommand.Append("      ,[create_user] = @create_user");
            strUpdateCommand.Append("      ,[update_date] = @update_date");
            strUpdateCommand.Append("      ,[update_time] = @update_time");
            strUpdateCommand.Append("      ,[update_user] = @update_user");
            
            return strUpdateCommand.ToString();
        }
    }
}
