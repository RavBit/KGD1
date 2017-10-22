using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public abstract class Base_Trainer{
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
    }
    public void InitPokemon()
    {
        Pokemon = Pokemon_Collections.instance.Pokemon.ToList();
        int amount = 0;
        while (amount < amount_pokemon && Pkm_Owner == PKM_Owner.Enemy)
        {
            System.Random rand = new System.Random();
            int r = rand.Next(Pokemon.Count);
            if (Pokemon[r].PKM_Owner != Pkm_Owner)
            {
                Pokemon[r].PKM_Owner = Pkm_Owner;
                amount++;
            }
        }
        foreach (Pokemon pokemon in Pokemon.ToList())
            if (pokemon.PKM_Owner != Pkm_Owner)
                Pokemon.Remove(pokemon);
        CurPokemon = Pokemon[0];
    }
    public void AdjustHealth(int modifier)
    {
        CurPokemon.Health = CurPokemon.Health + modifier;
        if(CurPokemon.Health <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log("DEAD");
    }
    public Pokemon GetCurPokemon()
    {
        return CurPokemon;
    }
}
