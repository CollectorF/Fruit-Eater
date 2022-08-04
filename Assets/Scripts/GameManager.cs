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
    private float timerOnLevelEnd;

    private int currentLevelNumber = 0; // Level numbers start from zero: 0 = "level1"
    private Coroutine timerCoroutine;
    private bool levelEnded;

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
        levelEnded = false;
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
        if (!levelEnded)
        {
            Debug.Log("You Win!");
            currentLevelNumber++;
            if (timerCoroutine == null)
            {
                timerCoroutine = StartCoroutine(LoadTimerCoroutine(timerOnLevelEnd));
            }
            levelEnded = true;
        }
    }

    private void LoseLevel()
    {
        if (!levelEnded)
        {
            Debug.Log("You Lose!");
            if (timerCoroutine == null)
            {
                timerCoroutine = StartCoroutine(LoadTimerCoroutine(timerOnLevelEnd));
            }
            levelEnded = true;
        }
    }


    private IEnumerator LoadTimerCoroutine(float timer)
    {
        yield return new WaitForSeconds(timer);
        try
        {
            LoadLevel(currentLevelNumber);
        }
        catch
        {
            currentLevelNumber = 0;
            LoadLevel(currentLevelNumber);
        }
        timerCoroutine = null;
    }
}
