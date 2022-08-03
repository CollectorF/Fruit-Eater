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
    private GameObject spawnPoint;
    [SerializeField]
    private List<Tiles> tilesLibrary;

    private ILevelLoader levelLoader;
    private LevelFiller levelFiller;
    private ElementLoader elementLoader;
    private Dictionary<char, GameObject> tileAssets;
    private List<TileController> tiles;

    internal Level level;
    internal List<LevelElement> levelElements;

    public delegate void LevelLoadEvent();

    public event LevelLoadEvent OnLevelLoad;

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
        elementLoader = GetComponent<ElementLoader>();
    }

    internal void SetupLevel(int levelid)
    {
        string levelName;
        //if (tiles != null)
        //{
        //    DestroyLevel();
        //}
        levelName = levelDir + (levelid + 1);
        level = levelLoader.ReadLevel(levelName);
        levelElements = levelLoader.ReadLevelInfo(levelName);
        tiles = levelFiller.FillLevel(tileAssets, level, spawnPoint);
        SetInitialPointPosition();
        elementLoader.SetupActiveElements(levelElements);
        OnLevelLoad?.Invoke();
    }

    private void SetInitialPointPosition()
    {
        Transform basePrefabTransform = null;
        Transform[] prefabElements = tilesLibrary[0].TileAsset.GetComponentsInChildren<Transform>();
        foreach (var item in prefabElements)
        {
            if (item.CompareTag("Base"))
            {
                basePrefabTransform = item;
                break;
            };
        }
        Vector2 levelSize = level.GetLevelSize();
        spawnPoint.transform.position = new Vector3
            (
                (-levelSize.x * basePrefabTransform.localScale.x / 2) + basePrefabTransform.localScale.x / 2,
                0,
                (levelSize.y * basePrefabTransform.localScale.z / 2) - basePrefabTransform.localScale.z / 2
            );
    }

    private void DestroyLevel()
    {
        if (tiles.Count != 0)
        {
            foreach (var item in tiles)
            {
                Destroy(item.gameObject);
            }
            tiles.Clear();
        }
    }
}

