using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleScenario : MonoBehaviour
{
    private enum scenario_state { FREEROAM, BEGIN, IN_PROGRESS, END };
    private uint cur_scenario = (uint)scenario_state.FREEROAM;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    public void Update_Scenario() {

    }
}
