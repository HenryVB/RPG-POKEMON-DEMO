using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapArea : MonoBehaviour
{
    [SerializeField]
    private List<Pokemon> wildPokemonList;
    
    public Pokemon getRandomWildPokemon()
    {
        var wildPokemon = wildPokemonList[Random.Range(0, wildPokemonList.Count)];
        wildPokemon.Init();
        return wildPokemon;
    }
}
