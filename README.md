# Pokedex


<img width="460" alt="image" src="https://user-images.githubusercontent.com/67638928/192004611-cedfdbf0-4ad8-47ee-83dd-31dafb9c3c16.png">


## Introduction 

Dans ce Tos nous allons voir comment débuter avec ReactiveUI et en particulier les DynamicList. Je vous conseille de cloner l'application et de tester la recherche et le filtre de type afin de savoir ce que nous allons essayer de réaliser. Vous pouvez aussi vous servir de ce repository pour voir quelles sont les bonnes pratiques de développement en MAUI (MVVM, IOC...).

Les dynamic lists permettent de gérer des gros volumes de données très facilement, on peut ajouter des filtres, du tri et cela en étant optimisé.

## Pour commencer

Nous allons utiliser ces différents Packages Nugets : 
  - Simple.Http => Pour réaliser des call Http et récupérer la liste de tous les Pokémons
  - Newtonsoft.json => Pour pouvoir déserialiser notre JSON
  - Prism (Maui & DryIoc) => Pour notre DI et IOC
  - ReactiveUI.Maui => Pour notre dynamic list
  
  ## Récupération des pokemons
  
  Pour pouvoir utiliser des dynamic lists ils nous faut un très grand volume de données, pour ce TOS j'ai choisi les pokémons. Il nous faut alors récupérer tous les pokémons, pour cela j'utilise la PokéApi (https://pokeapi.co/). Mais cela est très long à utiliser j'ai donc enregistré toutes ces informations dans un fichier JSON plus rapide à déserialiser. Nous avons alors deux service implémentant IPokemonService.
  
  ```C#
var pokemons = await _pokemonService.GetPokemonsAsync();
   ```
   Récupération de tous les pokemons.
  
  ## Initialisation
  
  Les dynamic lists sont composées de 3 différentes listes. Un *Source Cache*, il contient tous nos pokémons, une liste privée qui contiendra la liste des pokémons une fois les filtres et les tris réalisés et une liste publique sur laquelle on pourra se Bind.
  
  ```C#
private SourceCache<IPokemonEntity, long> _pokemonsCache = new SourceCache<IPokemonEntity, long>(r => r.Id);
private readonly ReadOnlyObservableCollection<IPokemonEntity> _pokemons;
public ReadOnlyObservableCollection<IPokemonEntity> Pokemons => _pokemons;
```
  
Un source cache utilise des index, il est donc important que notre entité possède un Id. Notre ReadOnlyObservableCollection public renvoi la valeur de notre propriété privée.
  
## Ajout des pokemons 
  
On peut donc ajouter nos pokemons à notre source cache en utilisant la méthode AddOrUpdate.
  
```C#
_pokemonsCache.AddOrUpdate(pokemons);
```
  
## Connect
  
Il faut ensuite spécifier les connexions entre les différentes listes :
  
  ```C#
_pokemonsCache
    .Connect()
    .Bind(out _pokemons) // Bind on our private prop
    .Subscribe(); // Subscribe to updates
```

 Ce code spécifie que l'on "Connecte" le source cache à notre liste privée, on s'abonne aux différentes modifications grâce au subscribe. C'est à dire que lorsque l'on ajoute, supprime ou modifie des données dans le source cache cela provoquera une mise à jour dans notre liste privée.
 
 ## Filtres et ordonnancement
 
 L'interet d'utiliser des listes Reactive UI c'est l'ajout des filtres et du tri. Pour l'exemple je vais ajouter une barre de recherche permettant de filtrer les pokemons par leur nom.
 
 ```XML
 <Entry Text="{Binding SearchBarText}"/>
 ````
 
 J'ajoute une barre de recherche dans ma page
 
 
 ```C#
private string _searchBarText;
public string SearchBarText
{
  get => _searchBarText;
  set => this.RaiseAndSetIfChanged(ref _searchBarText, value);
}
```
    
Je bind la propriété Text de mon entry à une propriété de mon ViewModel.
    
```C#
var searchFilter = this.WhenAnyValue(viewModel => viewModel.SearchBarText)
  .Select(searchBarText =>
  {
    if (!string.IsNullOrEmpty(searchBarText))
      return new Func<IPokemonEntity, bool>(pokemon => pokemon.Name?.Contains(searchBarText) ?? false);
    else
      return new Func<IPokemonEntity, bool>(pokemon => true);
  });
```
            
Je créé alors mon filtre. A chaque changement de la propriété SearchBarText de mon ViewModel, j'actualise le filtre. Ici je teste le nom. On peut alors créer plusieurs filtres comme celui ci et les aditionner pour obtenir le résultat désiré dans notre liste publique.
            
            
```C#
_pokemonsCache
  .Connect()
  .Filter(searchFilter) // Apply the search bar filter
  .SortBy(p => p.Id) // Sort all pokemons by Id
  .Bind(out _pokemons) // Bind on our private prop
  .Subscribe(); // Subscribe to updates
```
              
J'ajoute donc mon filtre dans le process de Connect. J'en profite par ailleurs pour trier mes pokemons par leurs Id.
              
## Conclusion
              
Et voila nous avons très simplement ajouté une barre de recherche dans notre application, dans le repository vous trouverez aussi le code pour trier à l'aide d'un picker. Si vous avez la moindre question, remarque ou amélioration à proposer, n'hésitez pas. Vous pouvez aller plus loin avec Reactive UI (Car les dynamic lists ne sont qu'une partie des fonctionnalitées ajoutées) en allant sur leur documentation officielle : https://www.reactiveui.net/.
    
    
  ![Screen Recording 2022-09-23 at 22 39 45](https://user-images.githubusercontent.com/67638928/192053178-d91ac173-bc98-4af8-995e-42fa6d6a72e8.gif)

  

