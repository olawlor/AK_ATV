/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSwitch : MonoBehaviour
{
    public int dropdown;
    public GameObject game1;
    public GameObject game2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         dropdown = GameObject.Find("VehicleDropdown").GetComponent<VehicleDropdown>().m_DropdownValue;
        if (dropdown == 0)
        {
            Debug.Log("We're in 1");
            game1.SetActive(true);
            game2.SetActive(false);
        }
        else if (dropdown == 1)
            {
                Debug.Log("We're in 1");
                game1.SetActive(false);
                game2.SetActive(true);
            }
        
    }
}
*/