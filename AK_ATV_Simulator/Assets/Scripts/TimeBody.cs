using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    bool isRewinding = false;
    Vector3 veloBeforeRewind;
    public float rewindTime = 5.0f;
    List<PointInTime> pointsInTime;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        pointsInTime = new List<PointInTime>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B)){
            StartRewind();
        }
        if(Input.GetKeyUp(KeyCode.B)){
            StopRewind();
        }
    }

    void FixedUpdate(){
        if(isRewinding)
            Rewind();
        else
            Track();
    }

    void Rewind(){
        if(pointsInTime.Count > 0){
            PointInTime pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            rb.velocity = pointInTime.velocity;
            pointsInTime.RemoveAt(0);
        }
        else{
            StopRewind();
        }
    }
    void Track(){
        if(pointsInTime.Count > Mathf.Round(rewindTime / Time.fixedDeltaTime))
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation, rb.velocity));
    }

    public void StartRewind(){
        veloBeforeRewind = rb.velocity;
        isRewinding = true;
        rb.isKinematic = true;
    }

    public void StopRewind(){
        rb.velocity = veloBeforeRewind;
        isRewinding = false;
        rb.isKinematic = false;
    }
}
