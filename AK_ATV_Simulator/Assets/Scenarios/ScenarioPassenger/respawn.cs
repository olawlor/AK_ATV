using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn : MonoBehaviour
{
    /*! The passenger2 object */
    public GameObject passenger2;

    /*! The atv respawn position */
    public GameObject atvRespawnPoint;

    /*! The original passenger2 position !*/
    public GameObject passenger2RespawnPoint;

    /*! This coroutine function respawns the atv and passenger2 at respawn points designated by the 
     * objects PassengerScenario_atvrespawnPoint and PassengerScenario_passenger2RespawnPoint
     * if the atv gets too far away from passenger2 */
    /*! This is a coroutine because just respawning the objects doesn't negate 
     * the forces that are acting upon it */
    /*! Therefore we need to set the rigidbodies of the atv and passenger2 
     * to kinimatic for one frame, which is possible by setting isKinematic to true and then returning
     * and the setting them back to false. The returning can only be done in a coroutine */
    IEnumerator<int> Test(float distance)
    {
        if (distance > 2)
        {
            this.transform.rotation = atvRespawnPoint.transform.rotation;
            passenger2.transform.rotation = passenger2RespawnPoint.transform.rotation;

            this.transform.position = atvRespawnPoint.transform.position;
            passenger2.transform.position = passenger2RespawnPoint.transform.position;

            passenger2.GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Rigidbody>().isKinematic = true;
            yield return 0;
            GetComponent<Rigidbody>().isKinematic = false;
            passenger2.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    // Update is called once per frame
    /*! Here we determine the distance between the atv and passenger2 */
    void Update()
    {
        Vector3 passengerPosition = passenger2.transform.position;
        float distance = Vector3.Distance(this.transform.position, passengerPosition);
        StartCoroutine(Test(distance));
    }
}
