using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

[CreateAssetMenu(fileName = "Pokemon", menuName ="Pokemon/Create new pokemon")]
public class PokemonBase : ScriptableObject
{
    [SerializeField]
    private string name;
    [TextArea]
    [SerializeField]
    private string description;
    [SerializeField]
    private Sprite frontSprite;
    [SerializeField] 
    private Sprite backSprite;
    [SerializeField]
    private PokemonType type1;
    [SerializeField]
    private PokemonType type2;

    [SerializeField]
    private int maxHP;
    [SerializeField]
    private int atk;
    [SerializeField]
    private int def;
    [SerializeField]
    private int spAtk;
    [SerializeField]
    private int spDef;
    [SerializeField]
    private int speed;

    [SerializeField]
    private List<LearnableMove> learnableMoves;

    public string Name { get => name; set => name = value; }
    public string Description { get => description; set => description = value; }
    public Sprite FrontSprite { get => frontSprite; set => frontSprite = value; }
    public Sprite BackSprite { get => backSprite; set => backSprite = value; }
    public PokemonType Type1 { get => type1; set => type1 = value; }
    public PokemonType Type2 { get => type2; set => type2 = value; }
    public int MaxHP { get => maxHP; set => maxHP = value; }
    public int Atk { get => atk; set => atk = value; }
    public int Def { get => def; set => def = value; }
    public int SpAtk { get => spAtk; set => spAtk = value; }
    public int SpDef { get => spDef; set => spDef = value; }
    public int Speed { get => speed; set => speed = value; }
    public List<LearnableMove> LearnableMoves { get => learnableMoves; set => learnableMoves = value; }
}

[System.Serializable]
public class LearnableMove
{
    [SerializeField] private MoveBase moveBase;
    [SerializeField] private int level;

    public MoveBase MoveBase { get => moveBase; set => moveBase = value; }
    public int Level { get => level; set => level = value; }
}

public enum PokemonType
{
    None,
    Normal,
    Fire,
    Water,
    Grass,
    Poison
}

public class TypeChart
{
    static float[][] chart = {
    
    //                        NOR, FIR,WATER,GRASS,POISON   
    /*Normal*/    new float[] {1f, 1f,  1f,    1f,     1f},
    /*Fire*/    new float[] {1f,   0.5f,  1f,    2f,     1f}, 
    /*Water*/    new float[] {1f,  2f, 0.5f,    1f,     1f},
    /*Grass*/    new float[] {1f,  0.5f,  2f,    0.5f,     0.5f}, 
    /*Poison*/    new float[] {1f, 1f,  1f,    2f,     0.5f},

    };

    public static float GetEffectiveness (PokemonType atkType,PokemonType defType)
    {
        if (atkType == PokemonType.None || defType == PokemonType.None)
            return 1;

        int row = (int)atkType - 1;
        int col = (int)defType - 1;

        return chart[row][col];
    }
}