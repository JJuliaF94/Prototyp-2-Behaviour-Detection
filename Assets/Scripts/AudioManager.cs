//this was actually to include music in the animations but the Unity Version of the MacBook that we used at the Interactive Future Exhibition was to old to include that so we deleted it from the Video Player Elements

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //BGM stands for Backgroundmusic ;)
    public AudioSource BGM;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeBGM(AudioClip music)
    {
        BGM.Stop();
        BGM.clip = music;
        BGM.Play();
    }
}
