using UnityEngine.Audio;
using UnityEngine;
using System;

[Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    public float volume;
    public float pitch;

    public bool looping;

    [HideInInspector]
    public AudioSource source;
}
