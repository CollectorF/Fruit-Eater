using System.Collections.Generic;
using UnityEngine;

public struct LevelTile
{
    public GameObject Tile;
    public char Code;
    public Vector2Int Position;

    public LevelTile(GameObject tile, char code, Vector2Int position)
    {
        Tile = tile;
        Code = code;
        Position = position;
    }
}

public class Level
{
    private LevelTile[][] level;
    public int TargetsQuantity { get; private set; }

    public Level(LevelTile[][] tiles, int targets)
    {
        level = tiles;
        TargetsQuantity = targets;
    }

    public Vector2Int GetLevelSize()
    {
        if (level.Length == 0)
        {
            return Vector2Int.zero;
        }
        return new Vector2Int(level.Length, level[0].Length);
    }

    public LevelTile GetTileAt(int x, int y)
    {
        return level[y][x];
    }

    //public void SetTileState(Vector2Int position, bool state)
    //{
    //    level[position.x][-position.y].isActive = state;
    //}


    //public Vector2 GetTileCenter(Tilemap tilemap, LevelTile tile)
    //{
    //    Vector2 position = tilemap.GetCellCenterWorld(new Vector3Int(tile.Position.y, -tile.Position.x, 0));
    //    return position;
    //}
}
