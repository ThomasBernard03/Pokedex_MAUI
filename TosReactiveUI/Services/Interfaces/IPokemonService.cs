using System;
using TosReactiveUI.Models.Interfaces;

namespace TosReactiveUI.Services.Interfaces;

public interface IPokemonService
{
    Task<IEnumerable<IPokemonEntity>> GetPokemonsAsync();
}

