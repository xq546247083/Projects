using System;
using System.Text;

namespace HaoCodeBuilder.Business
{
    internal class Import
    {
        private IData.ICreateCode createInstance;
        public Import(Model.DatabaseType dataBaseType)
        {
            createInstance = Factory.Factory.CreateCreateCodeInstance(dataBaseType);
        }

        /// <summary>
        /// 得到实体层引用字符串
        /// </summary>
        /// <returns></returns>
        public string GetImport_Model()
        {
            StringBuilder import = new StringBuilder();
            import.Append("using System;" + Environment.NewLine);
            import.Append("using System.Data;" + Environment.NewLine);
            return import.ToString();
        }

        /// <summary>
        /// 得到实体层引用字符串
        /// </summary>
        /// <returns></returns>
        public string GetImport_Model_Inside()
        {
            StringBuilder import = new StringBuilder();
            import.Append("using Tool.CustomAttribute;" + Environment.NewLine);
            return import.ToString();
        }

        /// <summary>
        /// 数据层引用字符串
        /// </summary>
        /// <returns></returns>
        public string GetImport_Data()
        {
            StringBuilder import = new StringBuilder();
            import.Append("using System;" + Environment.NewLine);
            import.Append("using System.Data;" + Environment.NewLine);
            return import.ToString();
        }

        /// <summary>
        /// 数据层引用字符串
        /// </summary>
        /// <returns></returns>
        public string GetImport_Data_Inside()
        {
            StringBuilder import = new StringBuilder();
            import.Append("using " + createInstance.GetDataNameSpace() + ";" + Environment.NewLine);
            return import.ToString();
        }

        /// <summary>
        /// 得到业务层引用字符串
        /// </summary>
        /// <returns></returns>
        public string GetImport_Business()
        {
            StringBuilder import = new StringBuilder();
            import.Append("using System;\r\n");
            import.Append("using System.Text;\r\n\r\n");
            return import.ToString();
        }

        /// <summary>
        /// 数据层引用字符串
        /// </summary>
        /// <returns></returns>
        public string GetImport_Business_Inside()
        {
            StringBuilder import = new StringBuilder();
            return import.ToString();
        }

        /// <summary>
        /// 得到接口层引用字符串
        /// </summary>
        /// <returns></returns>
        public string GetImport_Interface()
        {
            StringBuilder import = new StringBuilder();
            import.Append("using System;\r\n");
            return import.ToString();
        }

        /// <summary>
        /// 数据层引用字符串
        /// </summary>
        /// <returns></returns>
        public string GetImport_Interfacl_Inside()
        {
            StringBuilder import = new StringBuilder();
            return import.ToString();
        }

        /// <summary>
        /// 得到工厂层引用字符串
        /// </summary>
        /// <returns></returns>
        public string GetImport_Factory()
        {
            StringBuilder import = new StringBuilder();
            import.Append("using System;\r\n");
            import.Append("using System.Reflection;\r\n\r\n");
            return import.ToString();
        }

        /// <summary>
        /// 数据层引用字符串
        /// </summary>
        /// <returns></returns>
        public string GetImport_Factory_Inside()
        {
            StringBuilder import = new StringBuilder();
            return import.ToString();
        }
    }
}
