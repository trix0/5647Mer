
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;
public class MusicController : MonoBehaviour
{
    public Text currentSpeedText;
    public Sound[] sounds;


    public const string audioName = "merzedes-waltz-rythm-64bpm-05.wav";

    [Header("Audio Stuff")]
    public AudioSource audioSource;
    public AudioClip audioClip;
    public string soundPath;

    private void Awake()
    {

        foreach(Sound s in sounds)
        {
            s.source=gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

        }
        Play("1.1");
        Play("1.9");
    }

    public void Play(string name)
    {
        Sound s=Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }


}
