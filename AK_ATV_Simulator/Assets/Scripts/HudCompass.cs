/*
 Rotate the compass needle to match the current mission end target.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudCompass : MonoBehaviour
{
    private MeshRenderer mesh;
    
    // Start is called before the first frame update
    void Start()
    {
        mesh=gameObject.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject end = VehicleScenario.MissionEnd;
        mesh.enabled=(end!=null);
        
        if (end!=null) {
            // Figure out the world-coordinates direction to the mission end
            Vector3 endDir = end.transform.position - transform.position;
            
            // Rotate that vector into our parent's local coordinates
            //   (subtle: we can't rotate into our own coordinates, because we rotate!)
            Vector3 localDir = transform.parent.InverseTransformVector(endDir);
            
            // Convert to a heading angle (in horizontal plane only)
            float heading = Mathf.Atan2(localDir.x, localDir.z) * Mathf.Rad2Deg; 
            
            // Write that angle to our rotation
            transform.localEulerAngles=new Vector3(-heading,0,0);
        }
    }
}
