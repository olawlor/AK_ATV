using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class compass : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playerTransform;
    Vector3 dir;
    private void Awake(){
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        dir.x = playerTransform.eulerAngles.y;
        transform.localEulerAngles = dir;
    }
}
