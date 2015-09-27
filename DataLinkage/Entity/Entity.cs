using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MasterDataLinkage
{
    public class Entity : IDisposable
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
        /// �Q�ƌ�����SQL
        /// </summary>
        public string SourceSelectSQL { set; 
            get {
                //�O�����Z�q �e�[�u����������΁ASQL����Ԃ��A�Ȃ���Ή����Ȃ�
                return (SourceTable != string.Empty ? 
                    string.Format(SourceSelectSQL,SourceTable):
                    string.Empty
                    );
            } 
        }

        /// <summary>
        /// �Q�Ɛ挟��SQL
        /// </summary>
        public string DestSelectSQL { set;
            get
            {
                //�O�����Z�q �e�[�u����������΁ASQL����Ԃ��A�Ȃ���Ή����Ȃ�
                return (DestTable != string.Empty ?
                    string.Format(DestSelectSQL, DestTable) :
                    string.Empty
                    );
            } 
        }

        /// <summary>
        /// �Q�Ɛ�}��SQL
        /// </summary>
        public string DestInsertSQL { set; }

        /// <summary>
        /// �Q�Ɛ�X�VSQL
        /// </summary>
        public string DestUpdateSQL { set; }

        /// <summary>
        /// �p�����[�^���X�g
        /// </summary>
        public List<DBParameters> DbParamList = null;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public Entity(){
            this.DbParamList = new List<DBParameters>(); 
        }

        /// <summary>
        /// DB�p�����[�^�̐ݒ�
        /// </summary>
        public bool SetDBParameters(string name,DbType type) {

            //List�Ɋi�[
            this.DbParamList.Add(new DBParameters(name, type));

            return true;
        }

        // IDisposable �ɕK�{�̃��\�b�h�̎��� 
        public void Dispose() { 
            Dispose(true); 
            // Dispose() �ɂ���ă��\�[�X�̉�����s�����̂ŁA 
            // GC �ł̉�����K�v���������Ƃ� GC �ɒʒm���܂��B 
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
                // �}�l�[�W���\�[�X��������܂��B 
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
