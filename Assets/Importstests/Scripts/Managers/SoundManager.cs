
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //patricio malvasio maddalena ( eventos extras)


    [Header("variables")]
    public string startingMusic;

    [Header("References")]
    public static SoundManager Instance = null;
    public SoundClip[] SFXsound, BackgroundMusic, fallingSFX;
    public AudioSource SFXsource, BackgroundSource, fallingSource, footstepsSource, runningstepSource;


 

    public void Awake()
    {
        if(Instance== null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
   
          
       
    }

    public void Start()
    {
        PlayMusic(startingMusic);
    }

    public void PlaySFX(string Soundname)
    {
        SoundClip Sound = Array.Find(SFXsound, x => x.AudioName == Soundname);

        print("sound name " + Soundname);

        if (Sound == null)
        {
            Debug.Log("Sound Name Not Found, check name specified");
            return;
        }
        else
        {
            fallingSource.volume = 0;
            SFXsource.clip = Sound.Clip;   
            SFXsource.Play();
        }
    }

    public void PlayMusic(string MusicName)
    {
        SoundClip Music = Array.Find(BackgroundMusic, x => x.AudioName == MusicName);

        print("music name " + MusicName);

        if (Music == null)
        {
            Debug.Log("Sound Name Not Found, check name specified");
            return;
        }

        else
        {
            fallingSource.volume = 0;
            BackgroundSource.clip = Music.Clip;
            BackgroundSource.Play();
        }
    }

    public void EnableFallingSound()
    {
       
        fallingSource.enabled = true;
    }

    public void DisableFallingSound()
    {
        fallingSource.enabled = false;
    }

    public void PlayFootSteps()
    {
        footstepsSource.enabled = true;
        StopRunSteps();
    }

    public void StopFootSteps()
    {
        footstepsSource.enabled = false;
    }

    public void PlayRunStep()
    {
       runningstepSource.enabled = true;
        StopFootSteps();
    }

    public void StopRunSteps()
    {
        runningstepSource.enabled = false;
    }
}



