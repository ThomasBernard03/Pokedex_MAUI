using System;
using System.Reflection;
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
        try
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("pokemons.json");
            using var reader = new StreamReader(stream);

            var pokemons = JsonConvert.DeserializeObject<List<PokemonEntity>>(reader.ReadToEnd());
            return pokemons;
        }
        catch(Exception ex)
        {
            return null;
        }
    }
}

