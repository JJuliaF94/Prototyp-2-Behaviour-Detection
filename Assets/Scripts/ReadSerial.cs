using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//The System.IO.Ports namespace contains classes for controlling serial ports
using System.IO.Ports;

//[RequireComponent (typeof(AudioSource))]

public class ReadSerial : MonoBehaviour
{
    //Variables
    private GameObject AnimationSad;
    private GameObject AnimationHappy;
    // GameObject having the attached video
    private UnityEngine.Video.VideoPlayer videofileSad;
    private UnityEngine.Video.VideoPlayer videofileHappy; // VideoPLayer component 
    //'bool' is used to declare variables for storing the Boolean values TRUE and FALSE
    //the happy and sad videos are in general not activated
    /*we have implemented these two variables so that the animations (videos) are not played again if the same animation (video) has already been played back, so if the happy video is played at the moment, the happy value is true so it is activated and the sad value is not active
    if the sad video is played at the moment, the sad value is true so it is activated and the happy value is not active (this part in detail in the code starts at line 55)*/
    private bool HappyActive = false;
    private bool SadActive = false;

    //here the port of the used Arduino and the baud rate must be entered
    SerialPort stream = new SerialPort("/dev/cu.usbmodem14101", 9600);

    // Start is called before the first frame update
    void Start()
    {   
        //Open the Serial Stream
        stream.Open();
        //logs 'start' at the console, if the Stream is open
        Debug.Log("start");
        AnimationSad = GameObject.Find("AnimationSad"); // assigning GameObject
        AnimationHappy = GameObject.Find("AnimationHappy"); // assigning GameObject
        //the variable receives the video file from the component of the game object 
        videofileSad = AnimationSad.GetComponent<UnityEngine.Video.VideoPlayer>();
        videofileHappy = AnimationHappy.GetComponent<UnityEngine.Video.VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        string value = stream.ReadLine();
        //the console outputs the value 0 or 1 that it receives from the Arduino
        Debug.Log(value);

        /*if the Serial Stream is open and the value of the Arduino (which includes the MPR121) is 1, which means, that the Sensor isn't touched, so there is no person sitting on the chair, the console outputs 'mood is good'*/
        if (stream.IsOpen && int.Parse(value) == 1)
            {
            Debug.Log("mood is good");
            /*if the videofile happy is not playing and the videofile sad is not playing and the happy videofile is not active, activate the happy video, deactivate the sad video, stop the sad video and start the happy video
            with other words: the happy animation is active and should be played, the sad animation is not active and shouldn't be played*/
            if(!videofileHappy.isPlaying && !videofileSad.isPlaying && !HappyActive)
            {
                //the video is never interrupted, it is first played ready before the other file is output
                HappyActive = true;
                SadActive = false;
                videofileSad.Stop();
                videofileHappy.Play();
            }
        }

        else 
        {
            Debug.Log("mood is bad");
            /*if the videofile happy is not playing and the videofile sad is not playing and the sad videofile is not active, deactivate the happy video, activate the sad video, stop the happy video and start the sad video
            with other words: the happy animation is not active and should not be played, the sad animation is active and should be played*/
            if (!videofileHappy.isPlaying && !videofileSad.isPlaying && !SadActive)
            {
                //the video is never interrupted, it is first played ready before the other file is output
                HappyActive = false;
                SadActive = true;
                videofileHappy.Stop();
                videofileSad.Play();
            }
             
            }

    }
}
