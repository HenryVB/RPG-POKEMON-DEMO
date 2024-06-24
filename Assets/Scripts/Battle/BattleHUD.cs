using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    [SerializeField]
    private Text nameText;

    [SerializeField]
    private Text levelText;

    [SerializeField]
    private HPBar hpBar;

    public void SetData(Pokemon pokemon)
    {
        nameText.text = pokemon.Base.name;
        levelText.text = "Lvl " + pokemon.Level;
        hpBar.SetHP((float)pokemon.HP/pokemon.MaxHP);
    }

    public IEnumerator UpdateHP(Pokemon pokemon)
    {
        yield return hpBar.SetHPSmooth((float)pokemon.HP / pokemon.MaxHP);
    }
}
