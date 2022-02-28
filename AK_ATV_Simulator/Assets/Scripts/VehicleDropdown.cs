//Create a new Dropdown GameObject by going to the Hierarchy and clicking Create>UI>Dropdown. Attach this script to the Dropdown GameObject.
//Set your own Text in the Inspector window

using UnityEngine;
using UnityEngine.UI;

public class VehicleDropdown : MonoBehaviour
{
    //Attach this script to a Dropdown GameObject
    Dropdown m_Dropdown;
    //This is the string that stores the current selection m_Text of the Dropdown
    string m_Message;
    //This is the index value of the Dropdown
    public int m_DropdownValue;

    void Start()
    {
        Debug.Log("We're in " + m_DropdownValue);
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
        GameObject childObject = findChildFromParent("VehicleList", "ATV_New");
        GameObject childObject2 = findChildFromParent("VehicleList", "One_Seater");
        if (m_DropdownValue == 0)
        {
            Debug.Log("We're in 0");
            //childObject.GetComponent<ControllerKeyboard>().enabled = true;
            //childObject2.GetComponent<ControllerKeyboard>().enabled = false;
            //childObject.SetActive(true);
            //childObject2.SetActive(false);
        }
        if (m_DropdownValue == 1)
        {
            Debug.Log("We're in 1");
            //childObject.GetComponent<ControllerKeyboard>().enabled = false;
            //childObject2.GetComponent<ControllerKeyboard>().enabled = true;
        }
    }

    GameObject findChildFromParent(string parentName, string childNameToFind)
    {
        string childLocation = "/" + parentName + "/" + childNameToFind;
        GameObject childObject = GameObject.Find(childLocation);
        return childObject;
    }
}
