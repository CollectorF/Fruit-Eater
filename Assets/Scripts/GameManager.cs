using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private LevelLoader levelLoader;

    [ContextMenu("Load")]
    private void Load()
    {
        levelLoader.SetupLevel(0);
    }
}
