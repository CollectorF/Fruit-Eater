using System.Collections.Generic;

public interface ILevelReader
{
    Level ReadLevel(string levelId);

    List<LevelElement> ReadLevelInfo(string levelId);
}
