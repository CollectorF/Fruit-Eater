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

    private void Awake()
    {
        levelHandler.OnQuantityChanged += CheckWinLoseConditions;
    }

    private void Start()
    {
        Load(0);
    }

    private void Load(int levelNumber)
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
        }
    }

    private void LoseLevel()
    {
        if (!levelFinished)
        {
            Debug.Log("You Lose!");
        }
    }
}
