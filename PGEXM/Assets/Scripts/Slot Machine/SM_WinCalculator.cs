using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class SM_WinCalculator : MonoBehaviour {
    [Range(0, 100)]
    [Tooltip("Spawnrate of how much the panel will spawn")]
    public static float winrate = 80;

    public static bool FirstSpin = false;
    public delegate void WinCalcu(SM_Item item);
    public static System.Action<SM_Item> WinCalculator;
    [SerializeField]
    public static List<string> Roles;
    void Start()
    {
        Roles = new List<string>();
        winrate = new float();
        WinCalculator += AddFirstWin;
    }

    public static SM_Item CheckString(SM_Wheel wheel)
    {
        foreach (SM_Item panel in wheel.sm_items)
        {

            if (Roles.Contains(panel.curPanel.name))
            {
                int winrate2 = 30;
                int test = (Random.Range(0, 70));
                Debug.Log("test:  " + test + " winrate:  " + winrate2);
                if (test < winrate2)
                {
                    Debug.Log("RANDOM " + test);
                    return panel;
                }
                else
                {
                    Debug.Log("GOING FOR THE ELSE");
                    System.Random rand = new System.Random();
                    int r = rand.Next(wheel.sm_items.Count);
                    return wheel.sm_items[r];
                }
            }
        }
        System.Random rand2 = new System.Random();
        int r2 = rand2.Next(wheel.sm_items.Count);
        return wheel.sm_items[r2];
    }
       
    public void AddFirstWin(SM_Item item)
    {
        Roles.Add(item.curPanel.name);
    }

    public string AddChance()
    {
        if (Roles[0] != null)
        {
            float c = Random.Range(0, winrate);
            if (c < winrate)
                return Roles[0];
        }
        return null;
    }
}
