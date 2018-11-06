using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

using UnityEngine;

[XmlRoot("Map")]
public class MapData
{
    //public int map;
    //public int stage;

    [XmlArray("Monsters"), XmlArrayItem("Monster")]
    public MonsterData[] monsters;

    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(MapData));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
    }

    public static MapData Load(string path)
    {
        var serializer = new XmlSerializer(typeof(MapData));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as MapData;
        }
    }

    //Loads the xml directly from the given string. Useful in combination with www.text.
    public static MapData LoadFromText(string text)
    {
        var serializer = new XmlSerializer(typeof(MapData));
        return serializer.Deserialize(new StringReader(text)) as MapData;
    }
}
