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

    private bool levelFinished;
    private int currentLevelNumber = 0; // Level numbers start from zero: 0 = "level1"

    private void Awake()
    {
        levelHandler.OnQuantityChanged += CheckWinLoseConditions;
    }

    private void Start()
    {
        LoadLevel(currentLevelNumber);
    }

    private void LoadLevel(int levelNumber)
    {
        levelLoader.SetupLevel(levelNumber);
    }

    private void CheckWinLoseConditions(int targetQuantity, int elementsQuantity)
    {
        if (targetQuantity == 0)
        {
            WinLevel();
        }
        if(targetQuantity > 0 && elementsQuantity == 0)
        {
            LoseLevel();
        }
    }

    private void WinLevel()
    {
        if (!levelFinished)
        {
            Debug.Log("You Win!");
            currentLevelNumber++;
            levelFinished = true;
        }
    }

    private void LoseLevel()
    {
        if (!levelFinished)
        {
            Debug.Log("You Lose!");
            levelFinished = true;
        }
    }
}
