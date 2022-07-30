using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Tiles
{
    public char Code;
    public LevelTile MapTile;
    public GameObject TileAsset;
}


[RequireComponent(typeof(LevelFiller))]
public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private string levelDir = "Level\\Level";
    [SerializeField]
    private List<Tiles> tilesLibrary;

    private ILevelLoader levelLoader;
    private LevelFiller levelFiller;
    private List<TileController> tiles;
    Dictionary<char, GameObject> tileAssets;
    internal Level level;

    //internal event Action OnLevelLoad;

    private void Awake()
    {
        tileAssets = new Dictionary<char, GameObject>();
        List<TileController> tiles = new List<TileController>();

        foreach (var tile in tilesLibrary)
        {
            tileAssets.Add(tile.Code, tile.TileAsset);
        }
        levelLoader = new ResourcesLevelLoader(tileAssets);
        levelFiller = GetComponent<LevelFiller>();
    }

    internal void SetupLevel(int levelid)
    {
        string levelName = null;
        levelName = levelDir + (levelid + 1);
        level = levelLoader.ReadLevel(levelName);
        tiles = levelFiller.FillLevel(tileAssets, level);
        //OnLevelLoad?.Invoke();
    }
}

