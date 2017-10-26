using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SM_BaseItem : MonoBehaviour, SM_Items {

    //Event
    [SerializeField]
    public bool stopping = false;
    [SerializeField]
    public static float speed;
    [SerializeField]
    public SM_PanelData curPanel;
    public Image baseImage;
    private void Start() {
        Event_Manager.ResetSM += Init;
        Init();
    }
    //Init the panels and move them with a speed of 10
    public void Init() {
        stopping = false;
        speed = 10;
        baseImage = GetComponent<Image>();
    }

    //Check if they are stopping or not
    public void FixedUpdate() {
        if (!stopping)
            Move(Vector3.down * speed);
    }
    //Move the players and move them up if they reach an certain y-pos.  also changing the panel his image and type if they have to
    public void Move(Vector3 speed) {
        baseImage.rectTransform.Translate(speed);
        if (baseImage.rectTransform.localPosition.y <= -300) {
            if (!stopping) {
                curPanel = SM_Panels.instance.PanelSpawn(curPanel, GetComponentInParent<SM_Wheel>());
                baseImage.sprite = curPanel.image;
            }
            baseImage.transform.localPosition = new Vector3(0, 300, 0);
        }
    }
}

