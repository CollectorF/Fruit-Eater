using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    private LevelLoader levelLoader;
    [SerializeField]
    private LevelHandler levelHandler;
    [SerializeField]
    private GameObject prefab;

    private void Awake()
    {
        levelHandler.OnAllCollected += WinLevel;
    }

    private void Start()
    {
        Load(0);
    }

    private void Load(int levelNumber)
    {
        levelLoader.SetupLevel(levelNumber);
    }

    private void WinLevel()
    {
        Debug.Log("You Win!");
    }
}
