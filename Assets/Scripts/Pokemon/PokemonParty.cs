using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PokemonParty : MonoBehaviour
{
    [SerializeField]
    private List<Pokemon> pokemonList;

    public event Action OnUpdated;

    public List<Pokemon> PokemonList { 
        get => pokemonList;
        set { pokemonList = value; OnUpdated?.Invoke(); } 
    }


    private void Awake()
    {
        foreach (var pokemon in PokemonList)
        {
            pokemon.Init();
        }
    }

    private void Start()
    {
        /*
        foreach (var pokemon in PokemonList)
        {
            pokemon.Init();
        }*/
    }

    public Pokemon GetHealthyPokemon()
    {
        return PokemonList.Where(x => x.HP > 0).FirstOrDefault(); //obtiene el pokemon inicial saludable
    }

    public void AddPokemon(Pokemon newPokemon)
    {
        if (PokemonList.Count < 6)
        {
            PokemonList.Add(newPokemon);
            OnUpdated?.Invoke();
        }
    }

    public static PokemonParty GetPlayerParty()
    {
        return FindObjectOfType<PlayerController>().GetComponent<PokemonParty>();
    }

}
