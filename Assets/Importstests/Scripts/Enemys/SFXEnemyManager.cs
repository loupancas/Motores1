using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXEnemyManager : MonoBehaviour
{

    //patricio malvasio maddalena ( eventos extras)


    [Header("variables")]
    public string startingMusic;

    [Header("References")]
    public SoundClip[] SFXsound;
    public AudioSource SFXsource;

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
            SFXsource.clip = Sound.Clip;
            SFXsource.Play();
        }
    }
}
