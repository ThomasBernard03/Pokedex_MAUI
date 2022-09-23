using System;
using TosReactiveUI.Models.Interfaces;
using Newtonsoft.Json;

namespace TosReactiveUI.Models.DTOs;

public class PokemonDtoDown : IPokemonEntity
{
    public PokemonDtoDown()
    {
    }


    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("order")]
    public int Order { get; set; }

    [JsonProperty("sprites")]
    public Sprites Sprites { get; set; }

    [JsonProperty("types")]
    public IEnumerable<TypeDtoDown> TypesDto { get; set; }



    public string FrontSprite
    {
        get => Sprites?.FrontDefault;
        set => Sprites.FrontDefault = value;
    }

    public string BackSprite
    {
        get => Sprites?.BackDefault;
        set => Sprites.BackDefault = value;
    }

    public IEnumerable<string> Types
    {
        get => TypesDto.Select(t => t?.Type?.Name);
        set => throw new NotImplementedException();
    }
}

public class Sprites
{
    [JsonProperty("back_default")]
    public string BackDefault { get; set; }

    [JsonProperty("front_default")]
    public string FrontDefault { get; set; }
}

