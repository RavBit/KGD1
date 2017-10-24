using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PKM_Interface : MonoBehaviour {
    public PKM_Owner Pkm_Owner;
    [SerializeField]
    private Text Name;
    [SerializeField]
    private Image HP_bar;
    [SerializeField]
    private float HP;
    [SerializeField]
    private int max_HP;


    public void Start()
    {
         Event_Manager.PlayerUpdateInterface += UpdateInterface;
    }
    public void UpdateInterface()
    {
        switch (Pkm_Owner)
        {
            case PKM_Owner.Player:
                if (Battle_Manager.instance.Trainer_Manager.GetCurPokemon() != null)
                {
                    Name.text = Battle_Manager.instance.Trainer_Manager.GetCurPokemon().Name;
                    HP_bar.fillAmount = (Battle_Manager.instance.Trainer_Manager.GetCurPokemon().Health * 0.01f);
                    max_HP = 100;
                }
                break;
            case PKM_Owner.Enemy:
                if (Battle_Manager.instance.Enemy_Manager.GetCurPokemon() != null)
                {
                    Name.text = Battle_Manager.instance.Enemy_Manager.GetCurPokemon().Name;
                    HP_bar.fillAmount = (Battle_Manager.instance.Enemy_Manager.GetCurPokemon().Health * 0.01f);
                }
                break;

        }
    }
}
