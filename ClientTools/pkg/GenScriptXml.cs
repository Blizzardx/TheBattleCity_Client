using System.Collections.Generic;
using System.Xml.Serialization;
using Common.Config;

[XmlRoot("root")]
public class GenScriptXmlConfig : XmlConfigBase
{
    [XmlArray("classConfigList")]
    public List<GenScriptClassXmlConfig> classConfigList { get; set; }
}
public class GenScriptClassXmlConfig : XmlConfigBase
{
    [XmlElement("className")]
    public string className { get; set; }

    [XmlArray("lineConfigList")]
    public List<GenScriptLineXmlConfig> lineConfigList { get; set; }
}

public class GenScriptLineXmlConfig : XmlConfigBase
{
    [XmlElement("index")]
    public int index { get; set; }

    [XmlElement("classTypeName")]
    public string classTypeName { get; set; }

    [XmlElement("memberName")]
    public string memberName { get; set; }

    [XmlElement("desc")]
    public string desc { get; set; }

    [XmlElement("rangeMin")]
    public string rangeMin { get; set; }

    [XmlElement("rangeMax")]
    public string rangeMax { get; set; }

    [XmlElement("isList")]
    public bool isList { get; set; }
}
