using System;
namespace TosReactiveUI.Models.Interfaces;

public interface IPokemonEntity
{
    long Id { get; set; }
    string Name { get; set; }
    string FrontSprite { get; set; }
    string BackSprite { get; set; }
    IEnumerable<string> Types { get; set; }
}

