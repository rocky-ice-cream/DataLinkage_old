using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MasterDataLinkage
{

    /// <summary>
    /// DB�̃p�����[�^�N���X
    /// </summary>
    /// <remarks>
    /// �ʂ�Entity.cs�ɂ܂Ƃ߂Ă��ǂ��������ǐ����グ��ׁA�ʃt�@�C����
    /// </remarks>
    public class DBParameters
    {
        /// <summary>
        /// �p�����[�^��
        /// </summary>
        public string ParameterName { get; set; }

        public DbType DbType { get; set; }

        /// <summary>
        /// �������R���X�g���N�^
        /// </summary>
        /// <param name="name">�p�����[�^��</param>
        /// <param name="type">�p�����[�^�^</param>
        public DBParameters(string name, DbType type)
        {
            this.ParameterName = name;
            this.DbType = type;
        }
    }
}
