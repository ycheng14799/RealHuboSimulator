using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("BodyCollection")]
public class BodyContainer{

    [XmlArray("Bodies")]
    [XmlArrayItem("Body")]
    public List<Body> bodies = new List<Body>();

    public static BodyContainer Load(string path)
    {
        TextAsset _xml = Resources.Load<TextAsset>(path);

        XmlSerializer serializer = new XmlSerializer(typeof(BodyContainer));

        StringReader reader = new StringReader(_xml.text);

        BodyContainer bodies = serializer.Deserialize(reader) as BodyContainer;

        reader.Close();

        return bodies;
    }

}
