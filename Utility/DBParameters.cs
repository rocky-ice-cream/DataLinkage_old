using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataLinkage
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

        /// <summary>
        /// �p�����[�^�̎��
        /// </summary>
        public DbType DbType { get; set; }

        /// <summary>
        /// �p�����[�^���v���C�}���L�[���ǂ���
        /// </summary>
        public bool Primary { get; set; } 

        /// <summary>
        /// �������R���X�g���N�^
        /// </summary>
        /// <param name="name">�p�����[�^��</param>
        /// <param name="type">�p�����[�^�^</param>
        /// <param name="primary">�v���C�}���L�[���ǂ���</param>
        public DBParameters(string name, DbType type,bool primary)
        {
            this.ParameterName = name;
            this.DbType = type;
            this.Primary = primary;
        }
    }
}
