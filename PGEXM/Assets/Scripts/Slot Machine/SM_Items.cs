using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Interface of the SM_BaseItem. With the Init and Moving as the most important voids
public interface SM_Items {

    void Init();
    void Move(Vector3 speed);

}