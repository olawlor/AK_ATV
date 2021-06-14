using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioCheckpointTriggerTimer : MonoBehaviour
{
    public GameObject startTrigger;
    public GameObject nextTrigger;

    // Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    private void OnTriggerEnter(Collider other) {
        //Debug.Log("Trigger entered by " + other.gameObject.tag);
        if (other.gameObject.tag == "Player") {
            Debug.Log("checkpoint reached for " + other.gameObject.tag);
            this.gameObject.SetActive(false);
            nextTrigger.SetActive(true);
        }
    }
}
