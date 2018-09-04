using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

public class dataRetrieval : MonoBehaviour {
/*
    [XmlArray("bodies")]
    [XmlArrayItem("body")]
    public List<Items> Bodies = new List<Items>();

    public static dataRetrieval Load(string path)
    {
        TextAsset _xml = Resources.Load<TextAsset>(path);

        XmlSerializer serializer = new XmlSerializer(typeof(dataRetrieval));

        StringReader reader = new StringReader(_xml.text);

        dataRetrieval Bodies = serializer.Deserialize(reader) as dataRetrieval;

        reader.Close();

        return Bodies;
    }
    */
}
