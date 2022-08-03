using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class ResourcesLevelLoader : ILevelLoader
{
    private Dictionary<char, GameObject> tileLibrary;
    private string levelInfoFilePostfix = "_Parameters";

    public ResourcesLevelLoader(Dictionary<char, GameObject> tileLibrary)
    {
        this.tileLibrary = tileLibrary;
    }

    public Level ReadLevel(string levelId)
    {
        int targets = 0;
        string text = Resources.Load(levelId).ToString();
        string[] lines = Regex.Split(text, "\r\n");
        LevelTile[][] levelBase = new LevelTile[lines.Length][];
        for (int i = 0; i <= lines.Length - 1; i++)
        {
            string[] castedCode = lines[i].Split(' ');
            levelBase[i] = new LevelTile[castedCode.Length];
            for (int j = 0; j <= levelBase[i].Length - 1; j++)
            {
                char code = castedCode[j][0];
                Vector2Int position = new Vector2Int(i, j);
                tileLibrary.TryGetValue(castedCode[j][0], out GameObject tile);
                levelBase[i][j] = new LevelTile(tile, code, position);
                if (levelBase[i][j].Code == 'X')
                {
                    targets++;
                }
            }
        }
        return new Level(levelBase, targets);
    }

    public List<LevelElement> ReadLevelInfo(string levelId)
    {
        var fullFileName = string.Concat(levelId, levelInfoFilePostfix);
        string json = Resources.Load(fullFileName).ToString();
        List<LevelElement> levelParameters = JsonConvert.DeserializeObject<List<LevelElement>>(json);
        return levelParameters;
    }
}