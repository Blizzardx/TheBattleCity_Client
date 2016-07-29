using System.Xml.Serialization;

namespace Common.Config
{
    [XmlRoot("root")]
    public class SysConfig : XmlConfigBase
    {
        [XmlElement("serverConfigPath")]
        public string ServerConfigPath { get; set; }
        [XmlElement("clientConfigPath")]
        public string ClientConfigPath { get; set; }

        [XmlElement("excelPath")]
        public string ExcelPath { get; set; }

        [XmlElement("configCenterUrl")]
        public string ConfigCenterUrl { get; set; }

        [XmlElement("uploadUrl")]
        public string UploadUrl { get; set; }


    }
}
