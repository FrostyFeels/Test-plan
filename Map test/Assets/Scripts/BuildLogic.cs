using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BuildLogic
{
    //This is 2 functions which will get the material from resources and give a renderer change the color of a object
    public static void ChangeColor(string matName, Renderer render)
    {
        Material material = GetMaterial(matName);
        render.material = material;
    }
    public static void ChangeColor(string matName, List<Tile> tiles)
    {
        Material material = GetMaterial(matName);
        foreach (Tile _Tile in tiles)
        {
            _Tile.renderer.material = material;
        }
    }

    //Gets the Material from the resources list based on name
    public static Material GetMaterial(string matName)
    {
        Material material = Resources.Load<Material>("Materials/" + matName);
        return material;
    }

    //Selects a tile based on the layer used
    public static TileStats OnTileSelect(int layers)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000, layers))
        {
            return hit.collider.GetComponent<TileStats>();
        }
        else
            return null;
    }

    //This functions is used to get the id between a value of mapsize.x and mapsize.y
    public static Vector3 GetID(Vector3 id, MapHolder map)
    {
        Vector3 newID = new Vector3(id.x % map.mapSize.x, id.y, id.z % map.mapSize.z);
        return newID;
    }

    //Get the map that is currently being walked on
    public static Map GetMap(Vector3 id, MapHolder map) 
    {
        Vector2 mapIndex = new Vector2((int)(id.x / map.mapSize.x), (int)(id.z / map.mapSize.z));
        Map _Map = map.maps[(int)(mapIndex.x + (map.holderSize.x * mapIndex.y))];
        return _Map;
    }

    //Get the start and end of the tiles being selected since it was a long calculation
    public static Vector3[] GetStart(Vector3 start, Vector3 end)
    {
        Vector3 realStart;
        Vector3 realEnd;

        if (start.x > end.x)
        {
            realStart.x = end.x;
            realEnd.x = start.x;
        }
        else
        {
            realStart.x = start.x;
            realEnd.x = end.x;
        }

        if (start.z > end.z)
        {
            realStart.z = end.z;
            realEnd.z = start.z;
        }
        else
        {
            realStart.z = start.z;
            realEnd.z = end.z;
        }

        if (start.y > end.y)
        {
            realStart.y = end.y;
            realEnd.y = start.y;
        }
        else
        {
            realStart.y = start.y;
            realEnd.y = end.y;
        }


        Vector3[] pos = new Vector3[2];
        pos[0] = realStart;
        pos[1] = realEnd;

        return pos;
    }

    //Get the edges of the tiles being selected
    public static List<Tile> GetEdges(Vector3 start, Vector3 end, Tile[,,] list, int height)
    {
        List<Tile> dataList = new List<Tile>();
        Tile data;
        for (int z = (int)start.z; z <= end.z; z++)
        {
            for (int x = (int)start.x; x <= end.x; x++)
            {
                data = list[x, height, z];
                if (((x == start.x || x == end.x) || (z == start.z || z == end.z)))
                {
                    dataList.Add(data);
                }
            }
        }

        return dataList;
    }

    //Get the middle of the tiles being selected
    public static List<Tile> GetMiddle(Vector3 start, Vector3 end, Tile[,,] list, int height)
    {
        List<Tile> dataList = new List<Tile>();
        Tile data;
        for (int y = (int)start.z; y <= end.z; y++)
        {
            for (int x = (int)start.x; x <= end.x; x++)
            {
                data = list[x, height, y];
                if (((x != start.x && x != end.x) && (y != start.z && y != end.z)))
                {
                    dataList.Add(data);
                }

            }
        }

        return dataList;
    }
}
