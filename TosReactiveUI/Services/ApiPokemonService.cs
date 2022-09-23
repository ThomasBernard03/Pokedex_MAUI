using System;
using Simple.Http;
using TosReactiveUI.Models.DTOs;
using TosReactiveUI.Models.Interfaces;
using TosReactiveUI.Services.Interfaces;

namespace TosReactiveUI.Services;

public class ApiPokemonService : IPokemonService
{
    private readonly IHttpService _httpService;
    public ApiPokemonService(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task<IEnumerable<IPokemonEntity>> GetPokemonsAsync()
    {
        var pokemonsDto = new List<PokemonDtoDown>();


        for (int i = 1; i <= 898; i++)
        {
            var pokemonHttpResult = await _httpService.SendHttpRequest<PokemonDtoDown>($"https://pokeapi.co/api/v2/pokemon-form/{i}", HttpMethod.Get);
            pokemonsDto.Add(pokemonHttpResult?.Result);
        }

        return pokemonsDto;
    }
}

