using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public Vector3 offset;
    // Update is called once per frame
    // Transform.position is the position of the object
    //  the file is attributed to (in this case the 
    //      camera)
    void Update()
    {
        // "+offset" allows the camera to follow the player
        //      at a certain position
        transform.position = player.position + offset;
    }
}
