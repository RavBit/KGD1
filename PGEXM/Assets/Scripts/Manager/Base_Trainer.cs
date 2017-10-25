using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Base_Trainer :  IBase_Item{
    [SerializeField]
    protected int amount_pokemon = 3;
    [SerializeField]
    protected pkm_owner pkm_owner;
    [SerializeField]
    protected List<Pokemon> Pokemon;
    [SerializeField]
    protected Pokemon CurPokemon;
    public void Start()
    {
        Event_Manager.LoadingDataBank += InitPokemon;
        Event_Manager.PokemonAliveCheck += CheckAlive;
    }
    public void InitPokemon()
    {
        Pokemon = Pokemon_Collections.instance.Pokemon.ToList();
        int amount = 0;
        while (amount < amount_pokemon && pkm_owner == pkm_owner.Enemy)
        {
            System.Random rand = new System.Random();
            int r = rand.Next(Pokemon.Count);
            if (Pokemon[r].pkm_owner == pkm_owner.None)
            {
                Pokemon[r].pkm_owner = pkm_owner;
                amount++;
            }
        }
        foreach (Pokemon pokemon in Pokemon.ToList())
            if (pokemon.pkm_owner != pkm_owner)
                Pokemon.Remove(pokemon);
        InitCurPokemon(0);
    }
    public void InitCurPokemon(int n)
    {
        CurPokemon = Pokemon[n];
    }
    public void SetOwner()
    {
        foreach (Pokemon pokemon in Pokemon)
        {
            pokemon.pkm_owner = pkm_owner;
        }
    }
    public void AdjustHealth(int modifier)
    {
        CurPokemon.Health = CurPokemon.Health + modifier;
    }

    protected void CheckAlive()
    {
        Debug.Log("CHECKING ALIVE");
        if (CurPokemon.Health <= 0)
            Die();
    }
    void Die()
    {
        Event_Manager.Pokemon_Kill(pkm_owner);
        CurPokemon = null;
    }
    public Pokemon GetCurPokemon()
    {
        return CurPokemon;
    }
    public void SetCurPokemon(Pokemon _pkm)
    {
        Debug.Log("Cur pokemon set");
        CurPokemon = _pkm;
    }
    public List<Pokemon> GetCurPokemonList()
    {
        return Pokemon;
    }
}

public interface IBase_Item
{
    void AdjustHealth(int modifier);
}