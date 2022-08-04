using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    private string CURRENT_LEVEL = "Current_level";

    public int LoadPlayerPrefs()
    {
        return PlayerPrefs.GetInt(CURRENT_LEVEL, 0);
    }

    public void SavePlayerPrefs(int level)
    {
        PlayerPrefs.SetInt(CURRENT_LEVEL, level);
        PlayerPrefs.Save();
    }

    [ContextMenu("Clear Player Prefs")]
    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
