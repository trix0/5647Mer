
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;
public class MusicController : MonoBehaviour
{
    public Text currentSpeedText;
    private AudioSource currentPlaying;
    public DateTime currentStartedPlaying;
    public Sound[] sounds;



    [Header("Audio Stuff")]



    public AudioSource audioSource;
    public AudioClip audioClip;
    public string soundPath;
    public int currentSongLevel = 1;
    public int currentSongOrder = 1;

    private void Awake()
    {

        foreach(Sound s in sounds)
        {
            s.source=gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;


        }
        Play(1,1);

    }

    public void Play(int level, int levelOrder)
    {
        try {
            Sound s = Array.Find(sounds, sound => {

                if(sound.Level==level&&sound.LevelOrder== levelOrder)
                {
                    return true;
                }
                return false;
            });
            s.source.Play();
            currentPlaying = s.source;
            currentStartedPlaying = DateTime.Now;
        }
        catch(Exception e)
        {
            throw new Exception(e.StackTrace);
        }

    }

    void Update()
    {

        DateTime now = DateTime.Now;
        int currentSpeed = int.Parse(currentSpeedText.text);
        //7.5

        //1.875*2=3.75

        //15:40:10 currentStartedPlaying

        //15:40:20 now

        //50-100,  100-150, 150+
        //10-59, 60-119, 120+
        //

        if (currentSpeed >0 &&currentSpeed<=59)
        {
            currentSongLevel = 2;
        }
        else if (currentSpeed > 60 && currentSpeed <= 119)
        {
            currentSongLevel = 3;
        }
        else if (currentSpeed > 120)
        {
            currentSongLevel = 3;
        }
        var difference = (now- currentStartedPlaying).TotalMilliseconds;
        if (difference >= 3750)
        {
            if (currentSongOrder >= 16)
            {
                currentSongOrder = 1;
            }
            else
            {
                currentSongOrder++;
            }
            Play(currentSongLevel,currentSongOrder);
        }





    }


}
