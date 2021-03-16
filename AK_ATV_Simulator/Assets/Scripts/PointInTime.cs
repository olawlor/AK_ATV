/*! \file PointInTime.cs
 * \brief The source for the class PointInTime
*/
using UnityEngine;

public class PointInTime
{
    /*! \var position
    * \brief stored the position of the vehicle at a particular point in time
    */
   public Vector3 position;
   /*! \var rotation
    * \brief stored the rotation of the vehicle at a particular point in time
    */
   public Quaternion rotation;
   /*! \var velocity
    * \brief stored the velocity of the vehilce at a particular point in time
    */
   public Vector3 velocity;
    /*! \fn public PointInTime()
    * \brief sets the variables to values
    */
   public PointInTime(Vector3 _position, Quaternion _rotation, Vector3 _velocity){
       position = _position;
       rotation = _rotation;
       velocity = _velocity;
   }
}
