using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerPrefsManager))]
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private LevelLoader levelLoader;
    [SerializeField]
    private LevelHandler levelHandler;
    [SerializeField]
    private UIManager uiManager;
    [SerializeField]
    private float timerOnLevelEnd = 2;
    [SerializeField]
    private string winText = "You \n win!";
    [SerializeField]
    private string loseText = "You \n lose!";

    private PlayerPrefsManager prefsManager;

    private int currentLevelNumber; // Level numbers start from zero: 0 = "level1"
    private Coroutine timerCoroutine;
    private bool levelEnded;

    private void Awake()
    {
        prefsManager = GetComponent<PlayerPrefsManager>();
        uiManager.OnRestartClick += RestartLevel;
        levelHandler.OnQuantityChanged += CheckWinLoseConditions;
    }

    private void Start()
    {
        currentLevelNumber = prefsManager.LoadPlayerPrefs();
        LoadLevel(currentLevelNumber);
    }

    private void LoadLevel(int levelNumber)
    {
        uiManager.SetPopupState(false);
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
            uiManager.SetPopupText(winText);
            uiManager.SetPopupState(true);
            currentLevelNumber++;
            prefsManager.SavePlayerPrefs(currentLevelNumber);
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
            uiManager.SetPopupText(loseText);
            uiManager.SetPopupState(true);
            if (timerCoroutine == null)
            {
                timerCoroutine = StartCoroutine(LoadTimerCoroutine(timerOnLevelEnd));
            }
            levelEnded = true;
        }
    }

    private void RestartLevel()
    {
        LoadLevel(currentLevelNumber);
    }


    private IEnumerator LoadTimerCoroutine(float timer)
    {
        yield return new WaitForSeconds(timer);
        uiManager.SetPopupState(false);
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
