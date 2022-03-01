// Attach this script to VehicleDropdown in UI canvas.
// Vehicle GameObjects are the parent vehicle GameObjects in the hierarchy

using UnityEngine;
using UnityEngine.UI;

public class DropdownValue : MonoBehaviour
{
    //Attach this script to a Dropdown GameObject
    Dropdown m_Dropdown;
    //This is the string that stores the current selection m_Text of the Dropdown
    string m_Message;
    //This is the index value of the Dropdown
    int m_DropdownValue;
    public GameObject mainVehicle;
    public GameObject otherVehicle;

    void Start()
    {
        //Fetch the Dropdown GameObject
        m_Dropdown = GetComponent<Dropdown>();
        //Add listener for when the value of the Dropdown changes, to take action
        m_Dropdown.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(m_Dropdown);
        });
    }

    void Update()
    {

        //Keep the current index of the Dropdown in a variable
        m_DropdownValue = m_Dropdown.value;
        Debug.Log(m_DropdownValue);
        //Change the message to say the name of the current Dropdown selection using the value
        m_Message = m_Dropdown.options[m_DropdownValue].text;
    }

    //Ouput the new value of the Dropdown into Text
    public void DropdownValueChanged(Dropdown change)
    {
        m_DropdownValue = m_Dropdown.value;
        if (m_DropdownValue == 0)
        {
            mainVehicle.SetActive(true);
            otherVehicle.SetActive(false);
        }
        else if (m_DropdownValue == 1)
        {
            mainVehicle.SetActive(false);
            otherVehicle.SetActive(true);
        }
    }
}
