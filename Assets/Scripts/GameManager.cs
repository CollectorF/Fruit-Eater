using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    private LevelLoader levelLoader;
    [SerializeField]
    private GameObject prefab;

    private void Start()
    {
        Load(0);
    }

    private void Load(int levelNumber)
    {
        levelLoader.SetupLevel(levelNumber);
    }
}
