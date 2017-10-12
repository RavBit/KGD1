using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PKM_Select : MonoBehaviour {
    public GameObject[] List;
    public GameObject ListItem;
    public PKM_Owner PKM_List;

    public static PKM_Select instance;
    void Awake() {
        if (instance != null)
            Debug.LogError("More than one PKM_Select in the scene");
        else
            instance = this;
    }

    private void Start() {
        Init();
    }

    public void Init() {
        for (int i = 0; i < 3; i++) {
            foreach (Transform child in List[i].transform) {
                GameObject.Destroy(child.gameObject);
            }
        }
        for (int i = 0; i < Pokemon_Collections.instance.Pokemon.Count; i++) {
            GameObject go = Instantiate(ListItem, Vector3.zero, transform.rotation);
            Debug.Log("Init owner: " + Pokemon_Collections.instance.Pokemon[i].PKM_Owner);
            go.GetComponent<PKM_ListItem>().index = i;
            go.transform.parent = List[(int)Pokemon_Collections.instance.Pokemon[i].PKM_Owner].transform;
            go.GetComponent<PKM_ListItem>().Init(i,Pokemon_Collections.instance.Pokemon[i].Sprite, Pokemon_Collections.instance.Pokemon[i].Name);
        }
    }

}
