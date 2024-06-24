using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Pokemon/Create new move")]
public class MoveBase : ScriptableObject
{
    [SerializeField]
    private string name;
    [TextArea]
    [SerializeField]
    private string description;
    [SerializeField]
    private PokemonType type;
    [SerializeField]
    private int power;  
    [SerializeField]
    private int accurracy;
    [SerializeField]
    private int pp;

    public string Name { get => name; set => name = value; }
    public string Description { get => description; set => description = value; }
    public PokemonType Type { get => type; set => type = value; }
    public int Power { get => power; set => power = value; }
    public int Accurracy { get => accurracy; set => accurracy = value; }
    public int Pp { get => pp; set => pp = value; }

    public bool IsSpecial {
        get {
            if (type == PokemonType.Fire || type == PokemonType.Grass || type == PokemonType.Water)
                return true;
            else return false;
        }
    }
}
