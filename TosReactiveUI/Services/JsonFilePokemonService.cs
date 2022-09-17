using System;
using Newtonsoft.Json;
using TosReactiveUI.Models.Entities;
using TosReactiveUI.Models.Interfaces;
using TosReactiveUI.Services.Interfaces;

namespace TosReactiveUI.Services;

public class JsonFilePokemonService : IPokemonService
{
    public JsonFilePokemonService()
    {
    }

    public async Task<IEnumerable<IPokemonEntity>> GetPokemonsAsync()
    {
        var path = @"/Users/thomasbernard/Desktop/TosReactiveUI/TosReactiveUI/Resources/pokemons.json";
        var pokemonsJson = System.IO.File.ReadAllText(path);

        if (!string.IsNullOrEmpty(pokemonsJson))
        {
            var pokemons = JsonConvert.DeserializeObject<List<PokemonEntity>>(pokemonsJson);
            return pokemons;
        }

        return null;
    }
}

