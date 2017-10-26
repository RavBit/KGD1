using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class SM_WinCalculator : MonoBehaviour {
    [Range(0, 100)]
    [Tooltip("Spawnrate of how much the panel will spawn")]
    public float winrate = 80;
    private static float _winrate;
    public static bool firstSpin;
    public delegate void WinCalcu(SM_Item item);
    public static System.Action<SM_Item> WinCalculator;
    [SerializeField]
    public static List<SM_Item> roles;
    private void Start() {
        roles = new List<SM_Item>();
        _winrate = new float();
        _winrate = winrate;
        WinCalculator += AddFirstWin;
        Event_Manager.ResetSM += Reset;
    }

    //Check if the 'Role' so the wheel already contains the panel. If so it will spawn a new panel. Or else it will continue using the panel
    public static SM_Item CheckString(SM_Wheel wheel) {
        foreach (SM_Item panel in wheel.sm_items) {

            if (roles.Contains(panel)) {
                int random = (Random.Range(0, 70));
                if (random < _winrate) {
                    return panel;
                }
            }
        }
        System.Random rand = new System.Random();
        int r = rand.Next(wheel.sm_items.Count);
        return wheel.sm_items[r];
    }
    //Resets the calculation of the score of the slot machine
    public static void Reset() {
        roles.Clear();
    }

    //Everytime when you press a button also trigger this function. It will count the amount of buttons that are pressed and add the item to the calculation
    public void AddFirstWin(SM_Item item) {
        roles.Add(item);
        if (roles.Count == 3)
            Invoke("SwitchMode", 3);
    }
    //Switch and calculates the score of the player. Then returning to the Battle Manager
    private void SwitchMode() {
        float score = new float();
        string attack = "";
        if (roles[0].curPanel != roles[1].curPanel && roles[1].curPanel != roles[2].curPanel) {
            System.Random rand = new System.Random();
            int r = rand.Next(roles.Count);
            score = roles[r].curPanel.strength;
            attack = roles[r].curPanel.name;
        }
        if (roles[0].curPanel == roles[1].curPanel) {
            score = roles[0].curPanel.strength + roles[1].curPanel.strength;
            attack = roles[0].curPanel.name;
        }
        if (roles[1].curPanel == roles[2].curPanel) {
            score = roles[1].curPanel.strength + roles[2].curPanel.strength;
            attack = roles[1].curPanel.name;
        }
        if (roles[1].curPanel == roles[2].curPanel && roles[2].curPanel == roles[0].curPanel) {
            score = roles[0].curPanel.strength + roles[1].curPanel.strength + roles[2].curPanel.strength;
            attack = roles[0].curPanel.name;
        }
        Event_Manager.Switch_State(Turn_Menu.None);
        Event_Manager.Switch_BattleState(Battle_State.Fight);
        Battle_Manager.instance.StartAttack((int)score, attack);
    }
}
