using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Pokemon
{
    public string name;
    public int id;
    public List<PokemonType> types;
    public List<Stat> stats;
    public List<Ability> abilities;
    public List<Move> moves;
    public SpriteInfo sprites;
    public int height;
    public int weight;
}

[System.Serializable]
public class PokemonType
{
    public TypeDetail type;
}

[System.Serializable]
public class TypeDetail
{
    public string name;
}

[System.Serializable]
public class Stat
{
    public StatDetail stat;
    public int base_stat;
}

[System.Serializable]
public class StatDetail
{
    public string name;
}

[System.Serializable]
public class Ability
{
    public AbilityDetail ability;
}

[System.Serializable]
public class AbilityDetail
{
    public string name;
}

[System.Serializable]
public class Move
{
    public MoveDetail move;
}

[System.Serializable]
public class MoveDetail
{
    public string name;
}

[System.Serializable]
public class SpriteInfo
{
    public string front_default;
    public string back_default;
}
