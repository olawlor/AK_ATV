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
        Debug.Log("Pointer clicked " + e.target.name);
        IPointerClickHandler click = e.target.GetComponent<IPointerClickHandler>();
        if (click == null)
        {
            return;
        }

        click.OnPointerClick(new PointerEventData(EventSystem.current));
    }

    public void PointerInside(object sender, PointerEventArgs e)
    {
        Debug.Log("Pointer inside " + e.target.name);
        IPointerEnterHandler inside = e.target.GetComponent<IPointerEnterHandler>();
        if (inside == null)
        {
            return;
        }

        inside.OnPointerEnter(new PointerEventData(EventSystem.current));
    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {
        Debug.Log("Pointer outside " + e.target.name);
        Debug.Log("Pointer inside " + e.target.name);
        IPointerExitHandler outside = e.target.GetComponent<IPointerExitHandler>();
        if (outside == null)
        {
            return;
        }

        outside.OnPointerExit(new PointerEventData(EventSystem.current));
    }
}
