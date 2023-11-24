//NEW NEW CHANGES

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioData : ScriptableObject
{
    public enum AudioCategory{
        GAMEPLAY,
    }


    public AudioClip audioClip;//Clip that plays
    public AudioCategory audioCategory;//What category the clip falls under
    public float volume;//Volume on scale 0-1
    public int timesToRepeat;//Times the clip should repeat

}
