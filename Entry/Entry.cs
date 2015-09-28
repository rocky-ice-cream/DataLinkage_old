using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataLinkage
{
    /// <summary>
    /// DB�������s�p�N���X�i���N���X�j
    /// </summary>
    /// <remarks>
    /// DB�����Ɋւ���SQL�ATABLE���A�p�����[�^�Ȃǂ�ݒ肷��N���X
    /// SQL�Ȃǂ͏����ŗL�ɂȂ�ׁA�h���N���X���ŋL�q����B
    /// </remarks>
    public class Entry : IDisposable
    {
        // Dispose �������ǂ��� 
        private bool _disposed = false;

        /// <summary>
        /// �Q�ƌ��e�[�u��
        /// </summary>
        public string SourceTable { get; set; }

        /// <summary>
        /// �Q�Ɛ�e�[�u��
        /// </summary>
        public string DestTable { get; set; }

        /// <summary>
        /// �p�����[�^���X�g
        /// </summary>
        public List<DBParameters> DbParamList = null;

        /// <summary>
        /// �g�����U�N�V�����������s�����̔���
        /// </summary>
        public bool IsTransaction { get; set; }
                
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public Entry(){
            this.DbParamList = new List<DBParameters>();
            this.IsTransaction = false;
            this._disposed = false;
            this.SourceTable = string.Empty;
            this.DestTable = string.Empty;
        }

        /// <summary>
        /// DB�ڑ��Ɋւ������ݒ�
        /// </summary>
        public virtual void SetDbAccessInfo() { }

        /// <summary>
        /// DB�p�����[�^�̐ݒ�
        /// </summary>
        /// <param name="name">�p�����[�^��</param>
        /// <param name="type">�p�����[�^�^</param>
        /// <param name="primary">�v���C�}���L�[���ǂ���</param>
        /// <returns></returns>
        protected bool SetDBParameters(string name,DbType type, bool primary = false) {

            //List�Ɋi�[
            this.DbParamList.Add(new DBParameters(name, type, primary));

            return true;
        }

        // IDisposable �ɕK�{�̃��\�b�h�̎��� 
        public void Dispose() { 
            Dispose(true); 
            // Dispose() �ɂ���ă��\�[�X�̉�����s�����̂ŁA 
            // GC �ł̉�����K�v���������Ƃ� GC �ɒʒm�B 
            GC.SuppressFinalize(this); 
        }

        /// <summary>
        /// ��������̎��s
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing) { 
	        // Dispose ���܂����s����Ă��Ȃ��Ƃ��������s 
            if (!_disposed)
            {
                // disposing �� true �̏ꍇ(Dispose() �����s���ꂽ�ꍇ)�� 
                // �}�l�[�W���\�[�X������B 
                if (disposing)
                {
                    // �}�l�[�W���\�[�X�̉�� 
                    if (this.DbParamList != null)
                    {
                        this.DbParamList.Clear();
                        this.DbParamList = null;                       
                    }
                } 
                // �A���}�l�[�W���\�[�X�̉�� 
                this.DestTable = string.Empty;
                this.SourceTable = string.Empty;
                _disposed = true;
            }
        }

        /// <summary>
        /// �Q�ƌ�Select����Ԃ�
        /// </summary>
        /// <returns>�Q�ƌ�����SQL</returns>
        public virtual string GetSourceSelectCommandText()
        {
            StringBuilder strSelectCommand = new StringBuilder();

            return strSelectCommand.ToString();
        }

        /// <summary>
        /// �Q�Ɛ�Select����Ԃ�
        /// </summary>
        /// <returns>�Q�Ɛ挟��SQL</returns>
        public virtual string GetDestSelectCommandText()
        {
            StringBuilder strSelectCommand = new StringBuilder();

            return strSelectCommand.ToString();
        }

        /// <summary>
        /// Update����Ԃ�
        /// </summary>
        /// <returns>�Q�Ɛ�X�VSQL</returns>
        public virtual string GetUpdateCommandText()
        {
            return string.Empty;
        }


        /// <summary>
        /// Insert����Ԃ�
        /// </summary>
        /// <returns>�Q�Ɛ�}��SQL</returns>
        public virtual string GetInsertCommandText()
        {
            return string.Empty;
        }
        
    }

}
