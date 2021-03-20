
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[System.Serializable]
public class Sound {

    public int Level;
    public int LevelOrder;
    [HideInInspector]
    public string name;

    public Sound()
    {
        name = Level +"."+ LevelOrder;
    }
    [HideInInspector]
    public AudioSource source;

    public AudioClip clip;
}
