using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passengerScript : MonoBehaviour
{
    public GameObject player;
    public float followSharpness = 0.1f;

    Vector3 offset;

    void Start()
    {
        // Cache the initial offset at time of load/spawn:
        offset = transform.position - player.transform.position;
    }

    void FixedUpdate()
    {
        // Apply that offset to get a target position.
        Vector3 targetPosition = player.transform.position + offset;

        // Keep our y position unchanged.
        targetPosition.y = transform.position.y;

        // Smooth follow.    
        transform.position += (targetPosition - transform.position) * followSharpness;
    }
    
    public void setActive()
    {
        Vector3 targetPosition = player.transform.position + offset;
        targetPosition.y = transform.position.y;
        transform.position += (targetPosition - transform.position) * followSharpness;
        this.gameObject.SetActive(true);
    }
}
