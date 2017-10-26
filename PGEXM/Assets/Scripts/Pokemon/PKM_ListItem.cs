using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PKM_ListItem : MonoBehaviour {
    public Image PokeImage;
    public GameObject Moves;
    public GameObject AttackItem;
    public Text Name;
    public Text HP;
    public GameObject Attack;
    public int index;
    private int _switch = 0;

    //Init the panel with the data from the Poke_Collections
    public void Init(int i, Sprite img, string name) {
        PokeImage.sprite = img;
        Name.text = name;
        HP.text = "HP: " + Pokemon_Collections.instance.Pokemon[i].health;
        for (int x = 0; x < Pokemon_Collections.instance.Pokemon[i].attacks.Count; x++) {
            GameObject go = Instantiate(AttackItem, Vector3.zero, transform.rotation);
            go.transform.parent = Attack.transform;
            go.GetComponentInChildren<Text>().text = Pokemon_Collections.instance.Pokemon[i].attacks[x].name;
        }
    }
    //Toggle the moves screen to see what moves the player has
    public void ToggleMoves() {
        Moves.gameObject.SetActive(!Moves.gameObject.activeInHierarchy);
    }
    //Equip the pokemon and change the enum 'pkm_owner' to it
    public void Equip() {
        switch (Pokemon_Collections.instance.Pokemon[index].pkm_owner) {
            case pkm_owner.Player:
                Pokemon_Collections.instance.Pokemon[index].pkm_owner = pkm_owner.None;
                break;
            case pkm_owner.None:
                Pokemon_Collections.instance.Pokemon[index].pkm_owner = pkm_owner.Player;
                break;
        }

        PKM_Select.instance.Init();
    }
}
