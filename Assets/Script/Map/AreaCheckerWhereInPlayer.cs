using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;
using System.Xml;


public class AreaCheckerWhereInPlayer : MonoBehaviour
{
    public static AreaCheckerWhereInPlayer Get = null;
    public AreaCheckerWhereInPlayer()
    {
        if (Get == null)
            Get = this;
    }
    public List<GridMap> GridAreaList;

    List<GridMap> GridMapList = new List<GridMap>();
    Grid myGrid;
    Vector3Int? PlayerPosIndex;
    string nowTileArea = "None";
    void Start()
    {
        myGrid = GetComponent<Grid>();
        for (int i=0; i< transform.childCount;i++)
        {
            Transform child = transform.GetChild(i);
            GridMap tmp = child.GetComponent<GridMap>();
            if (tmp != null)
                GridMapList.Add(tmp);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }


    public Vector3Int GetTileIndexFromPos(Vector3 pos)
    {
        return new Vector3Int(Mathf.FloorToInt(pos.x / myGrid.cellSize.x), Mathf.FloorToInt(pos.y / myGrid.cellSize.y), 0);
    }

    public string GetAreaNameInPlayer(Vector3 pos)
    {
        TileBase tile = null;
        Vector3Int tmp = GetTileIndexFromPos(pos);
        if (tmp != PlayerPosIndex)
        {
            PlayerPosIndex = tmp;
            foreach (GridMap map in GridAreaList)
            {
                tile = map.GetTileFromPos(PlayerPosIndex.Value);
                if (tile != null)   break;
            }
            if (tile == null)   nowTileArea = "None";
            else                nowTileArea = tile.name;
        }
        return nowTileArea;
    }

    public string GetAreaDataTagByPos(string TagName,Vector3 pos)
    {
        string name = GetAreaNameInPlayer(pos);
        XmlFile Info = XmlFile.Load("AreaInfo");
        XmlElement Area = Info.GetNodeByID(name, "Area");
        XmlElement node = XMLUtil.FindOneByTag(Area, TagName);
        return (node == null ? "" : node.InnerText);
    }

    public string GetAreaMoveTypePos(Vector3 pos)
    {
        return GetAreaDataTagByPos("AreaMoveType", pos);
    }

    public string GetAreaUITypePos(Vector3 pos)
    {
        return GetAreaDataTagByPos("AreaUIType", pos);
    }
}
