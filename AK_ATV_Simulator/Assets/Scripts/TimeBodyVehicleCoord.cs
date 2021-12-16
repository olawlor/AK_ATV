/*! \file TimeBody.cs
/*! \file TimeBody.cs
 * \brief The source for the class TimeBody
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBodyVehicleCoord : MonoBehaviour
{
    /*! \var isRewinding
     * \brief a flag for checking if the vehicle is reversing
     */
    bool isRewinding = false;

    /*! \var veloBeforeRewind
     * \brief stores the velocity of the vehicle before it started reversing
     */
    Vector3 veloBeforeRewind;

    /*! \var rewindTime
     * \brief stores the value for the allotted time for reversing
     */
    public float rewindTime = 5.0f;

    /*! \var pointsInTime
     * \brief a list of PointInTime objects as defined in PointInTime.cs
     */
    List<PointInTime> pointsInTime;

    /*! \var rb
     * \brief the variable representation of the object that the script is attached to
     */
    Rigidbody rb;

    /*! \fn start()
     * Called before the first frame update. 
     * Defines pointsInTime and the variable for the rigid body
     */
    void Start()
    {
        pointsInTime = new List<PointInTime>();
        rb = GetComponent<Rigidbody>();
    }

    /*! \fn Update()
    * Called once per frame
    * Checks if the user pressed B and if they depressed B
    *   if B is pressed then StartRewind is called
    *   if B is depressed then StopRewind is called
    */
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B)){
            StartRewind();
        }
        if(Input.GetKeyUp(KeyCode.B)){
            StopRewind();
        }
    }

   /*! \fn FixedUpdate()
   * Called on a fixed interval determined by Time.fiexDeltaTime
   * Checks if the isRewinding Flag is set to true
   *   if true then Rewind is called
   *   else then Track is called
   */
    void FixedUpdate(){
        if(isRewinding)
            Rewind();
        else
            Track();
    }

    /*! \fn Rewind()
     * if pointsInTime has atleast one point in it
     *      Then set the position, rotation, and velocity of the vehicle to the first point in time stored in pointsInTime
     * else 
     *      then reversing is stopped by calling StopRewind
     */
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

    /*! \fn Track()
    * remove the last point in pointsInTime
    *   if pointsInTime has more points in it than the value of rewindTime/Time.fixedDeltaTime
    *       The ratio is effectively the number of times Track is called in 5 seconds (defined by the variable rewindTime).
    *       Therefore the if statement effectively reads "after a certain amount of points are added start removing them from the list." 
    *       This is to enforce that the length of time allotted to reversing feels the same regardless of whatever frame-rate a user's machine is at.
    * Then insert a point in pointsInTime
    *     the Insert method takes an index and a instance of an object of the same type that the list contains 
    *     Note: They way that points are stored the list is in reverse chronological order, with the most recent point first and oldest point last.
    */
    void Track(){
        if(pointsInTime.Count > Mathf.Round(rewindTime / Time.fixedDeltaTime))
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation, rb.velocity));
    }

    /*! \fn StartRewind()
     * Store the current velocity, make true the isRewinding flag, and make the object kinematic
     *  if isKinematic is set to true it means that forces will no longer affect the object, therefore the object will be under full control of the script.
     * Reviewer's note: At this point it seems that StartRewind makes reversing more of a replay than an actual reversing of a vehicle
    */
    public void StartRewind(){
        BroadcastMessage("DisplayRewind");
        veloBeforeRewind = rb.velocity;
        isRewinding = true;
        rb.isKinematic = true;
    }

    /*! \fn StopRewind()
     * set the current velocity to what it was before, make false the isRewinding flag, and make the object no longer kinematic
    */
    public void StopRewind(){
        //rb.velocity = veloBeforeRewind;
        BroadcastMessage("StopDisplayRewind");
        BroadcastMessage("HelmetCheck"); //respawns the atv after rewind if not wearing helmet
                                         //needs fine tuning/ explanation for player
        rb.velocity = Vector3.zero;
        isRewinding = false;
        rb.isKinematic = false;
    }
}
