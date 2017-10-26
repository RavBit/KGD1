using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle_Base_Menu : MonoBehaviour {
    public Turn_Menu TurnMenu;
    public GameObject button;
    public GameObject parent;
    void Awake() {
        LoadInObjects();
    }

    //Load in the Objects when you press attack or pokemon
    void LoadInObjects() {
        switch (TurnMenu) {
            case Turn_Menu.Attack:
                Attack();
                break;
            case Turn_Menu.Pokemon:
                break;
        }
    }

    //When the Turn-Menu jumps on 'Attack' the player or enemy attacks
    void Attack() {
        for (int i = 0; i < Pokemon_Collections.instance.Pokemon[0].attacks.Count; i++) {
            GameObject Go = Instantiate(button, parent.transform);
            Go.GetComponentInChildren<UnityEngine.UI.Text>().text = Pokemon_Collections.instance.Pokemon[0].attacks[i].name;
        }
    }
}
