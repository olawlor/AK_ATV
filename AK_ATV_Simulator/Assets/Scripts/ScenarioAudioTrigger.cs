/* Audio trigger for scenarios */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ScenarioAudioTrigger : MonoBehaviour
{
    public AudioSource playSound;
    void OnTriggerEnter(Collider other)
    {
        playSound.Play();
    }
}