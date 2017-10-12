using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SM_Panels : MonoBehaviour {
    public static SM_Panels instance;
    public List<SM_PanelData> paneldata = new List<SM_PanelData>();


    void Awake()
    {
        if (instance != null)
            Debug.LogError("More than one SM Panels in the scene");
        else
            instance = this;
        DontDestroyOnLoad(transform.gameObject);
    }

    public SM_PanelData PanelSpawn(SM_PanelData cur, SM_Wheel wheelcheck)
    {
        foreach (SM_PanelData panel in paneldata)
        {
            int rand = Random.Range(0, 100);
            if (rand <= panel.spawnrate && !wheelcheck.DuplicateWheelCheck(panel))
                return panel;

        }

        return cur;
    }
}
