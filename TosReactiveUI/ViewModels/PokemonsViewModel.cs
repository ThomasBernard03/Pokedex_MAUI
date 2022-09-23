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
        Types = new ObservableCollection<string>(new List<string>() { "Any" });

        var searchFilter = this.WhenAnyValue(viewModel => viewModel.SearchBarText)
            .Select(searchBarText =>
            {
                if (!string.IsNullOrEmpty(searchBarText))
                    return new Func<IPokemonEntity, bool>(pokemon => pokemon.Name?.Contains(searchBarText) ?? false);
                else
                    return new Func<IPokemonEntity, bool>(pokemon => true);
            });

        var typeFilter = this.WhenAnyValue(viewModel => viewModel.SelectedType)
            .Select(selectedType =>
            {
                if (!string.IsNullOrEmpty(selectedType) && SelectedType != "Any")
                    return new Func<IPokemonEntity, bool>(pokemon => pokemon.Types?.Contains(selectedType) ?? false);
                else
                    return new Func<IPokemonEntity, bool>(pokemon => true);
            });


        _pokemonsCache
            .Connect()
            .Filter(searchFilter) // Apply the search bar filter
            .Filter(typeFilter) // Apply the type filter
            .SortBy(p => p.Id) // Sort all pokemons by Id
            .Bind(out _pokemons) // Bind on our private prop
            .ObserveOn(RxApp.MainThreadScheduler) // Force the execution on the main thread
            .Subscribe(x => NumberOfPokemons = _pokemons.Count); // Subscribe to updates
    }

    #region Life cycle

    protected override async Task OnNavigatedToAsync(INavigationParameters parameters)
    {
        await base.OnNavigatedToAsync(parameters);

        var pokemons = await _pokemonService.GetPokemonsAsync();

        Types.Add(pokemons.SelectMany(p => p.Types).Distinct());
        _pokemonsCache.AddOrUpdate(pokemons);
    }

    #endregion


    #region Properties

    #region Dynamic list Pokemons
    private SourceCache<IPokemonEntity, long> _pokemonsCache = new SourceCache<IPokemonEntity, long>(r => r.Id);
    private readonly ReadOnlyObservableCollection<IPokemonEntity> _pokemons;
    public ReadOnlyObservableCollection<IPokemonEntity> Pokemons => _pokemons;
    #endregion

    #region SearchBarText

    private string _searchBarText;
    public string SearchBarText
    {
        get => _searchBarText;
        set => this.RaiseAndSetIfChanged(ref _searchBarText, value);
    }

    #endregion

    #region Types

    private ObservableCollection<string> _types;
    public ObservableCollection<string> Types
    {
        get => _types;
        set => this.RaiseAndSetIfChanged(ref _types, value);
    }

    #endregion

    #region SelectedType

    private string _selectedType;
    public string SelectedType
    {
        get => _selectedType;
        set => this.RaiseAndSetIfChanged(ref _selectedType, value);
    }

    #endregion

    #region NumberOfPokemons

    private int _numberOfPokemons;
    public int NumberOfPokemons
    {
        get => _numberOfPokemons;
        private set => this.RaiseAndSetIfChanged(ref _numberOfPokemons, value);
    }

    #endregion

    #endregion

    #region Methods & Commands

    #endregion
}

