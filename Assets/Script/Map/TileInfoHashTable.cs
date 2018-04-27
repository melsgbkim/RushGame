using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using System.Xml;

public class TileInfoHashTable
{
    static TileInfoHashTable _get = null;
    public static TileInfoHashTable Get
    {
        get
        {
            if (_get == null)
                _get = new TileInfoHashTable();
            return _get;
        }
    }
    XmlFile TileInfoFile = XMLFileLoader.Loader.File("TileInfo");
    Hashtable table = new Hashtable();


    public TileBase GetTile(string id)
    {
        if (table.ContainsKey(id))
            return table[id] as TileBase;
        XmlElement xmlNode = TileInfoFile.GetNodeByID(id, "Tile");
        XmlNodeList list = xmlNode.GetElementsByTagName("Path");
        if (list.Count != 1)
            Debug.Log("TileInfoXml , id="+id+" TileInfo.Path.count="+list.Count);
        TileBase result = Resources.Load(list[0].InnerText) as TileBase;
        table.Add(id, result);
        return result;
    }
}
