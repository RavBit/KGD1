using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SM_Wheel : MonoBehaviour {
    [SerializeField]
    private GameObject sm_panel;
    [Space(10)]
    public List<SM_Item> sm_items = new List<SM_Item>();

    //Add the items to the wheel
    void Start() {
        InitPanels();
        foreach (SM_Item item in GetComponentsInChildren<SM_Item>()) {
            sm_items.Add(item);
        }
    }

    //Init the panels one time (This is a really bad implementation but I didn't had time to fix this)
    private void InitPanels() {
        GameObject go = Instantiate(sm_panel, Vector3.zero, Quaternion.identity);
        GameObject go1 = Instantiate(sm_panel, Vector3.zero, Quaternion.identity);
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
        go.transform.localPosition = new Vector3(0, 500, 0);
        go1.transform.localPosition = new Vector3(0, 0, 0);
        go2.transform.localPosition = new Vector3(0, 100, 0);
        go3.transform.localPosition = new Vector3(0, 200, 0);
        go4.transform.localPosition = new Vector3(0, 300, 0);
        go5.transform.localPosition = new Vector3(0, 400, 0);
    }

    //Check for duplicates in the wheel. If there are more than 2 duplicates it will not spawn this panel again
    public bool DuplicateWheelCheck(SM_PanelData spawnpanel) {
        int dup = 0;
        foreach (SM_Item panel in sm_items) {
            if (panel.curPanel == spawnpanel) {
                dup++;
                if (dup >= 2)
                    return true;
            }
        }
        return false;
    }
    //Stop the wheel and give the score to the SM_Wincalculator
    public void StopWheel() {
        SM_Item item = null;
        if (!SM_WinCalculator.firstSpin) {
            SM_WinCalculator.firstSpin = true;
            System.Random rand = new System.Random();
            int r = rand.Next(sm_items.Count);
            item = sm_items[r];
        } else {
            item = SM_WinCalculator.CheckString(GetComponent<SM_Wheel>());
        }
        StartCoroutine("StopWheels", item);

    }
    //Stopping the wheel on the calculated panel
    IEnumerator StopWheels(SM_Item curitem) {
        SM_WinCalculator.WinCalculator(curitem);
        while (curitem.transform.localPosition.y != 0) {
            yield return new WaitForSeconds(0.01f);
        }
        if (curitem.transform.localPosition.y == 0) {
            foreach (SM_Item panel in sm_items) {
                panel.stopping = true;
            }
        }

    }

}