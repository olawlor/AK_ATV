using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCollision : MonoBehaviour
{
    // Start is called before the first frame update

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "water"){
            BroadcastMessage("StartRewind");
        }
    }
}
