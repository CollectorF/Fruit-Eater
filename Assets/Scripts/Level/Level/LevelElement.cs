using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

public class LevelElement
{
    [JsonProperty("type")]
    public ElementType Type { get; set; }

    [JsonProperty("rotation")]
    public int Rotation { get; set; }

    [JsonProperty("spawnPoint")]
    public int SpawnPoint { get; set; }
}
