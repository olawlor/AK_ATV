/*
 Make sound depending on the engine and vehicle speed.
 audio slider currently is setup to adjust the frequency of the engine sound,
  however the function that would actually adjust the frequency is commented out
*/

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class EngineSounds : MonoBehaviour
{
    private float phase=0.0f;
    private float sampling_frequency =48000.0f;
    private float volume;
    private float sliderVol = 0.3f;
    private VehicleProperties vehicle;
    /*! \ public AudioMixer audioMixer; */
    void Start() {
        vehicle=gameObject.GetComponent<VehicleProperties>();
        if (!vehicle) Debug.Log("Missing vehicle at audio setup.");
    }  
    void Update(){
        if(PauseMenu.GameIsPaused){
            volume = 0.001f;
        }
        else{
            volume = sliderVol;
        }
    }

    public void OnValueChanged(float newVolume){
        volume = newVolume;
        sliderVol = newVolume;
    }

    public void SetVolume(float _volume){
        volume = _volume;
        /*! \ audioMixer.SetFloat("AtvVolume", volume); */
    }
    
    void OnAudioFilterRead(float[] audio,int nchannels) {
        if (!vehicle) return;
        
        float frequency=10.0f+Mathf.Max(20.0f*(vehicle.cur_motor_power)+2.0f*Mathf.Abs(vehicle.mph),-1.0f);
        float twopi=2.0f*Mathf.PI;
        float increment=frequency*twopi / sampling_frequency;

            for (int i=0;i<audio.Length;i+=nchannels) 
            {
                float v=Mathf.Sin(phase);
                if (v>0.8f) v=-1.0f; /*! \ make it clippier (models explosions) */
                phase+=increment;
                if (phase>twopi) phase -= twopi;
                
                v*=volume;
                
                for (int c=0;c<nchannels;c++){
                    audio[i+c]=v;
                }
            }
        }        
}
