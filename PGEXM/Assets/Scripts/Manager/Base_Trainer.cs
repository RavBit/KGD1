using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Base_Trainer :  IBase_Item{
    [SerializeField]
    protected int amount_pokemon = 3;
    [SerializeField]
    protected PKM_Owner Pkm_Owner;
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
        while (amount < amount_pokemon && Pkm_Owner == PKM_Owner.Enemy)
        {
            System.Random rand = new System.Random();
            int r = rand.Next(Pokemon.Count);
            if (Pokemon[r].PKM_Owner == PKM_Owner.None)
            {
                Pokemon[r].PKM_Owner = Pkm_Owner;
                amount++;
            }
        }
        foreach (Pokemon pokemon in Pokemon.ToList())
            if (pokemon.PKM_Owner != Pkm_Owner)
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
            pokemon.PKM_Owner = Pkm_Owner;
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
        Event_Manager.Pokemon_Kill(Pkm_Owner);
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