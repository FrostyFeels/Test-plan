using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEditor : MonoBehaviour
{
    public MapInfo info;
    public MapViewer viewer;
    

    [SerializeField] LayerMask tileMask;
    public Map curMap;

    public Vector3 startTile, endTile;
    public bool choseTile;


    public List<Tile> selectedTiles = new List<Tile>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0) && !info.select)
        {
            MapSelecter();
        }
        else
        {
            choseTile = false;
            startTile = Vector3.one * -1;
            endTile = Vector3.one * -1;
            selectedTiles.Clear();
        }
    }

    public void MapSelecter()
    {
        TileStats tile = BuildLogic.OnTileSelect(tileMask);

        if (tile == null || tile.ID == endTile)
            return;

        if (!choseTile)
        {
            startTile = tile.ID;
            choseTile = true;
        }
    
        endTile = tile.ID;
        

        RedoBuildTiles();
        Build();
    }
    public void Build()
    {
        
        Vector3[] startEnd = BuildLogic. GetStart(startTile, endTile);

        Vector3 start = startEnd[0];
        Vector3 end = startEnd[1];


        
        start = BuildLogic.GetID(start, info.map);
        end = BuildLogic.GetID(end, info.map);

        BuildTiles(BuildLogic.GetEdges(start, end, info.currentMap.mapData, viewer.currentLevel));
        BuildTiles(BuildLogic.GetMiddle(start, end, info.currentMap.mapData, viewer.currentLevel));


    }
    public void BuildTiles(List<Tile> dataList)
    {

        foreach (Tile _Tile in dataList)
        {
            selectedTiles.Add(_Tile);
            _Tile.selected = true;
            BuildLogic.ChangeColor("White", _Tile.renderer);
        }
    }
    public void RedoBuildTiles()
    {
        foreach (Tile _Tile in selectedTiles)
        {
            _Tile.selected = !_Tile.selected;
            BuildLogic.ChangeColor("UnSelected", _Tile.renderer);
        }
        selectedTiles.Clear();
    }
}
