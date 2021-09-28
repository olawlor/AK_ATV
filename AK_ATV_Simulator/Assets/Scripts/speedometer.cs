using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//outdated, delete/refactor to meet new needs
public class spedometer : MonoBehaviour
{
    private const float MAX_SPEED_ANGLE = -180;
    private const float ZERO_SPEED_ANGLE = 40;
    private float speed;
    private float maxSpeed;
    private Transform needleTransform;
    private Transform speedLabelTemplateTransform;
    
    private void Awake(){
        needleTransform = transform.Find("speedDial");
        speedLabelTemplateTransform = transform.Find("speedLabelTemplate");
        speedLabelTemplateTransform.gameObject.SetActive(false);
        speed = 0f;
        maxSpeed = 200f;
        CreateSpeedLabels();
    }
    void Update(){
        needleTransform.eulerAngles = new Vector3(0,0, GetSpeedRotation());
    }
    private void CreateSpeedLabels(){
        int labelAmount = 10;
        float totalAngleSize = ZERO_SPEED_ANGLE - MAX_SPEED_ANGLE;
        for(int i = 0; i <= labelAmount; i++){
            Transform speedLabelTransform = Instantiate(speedLabelTemplateTransform, transform);
            float labelSpeedNormalized = (float)i / labelAmount;
            float speedLabelAngle = ZERO_SPEED_ANGLE -labelSpeedNormalized * totalAngleSize;
            speedLabelTransform.eulerAngles = new Vector3(0,0, speedLabelAngle);
            speedLabelTransform.Find("speedText").GetComponent<Text>().text = Mathf.RoundToInt(labelSpeedNormalized * maxSpeed).ToString();
           // speedLabelTransform.Find("speedText").eulerAngles = Vector3.zero;
            speedLabelTransform.gameObject.SetActive(true);
        }
    }
    public void RecieveSpeed(float spd){
        speed = spd;
    }
    private float GetSpeedRotation(){
        float totalAngleSize = ZERO_SPEED_ANGLE - MAX_SPEED_ANGLE;
        float speedNormalized = speed/maxSpeed;
        return ZERO_SPEED_ANGLE - speedNormalized *totalAngleSize;
    }
}
