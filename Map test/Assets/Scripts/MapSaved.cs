using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;


public class MapSaved : MonoBehaviour
{
    public MapHolder map;
    public Tile[,,] currentMap;
    public CameraMove move;

    public void Start()
    {
        if (map.maps.Count != 0)
            Make3DArray();
    }

    public void Make3DArray()
    {
       //set camera position
        move.target.transform.position = new Vector3((map.mapSize.x / 2), 0, -map.mapSize.z);
        

        foreach (Map _Map in map.maps) 
        {
            _Map.mapData = new Tile[(int)map.mapSize.x, (int)map.mapSize.y, (int)map.mapSize.z];
            foreach (Layer _Layer in _Map.layers)
            {
                foreach (Tile _Tile in _Layer.tiles)
                {
                    //add into a 3D array so its easier to access without having to do weird caluclations to figure out which tile we need it a list
                    _Map.mapData[(int)_Tile.pos.x, (int)_Tile.pos.y, (int)_Tile.pos.z] = _Tile;
                }
            }
        }

        //sets current map
        currentMap = map.maps[0].mapData;
    }
    public void CreatePrefab(string mapName)
    {
        foreach (Map _Map in map.maps)
        {
            foreach (Layer _Layer in _Map.layers)
            {
                foreach (Tile _Tile in _Layer.tiles)
                {
                    if (_Tile.selected)
                    {
                        _Tile.obj.SetActive(true);
                        BuildLogic.ChangeColor("White", _Tile.renderer);
                    }                      
                    else
                        _Tile.obj.SetActive(false);
                }
            }
        }

        string localPath = "Assets/Prefab/Maps/1/" + gameObject.name + ".prefab";
        print(localPath);


        PrefabUtility.SaveAsPrefabAsset(this.gameObject, localPath);

    }
}
