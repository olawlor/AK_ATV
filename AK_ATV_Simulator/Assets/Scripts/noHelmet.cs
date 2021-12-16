using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noHelmet : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject helmet;
    public GameObject atvRespawnPoint;
    
    // Update is called once per frame
    IEnumerator HelmetCheck()
    {
        if(!helmet.activeSelf){
            this.transform.rotation = atvRespawnPoint.transform.rotation;
            this.transform.position = atvRespawnPoint.transform.position;
            GetComponent<Rigidbody>().isKinematic = true;
            yield return 0;
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
