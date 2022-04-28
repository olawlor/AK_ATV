/* Used for triggering the Plane scenario animations. 
   Once the user hit the scenario trigger, 
   the corresponding animation should begin to work.

   Using instructions:
   Attach this script to a GameObject with an Animator component attached.
   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneAnimationTrigger : MonoBehaviour
{
    public Animator animator;

    void OnTriggerEnter(Collider other)
    {
        animator.Play("PlaneController");
    }
}
