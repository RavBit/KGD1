using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class SM_WinCalculator : MonoBehaviour {
    [Range(0, 100)]
    [Tooltip("Spawnrate of how much the panel will spawn")]
    public float winrate = 80;
    private static float _winrate;
    public static bool FirstSpin = false;
    public delegate void WinCalcu(SM_Item item);
    public static System.Action<SM_Item> WinCalculator;
    [SerializeField]
    public static List<SM_Item> Roles;
    void Start()
    {
        Roles = new List<SM_Item>();
        _winrate = new float();
        _winrate = winrate;
        WinCalculator += AddFirstWin;
        Event_Manager.ResetSM += Reset;
    }

    public static SM_Item CheckString(SM_Wheel wheel)
    {
        foreach (SM_Item panel in wheel.sm_items)
        {

            if (Roles.Contains(panel))
            {
                int random = (Random.Range(0, 70));
                if (random < _winrate)
                {
                    return panel;
                }
            }
        }
        System.Random rand = new System.Random();
        int r = rand.Next(wheel.sm_items.Count);
        return wheel.sm_items[r];
    }
    public static void Reset()
    {
        Roles.Clear();
    }
       
    public void AddFirstWin(SM_Item item)
    {
        Roles.Add(item);
        if (Roles.Count == 3)
            Invoke("SwitchMode", 3);
    }
    void SwitchMode() {
        float score = new float();
        score = Roles[0].curPanel.strength + Roles[1].curPanel.strength + Roles[2].curPanel.strength;
        Debug.Log("Score: " + score);
        Event_Manager.Switch_State(Turn_Menu.None);
        Event_Manager.Switch_BattleState(Battle_State.Fight);
        Battle_Manager.instance.ATTACKMngr((int)score);
    }
}
