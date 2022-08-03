using System.Collections.Generic;

public interface ILevelLoader
{
    Level ReadLevel(string levelId);

    List<LevelElement> ReadLevelInfo(string levelId);
}
