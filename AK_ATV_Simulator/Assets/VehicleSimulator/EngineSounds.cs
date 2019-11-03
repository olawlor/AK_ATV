/*
 Make sound depending on the engine and vehicle speed.
 
*/

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineSounds : MonoBehaviour
{
    private float phase=0.0f;
    private float sampling_frequency =48000.0f;
    public float volume=0.005f;
    private VehicleProperties vehicle;
    
    void Start() {
        vehicle=gameObject.GetComponent<VehicleProperties>();
        if (!vehicle) Debug.Log("Missing vehicle at audio setup.");
    }
    
    void OnAudioFilterRead(float[] audio,int nchannels) {
        if (!vehicle) return;
        
        float frequency=10.0f+Mathf.Max(20.0f*(vehicle.cur_motor_power)+2.0f*Mathf.Abs(vehicle.mph),-1.0f);
        float twopi=2.0f*Mathf.PI;
        float increment=frequency*twopi / sampling_frequency;
        for (int i=0;i<audio.Length;i+=nchannels) 
        {
            float v=Mathf.Sin(phase);
            if (v>0.8f) v=-1.0f; // make it clippier (models explosions)
            phase+=increment;
            if (phase>twopi) phase -= twopi;
            
            v*=volume;
            
            for (int c=0;c<nchannels;c++)
                audio[i+c]=v;
        }
        
    }
}
