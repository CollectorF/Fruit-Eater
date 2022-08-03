using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

public class LevelElement
{
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("rotation")]
    public int Rotation { get; set; }
}

//public class Element
//{
//}
//}
