using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SM_Image : MonoBehaviour {
    private Sequence sequence;
    private Image thisImage;

        // Use this for initialization
        void Start () {
            sequence = DOTween.Sequence();
            thisImage = GetComponent<Image>();
            //thisImage.transform.DOLocalMoveY(300, 10, false);
        }

        // Update is called once per frame
        void Update () {
            sequence.Kill();
        thisImage.rectTransform.Translate(Vector3.up * 10);
        if (thisImage.rectTransform.localPosition.y >= 300)
            {
                thisImage.transform.localPosition = new Vector3(0, -300, 0);
            }
        }
    }
