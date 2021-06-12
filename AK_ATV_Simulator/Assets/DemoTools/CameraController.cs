using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
        //transform.position = new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z);
  }

    void FixedUpdate()
  {
    // make sure the camera is above the terrain
    float min_camera_height = Terrain.activeTerrain.SampleHeight(transform.position)
      + Terrain.activeTerrain.transform.position.y
      + 1.5f;
    float current_camera_height = transform.position.y;
    if (current_camera_height < min_camera_height)
    {
      transform.Translate(0, 0.05f, 0);
    }
  }
  // Update is called once per frame
  void Update()
  {
    // Adapted from: http://3dcognition.com/unity-flight-simulator-phase-2/
    //   and http://wiki.unity3d.com/index.php/SmoothMouseLook
    
    // float altitude = transform.position.magnitude;
    
    float rotateSpeed = 30.0f; // degrees/second
    float speed = 1.0f; // * (altitude-0.99f); // WASD movement, earth radius/second
    float mouseSpeed=140.0f; // degrees rotation per pixel of mouse movement / second
    
    float transAmount = speed * Time.deltaTime;
    float rotateAmount = rotateSpeed * Time.deltaTime;
    
    float rotX = 0.0f;
    float rotY = 0.0f;
    
    if (Input.GetMouseButton(0)) {
      rotX += Input.GetAxis("Mouse X") * mouseSpeed * Time.deltaTime;
      rotY -= Input.GetAxis("Mouse Y") * mouseSpeed * Time.deltaTime;
    }

    if (Input.GetKey("up")) {
      rotY+=rotateAmount;	
    }
    if (Input.GetKey("down")) {
      rotY-=rotateAmount;	
    }
    if (Input.GetKey("left")) {
      rotX-=rotateAmount;
    }
    if (Input.GetKey("right")) {
      rotX+=rotateAmount;
    }
    transform.Rotate(rotY,rotX,0);

    if (Input.GetKey ("j")) {
      transform.Translate(-transAmount, 0, 0);
    }
    if (Input.GetKey ("l")) {
      transform.Translate(transAmount, 0, 0);
    }

    if (Input.GetKey ("o")) {
      transform.Translate(0,-transAmount, 0);
    }
    if (Input.GetKey ("u")) {
      transform.Translate(0,transAmount, 0);
    }

    if (Input.GetKey ("i")) {
      transform.Translate(0, 0, transAmount);
    }
    if (Input.GetKey ("k")) {
      transform.Translate(0, 0, -transAmount);
    }
  }
}
