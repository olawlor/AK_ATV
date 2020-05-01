using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.Extras;

public class LaserPointerHandler : MonoBehaviour
{
    public SteamVR_LaserPointer laser;
    public SteamVR_Action_Boolean laserAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("default", "MenuClick");
    public SteamVR_Input_Sources laserSource = SteamVR_Input_Sources.LeftHand;
    private bool isPressed = false;

    void Awake() {
        laser.PointerIn += PointerInside;
        laser.PointerOut += PointerOutside;
        laser.PointerClick += PointerClick;
    }

    void Start() {
        laserAction.AddOnStateDownListener(ButtonPressed, laserSource);
        laserAction.AddOnStateUpListener(ButtonReleased, laserSource);
    }

    public void ButtonPressed(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (!isPressed && !laser.active) laser.active = true;
        else if (!isPressed && laser.active) laser.active = false;

        isPressed = true;
    }

    public void ButtonReleased(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        isPressed = false;
    }

    public void PointerClick(object sender, PointerEventArgs e) {
        //Debug.Log("Pointer clicked " + e.target.name);
        IPointerClickHandler click = e.target.GetComponent<IPointerClickHandler>();
        if (click == null)
        {
            return;
        }

        click.OnPointerClick(new PointerEventData(EventSystem.current));
    }

    public void PointerInside(object sender, PointerEventArgs e) {
        //Debug.Log("Pointer inside " + e.target.name);
        IPointerEnterHandler inside = e.target.GetComponent<IPointerEnterHandler>();
        if (inside == null) {
            return;
        }

        inside.OnPointerEnter(new PointerEventData(EventSystem.current));
    }

    public void PointerOutside(object sender, PointerEventArgs e) {
        //Debug.Log("Pointer outside " + e.target.name);
        IPointerExitHandler outside = e.target.GetComponent<IPointerExitHandler>();
        if (outside == null) {
            return;
        }

        outside.OnPointerExit(new PointerEventData(EventSystem.current));
    }
}
