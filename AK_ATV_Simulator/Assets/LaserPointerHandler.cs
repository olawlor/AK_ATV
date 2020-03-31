using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;

public class LaserPointerHandler : MonoBehaviour
{
    public SteamVR_LaserPointer laser;

    void Awake()
    {
        laser.PointerIn += PointerInside;
        laser.PointerOut += PointerOutside;
        laser.PointerClick += PointerClick;
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {

    }

    public void PointerInside(object sender, PointerEventArgs e)
    {
        Debug.Log("Pointer inside " + e.target.name);
    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {
        Debug.Log("Pointer outside " + e.target.name);
    }
}
