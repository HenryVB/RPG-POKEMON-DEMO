using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pokemon
{
    [SerializeField]private PokemonBase _base;
    [SerializeField]private int level;
    private Move currentMove;
    private int maxMoves = 4;
    public int HP { get; set; }
    public void Init()
    {
        HP = MaxHP;

        AddMoves();
    }

    private void AddMoves()
    {
        Moves = new List<Move>();
        foreach (var move in Base.LearnableMoves)
        {
            if (move.Level <= Level)
                Moves.Add(new Move(move.MoveBase));

            if (Moves.Count >= maxMoves)
                break;
        }
    }

    public int Attack {
        get { return Mathf.FloorToInt((Base.Atk * Level) / 100f) + 5; }
    }
    public int Defense
    {
        get { return Mathf.FloorToInt((Base.Def * Level) / 100f) + 5; }
    }
    public int SpAtk
    {
        get { return Mathf.FloorToInt((Base.SpAtk * Level) / 100f) + 5; }
    }
    public int SpDef
    {
        get { return Mathf.FloorToInt((Base.SpDef * Level) / 100f) + 5; }
    }
    public int Speed
    {
        get { return Mathf.FloorToInt((Base.Speed * Level) / 100f) + 5; }
    }
    public int MaxHP
    {
        get { return Mathf.FloorToInt((Base.MaxHP * Level) / 100f) + 10; }
    }
    public List<Move> Moves { get; set; }
    public PokemonBase Base { get => _base; set => _base = value; }
    public int Level { get => level; set => level = value; }
    public Move CurrentMove { get => currentMove; set => currentMove = value; }

    public DamageDetails TakeDamage(Move move, Pokemon attacker)
    {
        float criticalHit = 1f;
        if (Random.value * 100f <= 6.25f)
        {
            criticalHit = 2f;
        }

        float type = TypeChart.GetEffectiveness(move.Base.Type, this.Base.Type1) * TypeChart.GetEffectiveness(move.Base.Type, this.Base.Type2);

        var damageDetails = new DamageDetails()
        {
            TypeEfectiveness = type,
            Critical = criticalHit,
            Fainted = false,
        };

        float attack = move.Base.IsSpecial ? attacker.SpAtk: attacker.Attack;
        float defense = move.Base.IsSpecial ? SpDef : Defense;

        float modifiers = Random.Range(0.85f, 1f) * type * criticalHit;
        float a = (2 * attacker.level + 10) / 250f;
        float d = a * move.Base.Power * ((float) attack / defense) + 2;
        int damage = Mathf.FloorToInt(d*modifiers);

        HP-=damage;

        if(HP <= 0) {
            HP = 0;
            damageDetails.Fainted = true;
        }

        return damageDetails;
    }

    public Move DoRandomMove()
    {
        int r = Random.Range(0, Moves.Count);
        return Moves[r];
    }
}

public class DamageDetails {
    private bool fainted;
    private float critical;
    private float type;

    public bool Fainted { get => fainted; set => fainted = value; }
    public float Critical { get => critical; set => critical = value; }
    public float TypeEfectiveness { get => type; set => type = value; }
}