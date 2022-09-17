using System;
using Newtonsoft.Json;

namespace TosReactiveUI.Models.DTOs;

public class TypeDtoDown
{
    public TypeDtoDown()
    {
    }

    [JsonProperty("slot")]
    public int Slot { get; set; }

    [JsonProperty("type")]
    public TypeDescriptionDtoDown Type { get; set; }
}

public class TypeDescriptionDtoDown
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("url")]
    public string Url { get; set; }
}