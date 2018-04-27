using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using System.Xml;

public class GridMap : MonoBehaviour
{
    Tilemap tilemap;

    // Use this for initialization
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        tilemap.SetTile(new Vector3Int(1, 0, 0), TileInfoHashTable.Get.GetTile("Tilemap_0_1"));
        tilemap.SetTile(new Vector3Int(2, 0, 0), TileInfoHashTable.Get.GetTile("Tilemap_0_2"));
        tilemap.SetTile(new Vector3Int(3, 0, 0), TileInfoHashTable.Get.GetTile("Tilemap_0_3"));

        tilemap.SetTile(new Vector3Int(0, 1, 0), TileInfoHashTable.Get.GetTile("Tilemap_1_0"));
        tilemap.SetTile(new Vector3Int(1, 1, 0), TileInfoHashTable.Get.GetTile("Tilemap_1_1"));
        tilemap.SetTile(new Vector3Int(2, 1, 0), TileInfoHashTable.Get.GetTile("Tilemap_1_2"));
        tilemap.SetTile(new Vector3Int(3, 1, 0), TileInfoHashTable.Get.GetTile("Tilemap_1_3"));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
