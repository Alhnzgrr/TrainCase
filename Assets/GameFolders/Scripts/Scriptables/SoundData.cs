using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound Data", menuName = "Data/Sound Data")]
public class SoundData : ScriptableObject
{
    [SerializeField] SoundClip[] soundClips;

    public SoundClip[] SoundClips => soundClips;
}
