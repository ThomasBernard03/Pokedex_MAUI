using System;
using DynamicData;
using System.Collections.ObjectModel;
using TosReactiveUI.Services.Interfaces;
using TosReactiveUI.ViewModels.Base;
using TosReactiveUI.Models.Interfaces;
using ReactiveUI;
using System.Reactive.Linq;
using Newtonsoft.Json;
using TosReactiveUI.Models.Entities;

namespace TosReactiveUI.ViewModels;

public class PokemonsViewModel : BaseViewModel
{
    private readonly IPokemonService _pokemonService;

    public PokemonsViewModel(INavigationService navigationService, IPokemonService pokemonService) : base(navigationService)
    {
        _pokemonService = pokemonService;

        _pokemonsCache
            .Connect()
            .Bind(out _pokemons)
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe();
    }

    #region Life cycle

    protected override async Task OnNavigatedToAsync(INavigationParameters parameters)
    {
        await base.OnNavigatedToAsync(parameters);

        var pokemons = await _pokemonService.GetPokemonsAsync();

        Types = pokemons.SelectMany(p => p.Types).Distinct();
        _pokemonsCache.AddOrUpdate(pokemons);

        //var x = JsonConvert.SerializeObject(pokemons);
    }

    #endregion


    #region Properties

    #region Dynamic list Pokemons
    private SourceCache<IPokemonEntity, long> _pokemonsCache = new SourceCache<IPokemonEntity, long>(r => r.Id);
    private readonly ReadOnlyObservableCollection<IPokemonEntity> _pokemons;
    public ReadOnlyObservableCollection<IPokemonEntity> Pokemons => _pokemons;
    #endregion

    #region Types

    private IEnumerable<string> _types;
    public IEnumerable<string> Types
    {
        get => _types;
        set => this.RaiseAndSetIfChanged(ref _types, value);
    }

    #endregion

    #endregion

    #region Methods & Commands

    #endregion
}

