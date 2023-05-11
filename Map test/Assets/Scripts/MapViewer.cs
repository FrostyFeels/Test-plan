using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapViewer : MonoBehaviour
{
    public MapInfo info;
    public int currentLevel;


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChangeLevel(-1, PrepareMaps(info.currentMap));

        if (Input.GetKeyDown(KeyCode.Alpha2))
            ChangeLevel(1, PrepareMaps(info.currentMap));
    }

    public List<Map> PrepareMaps(Map map)
    {
        List<Map> _Map = new List<Map>();
        _Map.Add(map);
        return _Map;
    }
    public List<Map> PrepareMaps(List<Map> maps)
    {
        return maps;
    }

    public void ChangeLevel(int increase, List<Map> maps)
    {
        int highestHight = 0;

        foreach (Map _Map in maps)
        {
            if (_Map.height > currentLevel)
                if(increase > 0)
                {
                    _Map.layers[currentLevel].ChangeVisibility(false, true);
                }
                else
                {
                    _Map.layers[currentLevel].ChangeVisibility(false, false);
                }
                

            if (_Map.height > highestHight)
                highestHight = _Map.height;
        }

        if(currentLevel + increase >= 0 && (highestHight > currentLevel + increase))
            currentLevel += increase;

        foreach (Map _Map in maps)
        {
            if (_Map.height > currentLevel)
                _Map.layers[currentLevel].ChangeVisibility(true, true);
        }

        ChangeVisibility(maps);    
    }    
    public void ChangeVisibility(List<Map> maps)
    {
        foreach (Map _Map in maps)
        {
            foreach (Layer _Layer in _Map.layers)
            {
                foreach (Tile _Tile in _Layer.tiles)
                {
                    if (!_Tile.selected)
                        BuildLogic.ChangeColor("UnSelected", _Tile.renderer);
                }
            }
        }
    }
}
