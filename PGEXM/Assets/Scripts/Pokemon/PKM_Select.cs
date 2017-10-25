using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PKM_Select : MonoBehaviour {
    public GameObject[] List;
    public GameObject ListItem;
    public pkm_owner PKM_List;

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
            go.GetComponent<PKM_ListItem>().index = i;
            go.transform.parent = List[(int)Pokemon_Collections.instance.Pokemon[i].pkm_owner].transform;
            go.GetComponent<PKM_ListItem>().Init(i,Pokemon_Collections.instance.Pokemon[i].Sprite, Pokemon_Collections.instance.Pokemon[i].Name);
        }
    }
    public void StartGame() {
        int count = 0;
        foreach(Pokemon pkm in Pokemon_Collections.instance.Pokemon) {
            if (pkm.pkm_owner == pkm_owner.Player)
                count++;
        }
        if (count == 0)
            return;
        SceneManager.LoadScene("battle");
    }

}
