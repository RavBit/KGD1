using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle_Base_Menu : MonoBehaviour {
    public Turn_Menu TurnMenu;
    public GameObject Button;
    public GameObject Parent;
    void Awake()
    {
        LoadInObjects();
    }

    void LoadInObjects()
    {
        switch (TurnMenu)
        {
            case Turn_Menu.Attack:
                Debug.Log("Attack");
                Attack();
                break;
            case Turn_Menu.Pokemon:
                break;
        }
    }

    void Attack()
    {
        for (int i = 0; i < Pokemon_Collections.instance.Pokemon[0].attacks.Count; i++)
        {
            GameObject Go = Instantiate(Button, Parent.transform);
            Go.GetComponentInChildren<UnityEngine.UI.Text>().text = Pokemon_Collections.instance.Pokemon[0].attacks[i].name;
        }
    }
}
