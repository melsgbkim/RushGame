using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using System.Xml;

public class GridMap : MonoBehaviour
{
    Tilemap tilemap;
    public Grid grid;

    public enum GridType
    {
        Nomal,
        HasCollision,
        AreaData
    }
    public GridType type = GridType.Nomal;

    // Use this for initialization
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        switch(type)
        {
            case GridType.Nomal:
                tilemap.SetTile(new Vector3Int(0, 0, 0), TileInfoHashTable.Get.GetTile("Tilemap_0_0"));
                tilemap.SetTile(new Vector3Int(1, 0, 0), TileInfoHashTable.Get.GetTile("Tilemap_0_1"));
                tilemap.SetTile(new Vector3Int(2, 0, 0), TileInfoHashTable.Get.GetTile("Tilemap_0_2"));
                tilemap.SetTile(new Vector3Int(3, 0, 0), TileInfoHashTable.Get.GetTile("Tilemap_0_3"));
                tilemap.SetTile(new Vector3Int(0, 1, 0), TileInfoHashTable.Get.GetTile("Tilemap_0_0"));
                tilemap.SetTile(new Vector3Int(1, 1, 0), TileInfoHashTable.Get.GetTile("Tilemap_0_1"));
                tilemap.SetTile(new Vector3Int(2, 1, 0), TileInfoHashTable.Get.GetTile("Tilemap_0_2"));
                tilemap.SetTile(new Vector3Int(3, 1, 0), TileInfoHashTable.Get.GetTile("Tilemap_0_3"));
                tilemap.SetTile(new Vector3Int(0, 2, 0), TileInfoHashTable.Get.GetTile("Tilemap_0_0"));
                tilemap.SetTile(new Vector3Int(1, 2, 0), TileInfoHashTable.Get.GetTile("Tilemap_0_1"));
                tilemap.SetTile(new Vector3Int(2, 2, 0), TileInfoHashTable.Get.GetTile("Tilemap_0_2"));
                tilemap.SetTile(new Vector3Int(3, 2, 0), TileInfoHashTable.Get.GetTile("Tilemap_0_3"));
                break;
            case GridType.HasCollision:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public TileBase GetTileFromPos(Vector3Int posInt)
    {
        return tilemap.GetTile(posInt);
    }
}
