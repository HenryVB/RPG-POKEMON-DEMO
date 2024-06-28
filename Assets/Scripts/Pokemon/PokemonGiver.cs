using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonGiver : MonoBehaviour
{
    [SerializeField] private List<Pokemon> pokemonsToGive;
    [SerializeField] private Dialog dialog;

    private Pokemon pokemonToGive;

    bool used = false;

    public Pokemon PokemonToGive { get => pokemonToGive; set => pokemonToGive = value; }

    public IEnumerator GivePokemon(PlayerController player)
    {
        yield return DialogManager.Instance.ShowDialog(dialog);


        SelectRandomPokemon();

        //add pokemon to player party
        player.GetComponent<PokemonParty>().AddPokemon(pokemonToGive);

        used = true;

        AudioManager.instance.PlaySfx(AudioId.PokemonObtained, pauseMusic: true);
        string dialogText = $"{player.Name} received {PokemonToGive.Base.Name}";

        yield return DialogManager.Instance.ShowDialogText(dialogText);
    }

    public bool CanBeGiven()
    {
        //return PokemonToGive != null && !used;
        return pokemonsToGive != null && !used;
    }

    private void SelectRandomPokemon()
    {
        //Select random pokemon from list
        int indexPokemonToGive = Random.Range(0, pokemonsToGive.Count);
        pokemonToGive = pokemonsToGive[indexPokemonToGive];
        pokemonToGive.Init();
    }
}
