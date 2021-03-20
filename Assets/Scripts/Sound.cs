
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[System.Serializable]
public class Sound {

    public int Level;
    public int LevelOrder;
    public string name;
    [HideInInspector]
    public AudioSource source;

    public AudioClip clip;
}
