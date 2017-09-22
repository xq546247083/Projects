using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace HaoCodeBuilder.Common
{
    public class Config_CodeBuilder
    {
        public Config_CodeBuilder()
        {
            XmlFileExists();
        }
        /// <summary>
        /// XML文件路径
        /// </summary>
        private string XmlFile = string.Format("{0}Config\\CodeBuilder.xml", Func.GetAppPath());
        /// <summary>
        /// 检查配置文件是否存在，没有则创建
        /// </summary>
        private void XmlFileExists()
        {
            FileInfo fiXML = new FileInfo(XmlFile);
            if (!(fiXML.Exists))
            {
                XDocument xelLog = new XDocument(
                    new XDeclaration("1.0", "utf-8", string.Empty),
                    new XElement("root")
                 );
                xelLog.Save(XmlFile);
            }
        }
        /// <summary>
        /// 添加一个类代码生成配置
        /// </summary>
        /// <param name="ccb"></param>
        public bool Add(Model.ConfigCodeBuilder ccb)
        {
            try
            {
                //先删除
                Delete(ccb.Modle);
                XElement xelem = XElement.Load(XmlFile);
                XElement newLog =
                    new XElement("CodeBuilder",
                    new XElement("Modle", ccb.Modle),
                    new XElement("Add", ccb.Add),
                    new XElement("Delete", ccb.Delete),
                    new XElement("Update", ccb.Update),
                    new XElement("Exist", ccb.Exist),
                    new XElement("GetAll", ccb.GetAll),
                    new XElement("Get", ccb.Get),
                    new XElement("GetCount", ccb.GetCount)
                                  );
                xelem.Add(newLog);
                xelem.Save(XmlFile);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 保存类代码生成配置
        /// </summary>
        /// <param name="cns"></param>
        /// <returns></returns>
        public bool Save(Model.ConfigCodeBuilder cns, string oldmodel = "")
        {
            if (!oldmodel.IsNullOrEmpty())
                Delete(oldmodel);
            return Add(cns);
        }
        /// <summary>
        /// 删除一个代码生成配置
        /// </summary>
        /// <param name="modle"></param>
        /// <returns></returns>
        public bool Delete(string modle)
        {
            try
            {
                XElement xelem = XElement.Load(XmlFile);
                var queryXML = from xele in xelem.Elements("CodeBuilder")
                               where xele.Element("Modle").Value == modle
                               select xele;

                queryXML.Remove();
                xelem.Save(XmlFile);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 查询一个代码生成配置
        /// </summary>
        /// <param name="namespace1"></param>
        /// <returns></returns>
        public Model.ConfigCodeBuilder Get(string model)
        {
            try
            {
                XElement xelem = XElement.Load(XmlFile);
                var queryXML = from xele in xelem.Elements("CodeBuilder")
                               where xele.Element("Modle").Value == model
                               select new
                               {
                                   Modle = xele.Element("Modle").Value,
                                   Add = xele.Element("Add").Value,
                                   Delete = xele.Element("Delete").Value,
                                   Update = xele.Element("Update").Value,
                                   Exist = xele.Element("Exist").Value,
                                   GetAll = xele.Element("GetAll").Value,
                                   Get = xele.Element("Get").Value,
                                   GetCount = xele.Element("GetCount").Value
                               };
                Model.ConfigCodeBuilder ccb = GetDefault(model);
                if (queryXML.Count() > 0)
                {
                    ccb.Modle = queryXML.First().Modle;
                    ccb.Add = Boolean.Parse(queryXML.First().Add);
                    ccb.Delete = Boolean.Parse(queryXML.First().Delete);
                    ccb.Update = Boolean.Parse(queryXML.First().Update);
                    ccb.Exist = Boolean.Parse(queryXML.First().Exist);
                    ccb.GetAll = Boolean.Parse(queryXML.First().GetAll);
                    ccb.Get = Boolean.Parse(queryXML.First().Get);
                    ccb.GetCount = Boolean.Parse(queryXML.First().GetCount);
                }
                return ccb;
            }
            catch
            {
                return GetDefault(model);
            }
        }
        /// <summary>
        /// 查询默认代码生成配置
        /// </summary>
        /// <param name="namespace1"></param>
        /// <returns></returns>
        public Model.ConfigCodeBuilder GetDefault(string model)
        {
            return new Model.ConfigCodeBuilder()
            {
                Modle = model,
                Add = true,
                Delete = true,
                Update = true,
                Exist = true,
                GetAll = true,
                Get = true,
                GetCount = true
            };
        }

        /// <summary>
        /// 查询一个代码生成配置默认类型
        /// </summary>
        /// <param name="namespace1"></param>
        /// <returns></returns>
        public String GetDefalutBuildType()
        {
            try
            {
                XElement xelem = XElement.Load(XmlFile);
                var queryXML = from xele in xelem.Elements("CodeBuilderDefault")
                               select new
                               {
                                   Name = xele.Element("Name").Value
                               };
                if (queryXML.Count() < 1)
                {
                    XElement newLog =
                        new XElement("CodeBuilderDefault",
                            new XElement("Name", "Custom")
                        );
                    xelem.Add(newLog);
                    xelem.Save(XmlFile);
                    return "Custom";
                }

                return queryXML.First().Name;
            }
            catch (Exception ex)
            {
                return "Custom";
            }
        }

        /// <summary>
        /// 更新一个代码生成配置默认类型
        /// </summary>
        /// <param name="namespace1"></param>
        /// <returns></returns>
        public void UpdateDefalutBuildType(string modelName)
        {
            try
            {
                XElement xelem = XElement.Load(XmlFile);
                var queryXML = from xele in xelem.Elements("CodeBuilderDefault")
                    select xele;

                queryXML.Remove();
                xelem.Save(XmlFile);

                XElement newLog =
                    new XElement("CodeBuilderDefault",
                        new XElement("Name", modelName)
                    );
                xelem.Add(newLog);
                xelem.Save(XmlFile);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
