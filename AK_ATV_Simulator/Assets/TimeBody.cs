using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    bool isRecording = false;
    public float recordTime = 5f;
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
        if(Input.GetKeyDown(KeyCode.B))
            StartRecord();
        if(Input.GetKeyUp(KeyCode.B))
            StopRecord();
    }

    void FixedUpdate(){
        if(isRecording)
            Record();
        else
            Track();
    }

    void Record(){
        if(pointsInTime.Count > 0){
            PointInTime pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            rb.velocity = pointInTime.velocity;
            pointsInTime.RemoveAt(0);
        }
        else{
            StopRecord();
        }
    }
    void Track(){
        if(pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation, rb.velocity));
    }

    public void StartRecord(){
        isRecording = true;
        rb.isKinematic = true;
    }

    public void StopRecord(){
        isRecording = false;
        rb.isKinematic = false;
    }
}
