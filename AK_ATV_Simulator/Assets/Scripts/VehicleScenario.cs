using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehicleScenario : MonoBehaviour
{
    private enum scenario_state { FREEROAM, STARTING, IN_PROGRESS, ENDING };
    private scenario_state state = scenario_state.FREEROAM;

    public GameObject scenarioMenu;
    public Text scenarioAnnounce;
    public Text scenarioDesc;
    public GameObject[] passengers;
    public VehicleScenario vehicle;
    public bool inScenario = false;
    public GameObject[] scenarioStartPoints;
    void SetStartsActive(bool active)
    {
        foreach (GameObject sp in scenarioStartPoints) sp.SetActive(active);
    }


    // Start is called before the first frame update
    void Start()
    {
        state = scenario_state.FREEROAM;
    }
    void Update()
    {
        if (inScenario)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                EndScenario("You've exited the scenario.");
                foreach (GameObject passenger in passengers)
                {
                    passenger.SetActive(false);

                }
                inScenario = false;
            }
        }
    }

    // Bring up menu at scenario start
    private void BeginScenarioMenu(string msg)
    {
        state = scenario_state.STARTING; //<- bring up starting scenario menu
        inScenario = true;
        scenarioMenu.SetActive(true);
        Time.timeScale = 0f;
        scenarioAnnounce.text = "New Scenario";
        scenarioDesc.text = msg;

        // No other mission is active:
        SetStartsActive(false);
    }

    // Bring up menu at scenario end
    private void EndScenarioMenu(string msg)
    {
        state = scenario_state.ENDING;
        scenarioMenu.SetActive(true);
        Time.timeScale = 0f;
        scenarioAnnounce.text = "Scenario Complete";
        scenarioDesc.text = msg;
        inScenario = false;

        // Any (other) mission can be active now:
        SetStartsActive(true);
    }

    // This is called by Canvas -> ScenarioButton -> ResumeButton to exit a menu.
    public void Resume()
    {
        scenarioMenu.SetActive(false);
        Time.timeScale = 1f;
        UpdateScenario();
    }

    // Advance the finite state machine for the scenario handling.
    public void UpdateScenario()
    {
        if (state == scenario_state.STARTING) // exit the menu with mission description
        {
            Debug.Log("Scenario in progress...");
            state = scenario_state.IN_PROGRESS;
        }
        else if (state == scenario_state.ENDING) // exit the mission-done modal menu
        {
            Debug.Log("Returning to freeroam");
            state = scenario_state.FREEROAM;
        }
        else
        {
            Debug.Log("ERROR: Impossible state reached in Vehicle Scenario.UpdateScenario");
        }
    }

    /* Public static members store the hud-visible scenario state: */
    public static string MissionGoal;  ///< mission goal text
    public static float MissionGoalTime = 10000.0f; // last time mission text changed

    // Set this as the new mission goal
    public static void UpdateMissionGoal(string msg)
    {
        Debug.Log("Updating mission text to " + msg);
        VehicleScenario.MissionGoal = msg;
        VehicleScenario.MissionGoalTime = Time.time;
    }


    public static GameObject MissionStart; ///< stores mission start trigger (so it can be reactivated)
    public static GameObject MissionEnd; ///< mission end trigger object (or null if none active)

    /* Start a new scenario: */
    public void StartScenario(string msg, GameObject startTrigger, GameObject endTrigger)
    {
        Debug.Log("Starting scenario " + msg);

        startTrigger.SetActive(false); //<- turn off the current trigger (avoid re-triggering)
        endTrigger.SetActive(true); ///<- turn on the end trigger so user can finish

        // Store data in static variables, for access by the HUD
        VehicleScenario.UpdateMissionGoal(msg);
        VehicleScenario.MissionStart = startTrigger;
        VehicleScenario.MissionEnd = endTrigger;

        // Bring up start menu:
        BeginScenarioMenu(msg);
    }

    /* Update the end trigger (for compass) */
    public void UpdateEnd(GameObject endTrigger)
    {
        VehicleScenario.MissionEnd = endTrigger;
    }

    /* Finish the current scenario: */
    public void EndScenario(string msg)
    {
        Debug.Log("Ending scenario " + msg);

        // Bring up end menu:
        //EndScenarioMenu(msg);
        SetStartsActive(true);

        VehicleScenario.MissionEnd.SetActive(false); ///<- hide end arrow
        VehicleScenario.MissionStart.SetActive(false); ///<- don't allow user to retry last mission immediately

        VehicleScenario.UpdateMissionGoal(msg);
        VehicleScenario.MissionStart = null;
        VehicleScenario.MissionEnd = null;
        inScenario = false;
    }
}
