// To test with ATV, copy ATV_New prefab in file explorer
// Attach this script to both vehicles VehicleCoords in hierarchy
// otherPlayer = opposite players VehicleCoords, camera = Main Camera in CameraHUD

using UnityEngine;
using UnityEngine.UI;



public class VehicleSwitch : MonoBehaviour
{
    public GameObject otherPlayer;
    public GameObject camera1;
    public GameObject camera2;

    void OnMouseDown()
    {
        otherPlayer.GetComponent<ControllerKeyboard>().enabled = false;
        camera1.SetActive(true);
        camera2.SetActive(false);

        GetComponent<ControllerKeyboard>().enabled = true;
    }
}


