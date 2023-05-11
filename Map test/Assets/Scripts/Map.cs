using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//The MapHolder holds each map so we can seperately call then
//Holds the size of all maps and the tileSize //NOTE: might be changed to per map but probally not
[Serializable]
public class MapHolder
{
    public Vector2 holderSize;
    public Vector3 mapSize;
    public int tileSize;

    public List<Map> maps = new List<Map>();

    //Creates a map and adds layers depend on the y-size
    //Then calls create layers
    public void CreateMap()
    {
        int row = 0;
        int colum = 0;

        foreach (Map _Map in maps)
        {
            _Map.gridspot = new Vector2(row, colum);
            for (int x = 0; x < mapSize.y; x++)
            {
                _Map.layers.Add(new Layer());
            }
            _Map.height = _Map.layers.Count;
            _Map.CreateLayer(mapSize);

            row++;
            if (row >= holderSize.x)
            {
                row = 0;
                colum++;
            }              
        }
    }
}


//The map holds every Layer.
//A layer is a grid of x long and z wide
//Layers stack on eachother
//Holds height and the mapdata for easy calling
//Creates a tile for the mapsize
[Serializable]
public class Map
{
    public Vector2 gridspot;
    public Tile[,,] mapData;

    public int height;

    public List<Layer> layers = new List<Layer>();

    //This creates the Tile list for the layer
    public void CreateLayer(Vector3 mapsize)
    {
        int count = 0;
        foreach (Layer _layer in layers)
        {
            for (int z = 0; z < mapsize.z; z++)
            {
                for (int x = 0; x < mapsize.x; x++)
                {
                    _layer.layerIndex = count;
                    Tile tile = new Tile();
                    tile.pos = new Vector3(x, count, z);
                    _layer.tiles.Add(tile);       
                }
            }
            count++;
        }
    }
    public void HideMap(bool hide)
    {
        foreach (Layer _Layer in layers)
        {
            _Layer.ChangeVisibility(hide, hide);
        }
    }
}


//Layer holds every tile and a fucntion to easily turn them off or on
[Serializable]
public class Layer
{
    public int layerIndex;
    public List<Tile> tiles = new List<Tile>();

    //Changes visibility for a layer by either turning them on or off or by making them white or black
    //Black for when the tile is under the layer you are working on
    //White for the layer you are working on
    public void ChangeVisibility(bool setting, bool keepFilled)
    {
        foreach (Tile _tile in tiles)
        {
            if(setting)
            {
                _tile.obj.SetActive(setting);
                if(_tile.selected)
                    BuildLogic.ChangeColor("White", _tile.renderer);
                else
                    BuildLogic.ChangeColor("UnSelected", _tile.renderer);
            }

            if(!setting)
            {
                if(_tile.selected && keepFilled)
                {
                    BuildLogic.ChangeColor("Black", _tile.renderer);
                }
                else
                {
                    _tile.obj.SetActive(setting);
                    
                }
            }
        }
    }
}

//Tiles holds the position if theyre selected if theyre a edge tile and the obj and renderer for the tile
[Serializable]
public class Tile
{
    public Vector3 pos;
    public bool selected;
    public bool edgeTile;
    public bool stoodOn;

    public GameObject obj;
    public Renderer renderer;
    public BoxCollider collider;
}
