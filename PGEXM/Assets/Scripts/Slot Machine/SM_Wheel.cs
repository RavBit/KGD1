using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SM_Wheel : MonoBehaviour {
    [SerializeField]
    private GameObject sm_panel;
    [Space(10)]
    public List<SM_Item> sm_items = new List<SM_Item>();

    void Start()
    {
        InitPanels();
        foreach (SM_Item item in GetComponentsInChildren<SM_Item>())
        {
            sm_items.Add(item);
        }
    }

    void InitPanels()
    {
        GameObject go = Instantiate(sm_panel, Vector3.zero, Quaternion.identity);
        GameObject go1 = Instantiate(sm_panel, Vector3.zero , Quaternion.identity);
        GameObject go2 = Instantiate(sm_panel, Vector3.zero, Quaternion.identity);
        GameObject go3 = Instantiate(sm_panel, Vector3.zero, Quaternion.identity);
        GameObject go4 = Instantiate(sm_panel, Vector3.zero, Quaternion.identity);
        GameObject go5 = Instantiate(sm_panel, Vector3.zero, Quaternion.identity);
        go.transform.parent = transform;
        go1.transform.parent = transform;
        go2.transform.parent = transform;
        go3.transform.parent = transform;
        go4.transform.parent = transform;
        go5.transform.parent = transform;
        go1.transform.localPosition = new Vector3(0, 500, 0);
        go1.transform.localPosition = new Vector3(0, 0, 0);
        go2.transform.localPosition = new Vector3(0, 100, 0);
        go3.transform.localPosition = new Vector3(0, 200, 0);
        go4.transform.localPosition = new Vector3(0, 300, 0);
        go5.transform.localPosition = new Vector3(0, 400, 0);
    }

    public bool DuplicateWheelCheck(SM_PanelData spawnpanel)
    {
        int dup = 0;
        foreach (SM_Item panel in sm_items)
        {
            if (panel.curPanel == spawnpanel)
            {
                dup++;
                if(dup >= 2)
                    return true;
            }
        }
        return false;
    }

    public void StopWheel()
    {
        foreach (SM_Item panel in sm_items)
        {
            panel.stopping = true;
        }
        System.Random rand = new System.Random();
        int r = rand.Next(sm_items.Count);
        SM_Item item =  sm_items[r];
        Debug.Log("sm: " + sm_items[r].curPanel.name);
        sm_items[r].baseImage.rectTransform.DOLocalMove(new Vector3(0, 0, 0), 1);

    }
}