using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PKM_Interface : MonoBehaviour {
    public pkm_owner pkm_owner;
    [SerializeField]
    private Text name;
    [SerializeField]
    private Image hp_bar;


    public void Start() {
        Event_Manager.PlayerUpdateInterface += UpdateInterface;
    }

    //Update the interface of the enemy and the player. This will Update once called the event
    public void UpdateInterface() {
        switch (pkm_owner) {
            case pkm_owner.Player:
                if (Battle_Manager.instance.trainer_Manager.GetCurPokemon() != null) {
                    name.text = Battle_Manager.instance.trainer_Manager.GetCurPokemon().name;
                    hp_bar.fillAmount = (Battle_Manager.instance.trainer_Manager.GetCurPokemon().health * 0.01f);
                }
                break;
            case pkm_owner.Enemy:
                if (Battle_Manager.instance.enemy_Manager.GetCurPokemon() != null) {
                    name.text = Battle_Manager.instance.enemy_Manager.GetCurPokemon().name;
                    hp_bar.fillAmount = (Battle_Manager.instance.enemy_Manager.GetCurPokemon().health * 0.01f);
                }
                break;

        }
    }
}
