using System;
using TosReactiveUI.Models.Interfaces;

namespace TosReactiveUI.Models.Entities;

public class PokemonEntity : IPokemonEntity
{
    public PokemonEntity()
    {
    }

    public PokemonEntity(IPokemonEntity pokemonEntity)
    {
        Id = pokemonEntity.Id;
        Name = pokemonEntity.Name;
        FrontSprite = pokemonEntity.FrontSprite;
        BackSprite = pokemonEntity.BackSprite;
        Types = pokemonEntity.Types;
    }

    public long Id { get; set; }
    public string Name { get; set; }
    public string FrontSprite { get; set; }
    public string BackSprite { get; set; }
    public IEnumerable<string> Types { get; set; }
}

