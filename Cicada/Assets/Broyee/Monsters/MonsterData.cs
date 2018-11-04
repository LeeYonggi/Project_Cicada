using System.Xml;
using System.Xml.Serialization;

using UnityEngine;

public class MonsterData {

    [XmlAttribute("Monster")]
    public string name;
    public Vector3 pos;
}
