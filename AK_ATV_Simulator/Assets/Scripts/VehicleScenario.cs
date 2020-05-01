using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehicleScenario : MonoBehaviour
{
    private enum scenario_state { FREEROAM, BEGIN, IN_PROGRESS, END };
    private scenario_state cur_scenario = scenario_state.FREEROAM;

    public GameObject scenarioMenu;
    public Text scenarioAnnounce;
    public Text scenarioDesc;

    public GameObject[] scenarioStartPoints;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    public void Resume() {
        scenarioMenu.SetActive(false);
        Time.timeScale = 1f;
        Update_Scenario();
    }

    private void BeginScenario(string msg) {
        scenarioMenu.SetActive(true);
        Time.timeScale = 0f;
        scenarioAnnounce.text = "! New Scenario:";
        scenarioDesc.text = msg;
        foreach (GameObject sp in scenarioStartPoints) sp.SetActive(false);
    }

    private void EndScenario(string msg) {
        scenarioMenu.SetActive(true);
        Time.timeScale = 0f;
        scenarioAnnounce.text = "! Info:";
        scenarioDesc.text = msg;
        foreach (GameObject sp in scenarioStartPoints) sp.SetActive(true);
    }

    public void Update_Scenario(params string[] msg) {
        if (cur_scenario == scenario_state.FREEROAM)
        {
            cur_scenario = scenario_state.BEGIN;
            BroadcastMessage("BeginScenario", msg[0]);
        }
        else if (cur_scenario == scenario_state.BEGIN)
        {
            cur_scenario = scenario_state.IN_PROGRESS;
        }
        else if (cur_scenario == scenario_state.IN_PROGRESS)
        {
            cur_scenario = scenario_state.END;
            BroadcastMessage("EndScenario", msg[0]);
        }
        else if (cur_scenario == scenario_state.END)
        {
            cur_scenario = scenario_state.FREEROAM;
        }
        else
        {
            // This state should never be reached
        }
    }
}
