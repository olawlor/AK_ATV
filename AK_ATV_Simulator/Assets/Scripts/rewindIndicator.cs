using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rewindIndicator : MonoBehaviour
{
    CanvasGroup indicatorCanvas;
    // Start is called before the first frame update
    void Start()
    {
        indicatorCanvas = this.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B)){
            DisplayRewind();
        }
        if(Input.GetKeyUp(KeyCode.B)){
            StopDisplayRewind();
        }
    }
    public void DisplayRewind(){
        
        indicatorCanvas.alpha = 1;
    }

    public void StopDisplayRewind(){   
        indicatorCanvas.alpha = 0;
    }

}
