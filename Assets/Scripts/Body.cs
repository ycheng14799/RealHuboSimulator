using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class Body {

    [XmlAttribute("name")]
    public string kinbody;
    [XmlElement("modelsdir")]
    public string modelDir;
    [XmlAttribute("body")]
    public string name;
    public string type;
}