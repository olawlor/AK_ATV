using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helmetToggle : MonoBehaviour
{
    public GameObject missingHelmet;

    public GameObject helmet;
    
        void Start()
    {
        missingHelmet.SetActive(true);
        helmet.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("h")){
            if(missingHelmet.activeSelf){
                missingHelmet.SetActive(false);
                helmet.SetActive(true);
            }else{
                missingHelmet.SetActive(true);
                helmet.SetActive(false);
            }
        }
        
    }
}
